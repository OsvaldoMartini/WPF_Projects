namespace MVC_WPF_Search.Model
{
    /// <summary>
    /// Holds the data for a specific product
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets the product name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the product
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the price of the product
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets the path of the image
        /// </summary>
        public string Image { get; set; }
    }
}
