namespace BookStoreApp.API.Static
{
    public static class Messages
    {
        public const string Error500Message = "There was an error completing your request. Please Try Again Later.";
        public const string RequestInitiated = "{ActionName}: Request received.";
        public const string EntityNotFound = "{ActionName}: {Entity} with id {Id} was not found.";
        public const string IdMismatch = "{ActionName} ({Entity}): Route id {RouteId} does not match payload id {PayloadId}.";
        public const string DeletingEntity = "{ActionName}: Removing {Entity} with id {Id}.";
        public const string ErrorPerformingAction = "{ActionName}: An unexpected error occurred.";
        public const string ConcurrencyError = "{ActionName}: Concurrency issue detected for {Entity} with id {Id}.";
        public const string AuthorEntityName = "Author";
    }
}
