# E-commerce Cart API Documentation
<p>This API is designed to provide a unified shopping cart experience for users. It allows users to manage their shopping carts by adding items, removing items, listing cart items, and retrieving information about individual items.</p>

## Cart Model
The `CartModel` represents an item in the shopping cart.

### Properties

- **Id** (integer): Unique identifier for the cart item.
- **UserId** (integer): Identifier of the user associated with the cart item.
- **Quantity** (integer): The quantity of the item in the cart.
- **Item** (object, optional): Information about the item in the cart. (e.g., `ItemModel`)
- **CreatedOn** (datetime): The date and time when the cart item was created.
- **LastUpdatedOn** (datetime, optional): The date and time when the cart item was last updated.

### Example

```json
{
    "Id": 1,
    "UserId": 123,
    "Quantity": 2,
    "Item": {
        "Id": 12345,
        "Name": "Product A",
        "Price": 19.99
    },
    "CreatedOn": "2023-09-18T12:00:00Z",
    "LastUpdatedOn": "2023-09-19T10:30:00Z"
}
```

## Add Item to Cart
Adds items to the shopping cart with a specified quantity. If the same item ID already exists in the cart, the quantity will be increased.

<p><strong>HTTP Method : <strong/> POST</p>
<p><strong>Endpoint : <strong/> /cartItems</p>

### Request Body
An example of the request body

```json
{
    "itemId": 4,
    "Quantity": 2
}
```

### Success Response Status 
201 Created


## Remove Item from Cart
Removes a specific item from the shopping cart with an Id.

<p><strong>HTTP Method : <strong/> DELETE</p>
<p><strong>Endpoint : <strong/> /cartItems/{id}</p>

### Success Response Status 
200 Ok


## List Cart Items
Retrieves a list of all items in the shopping cart, optionally filtered by phone numbers, time, quantity, and item name.

<p><strong>HTTP Method : <strong/> Get</p>
<p><strong>Endpoint : <strong/> /cartItems</p>

### Query Parameters
Parameters for filtering cart items and paginate
- **Phone Numbers** (string, optional): Filter cart items by phone numbers.
- **Time** (datetime, optional): Filter cart items by time.
- **Quantity** (integer, optional): The quantity of the item in the cart.
- **Item** (string, optional):  Filter cart items by item name.
- 
- **page** (integer, optional): Filter cart items by quantity.
- **page Size** (integer): For Pagination.

### Success Response Status 
200 Ok


## Get Single Item from Cart
Retrieves information about a specific item in the shopping cart by ID.

<p><strong>HTTP Method : <strong/> Get</p>
<p><strong>Endpoint : <strong/> /cartItems/{id}</p>

### Success Response Status 
200 Ok


## Error Handling
In case of errors, the API will respond with appropriate HTTP status codes and error messages in the response body.

<p><strong>400 Bad Request : <strong/> Invalid request format or missing required fields.</p>
<p><strong>404 Not Found : <strong/> Item not found in the cart.</p>
<p><strong>500 Internal Server Error : <strong/> Server encountered an unexpected error.</p>
<p><strong>401 Unauthorized  : <strong/> User authentication is required for the requested operation.</p>
<p><strong>403 Not Forbidden : <strong/> User lacks permission to perform the requested operation.</p>


## Installation Guide

This guide will walk you through the process of setting up and running the E-commerce Cart API on your local development environment.

### Prerequisites

Before you begin, ensure you have the following prerequisites installed on your system:

- [.NET SDK](https://dotnet.microsoft.com/download) (version X.X.X or higher)
- [Git](https://git-scm.com/downloads)

## Installation Steps

1. **Clone the Repository**

   Open your terminal or command prompt and run the following command to clone the repository:

   ```shell
   git clone https://github.com/yourusername/your-repo.git

2. **Navigate to the Project Directory**

   Change your current directory to the project's root folder:

   ```shell
   cd your-repo

2. **Build the Project**

  Build the .NET project to restore dependencies and compile the code:

   ```shell
   dotnet build
