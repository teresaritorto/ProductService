namespace ProductService.Models
{
    /// <summary>
    /// Query model
    /// </summary>
    public class FilterProductQuery
    {
        /// <summary>
        /// Model query
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// Brand query
        /// </summary>
        public string Brand { get; set; }
        /// <summary>
        /// Description query
        /// </summary>
        public string Description { get; set; }
    }
}