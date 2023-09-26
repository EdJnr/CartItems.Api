using AutoMapper;
using CartItems.Api.Dtos.CartItems;
using CartItems.Api.Dtos.Items;
using CartItems.Api.Interfaces.IPersistence;
using CartItems.Api.Interfaces.IServices;
using CartItems.Api.Models;
using CartItems.Api.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Cart.Api.Services
{
    public class ItemsService : IItemsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        const string ItemName = "Item";

        public ItemsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<string>> AddItemAsync(CreateItemDto payload)
        {
            //Intial check if item exists
            var existingItem =
                (
                     await _unitOfWork.Items.QueryAsync(filter: item => item.ItemName.Trim().ToLower() == payload.ItemName)
                ).FirstOrDefault();

            if (existingItem != null)
            {
                return new ApiResponse<string>
               (
                   false,
                   null,
                   (int)HttpStatusCode.BadRequest,
                   ResponseMessages.Exists(ItemName, "Name", payload.ItemName)
               );
            }

            var model = _mapper.Map<ItemModel>(payload);

            await _unitOfWork.Items.CreateAsync(model);
            bool success = await _unitOfWork.SaveAsync();

            return new ApiResponse<string>
            (
                success,
                success ? ResponseMessages.Created(ItemName) : null,
                success ? (int)HttpStatusCode.OK : (int)HttpStatusCode.BadRequest,
                !success ? ResponseMessages.OperationFailed(ItemName) : null
            );
        }

        public async Task<ApiResponse<string>> DeleteItemAsync(int id)
        {
            await _unitOfWork.Items.DeleteAsync(id);

            bool success = await _unitOfWork.SaveAsync();

            return new ApiResponse<string>(
                success,
                success ? ResponseMessages.Deleted(ItemName, id) : null,
                success ? (int)HttpStatusCode.OK : (int)HttpStatusCode.NotFound,
                !success ? ResponseMessages.NotFound(ItemName, id) : null
            );
        }

        public async Task<ApiResponse<IReadOnlyList<GetItemDto>>> GetItemsAsync(string? searchText)
        {
            var fromDb = (searchText == null) ?
                await _unitOfWork.Items.GetAllAsync()
                :
                await _unitOfWork.Items.QueryAsync
                (
                    filter: item => item.ItemName.Trim().ToLower().Contains(searchText.Trim().ToLower())
                );

            var results = _mapper.Map<IReadOnlyList<GetItemDto>>(fromDb);

            return new ApiResponse<IReadOnlyList<GetItemDto>>(
                true,
                results,
                (int)HttpStatusCode.OK
            );
        }
    }
}
