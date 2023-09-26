using AutoMapper;
using CartItems.Api.Dtos.CartItems;
using CartItems.Api.Interfaces.IPersistence;
using CartItems.Api.Interfaces.IServices;
using CartItems.Api.Models;
using CartItems.Api.Responses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cart.Api.Services
{
    public class CartItemsService : ICartItemsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        const string ItemName = "Cart Item";

        public CartItemsService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResponse<string>> CreateCartItemAsync(CreateCartItemDto requestBody)
        {
            var userId = int.Parse(_httpContextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? string.Empty);

            //item in cart check
            var cartItem = (await _unitOfWork.CartItems.QueryAsync
            (
                filter: cart => (cart.ItemId == requestBody.ItemId) && (cart.UserId == userId)
            )).FirstOrDefault();

            if (cartItem == null)
            {
                var item = await _unitOfWork.Items.GetAsync(requestBody.ItemId);
                if (item == null) return new ApiResponse<string>(
                    false,
                    null,
                    (int)HttpStatusCode.BadRequest,
                    ResponseMessages.NotFound(ItemName, requestBody.ItemId)
                );

                //new cart item instance
                var model = _mapper.Map<CartItemModel>(requestBody);

                //add user Id to cart record
                model.UserId = userId;

                await _unitOfWork.CartItems.CreateAsync(model);
                var saveSuccess = await _unitOfWork.SaveAsync();

                return new ApiResponse<string>(
                    saveSuccess,
                    saveSuccess ? ResponseMessages.Created(ItemName) : null,
                    saveSuccess ? (int)HttpStatusCode.OK : (int)HttpStatusCode.BadRequest,
                    saveSuccess ? null : ResponseMessages.OperationFailed(ItemName)
                );
            }

            //item exists in cart instance
            cartItem.Quantity += requestBody.Quantity;
            cartItem.LastUpdatedOn = DateTime.UtcNow;

            await _unitOfWork.CartItems.UpdateAsync(cartItem.CartId, cartItem);
            var updateSuccess = await _unitOfWork.SaveAsync();

            return new ApiResponse<string>(
                updateSuccess,
                updateSuccess ? ResponseMessages.Created(ItemName) : ResponseMessages.OperationFailed(ItemName),
                updateSuccess ? (int)HttpStatusCode.OK : (int)HttpStatusCode.BadRequest,
                updateSuccess ? null : ResponseMessages.OperationFailed(ItemName)
            );
        }

        public async Task<ApiResponse<string>> UpdateCartItemAsync(UpdateCartItemDto requestBody)
        {
            var existingCartItem = await _unitOfWork.CartItems.GetAsync(requestBody.CartId);
            if (existingCartItem == null) return new ApiResponse<string>(
                false,
                null,
                (int)HttpStatusCode.BadRequest,
                ResponseMessages.NotFound(ItemName, requestBody.CartId)
            );

            // edit cart item's quantity and last updated
            existingCartItem.Quantity = requestBody.Quantity;
            existingCartItem.LastUpdatedOn = DateTime.UtcNow;

            await _unitOfWork.CartItems.UpdateAsync(requestBody.CartId, existingCartItem);
            var success = await _unitOfWork.SaveAsync();

            return new ApiResponse<string>(
                success,
                success ? ResponseMessages.Updated(ItemName, requestBody.CartId) : null,
                success ? (int)HttpStatusCode.OK : (int)HttpStatusCode.BadRequest,
                !success ? ResponseMessages.OperationFailed(ItemName) : null
            );
        }

        public async Task<ApiResponse<string>> DeleteCartItemAsync(int id)
        {
            await _unitOfWork.CartItems.DeleteAsync(id);

            var success = await _unitOfWork.SaveAsync();

            return new ApiResponse<string>(
                success,
                success ? ResponseMessages.Deleted(ItemName, id) : null,
                success ? (int)HttpStatusCode.OK : (int)HttpStatusCode.NotFound,
                !success ? ResponseMessages.NotFound(ItemName, id) : null
            );
        }

        public async Task<ApiResponse<IReadOnlyList<GetCartItemDto>>> GetAllCartItemsAsync(GetCartItemQuery query)
        {
            //values from header
            var userId = int.Parse(_httpContextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? string.Empty);
            var role = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;

            int? contactMatchedUserId = null;
            if (query.phoneNumber != null)
            {
                contactMatchedUserId = (await _unitOfWork.Users.QueryAsync
                 (
                     filter: user => user.Contact.Trim().ToLower() == query.phoneNumber.Trim().ToLower()
                 )).FirstOrDefault()?.UserId ?? 0;
            }
            

            //filters
            Expression<Func<CartItemModel, bool>> filter = p =>
                (role == "user" ? p.UserId == userId : true) &&
                ((contactMatchedUserId != null && role != "user") ? p.UserId == contactMatchedUserId : true) &&
                (query.startDate != null ? p.CreatedOn.Date >= query.startDate.Value.Date : true) &&
                (query.endDate != null ? p.CreatedOn.Date <= query.endDate.Value.Date : true) &&
                (query.quantity > 0 ? p.Quantity == query.quantity : true) && 
                (query.item != null ? p.Item.ItemName.Trim().ToLower().Contains(query.item.Trim().ToLower()) : true);

            //includes
            var includes = new Expression<Func<CartItemModel, object>>[] { e => e.Item};

            var fromDb = await _unitOfWork.CartItems.QueryAsync
            (
                filter: filter,
                includes : includes,
                page : query.page ?? 0,
                pageSize : query.PageSize ?? 0
            );
            
            var result = _mapper.Map<IReadOnlyList<GetCartItemDto>>(fromDb);

            return new ApiResponse<IReadOnlyList<GetCartItemDto>>(
                true,
                result,
                (int)HttpStatusCode.OK
            );
        }

        public async Task<ApiResponse<GetCartItemDto>> GetCartItemAsync(int id)
        {
            var fromDb = (await _unitOfWork.CartItems.QueryAsync
            (
                filter: cart => cart.CartId == id,
                includes : new Expression<Func<CartItemModel, object>>[] { e => e.Item}

            )).FirstOrDefault();

            var result = _mapper.Map<GetCartItemDto>(fromDb);
            return new ApiResponse<GetCartItemDto>(
                result == null,
                result,
                result == null ? (int)HttpStatusCode.NotFound : (int)HttpStatusCode.OK,
                result == null ? ResponseMessages.NotFound(ItemName, id) : null
            );
        }
    }
}
