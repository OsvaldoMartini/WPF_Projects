namespace MVC_WPF_Search
{
    /// <summary>
    /// Defines a list of messages that the controllers use to communicate with each other
    /// </summary>
    public static class Messages
    {
        /// <summary>
        /// Message to notify that all products should be fetched
        /// </summary>
        public const string GetAllProducts = "getAll";

        /// <summary>
        /// Message to notify that a search by name should be made
        /// </summary>
        public const string SearchByName = "searchByName";

        /// <summary>
        /// Message to notify that a product is selected
        /// </summary>
        public const string SelectProduct = "selectProduct";
    }
}
