

namespace CartItems.Api.Responses
{
    public static class ResponseMessages
    {
        public static string Created(string ItemName)
        {
            return $"{ItemName} created successfully";
        }

        public static string Exists(string ItemName, string recordName, string recordValue)
        {
            return $"{ItemName} with {recordName} '{recordValue}' already exists";
        }

        public static string Updated(string ItemName, int id)
        {
            return $"{ItemName} with id {id} updated successfully";
        }

        public static string Deleted(string ItemName, int id)
        {
            return $"{ItemName} with id {id} deleted successfully";
        }

        public static string NotFound(string ItemName, int id)
        {
            return $"No {ItemName} matches the id {id}.";
        }

        public static string NotFoundSearch(string ItemName, string searchtext)
        {
            return $"No {ItemName} matches {searchtext}.";
        }

        public static string OperationFailed(string ItemName)
        {
            return $"Operation Failed. Failed to create{ItemName}";
        }
    }
}