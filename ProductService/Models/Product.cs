namespace ProductService.Models
{
    /// <summary>
    /// Product POCO
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Get/Set Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Get/Set Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Get/Set Model
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// Get/Set Brand
        /// </summary>
        public string Brand { get; set; }
    }
}