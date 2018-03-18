using ProductService.Models;
using System.Collections.Generic;

namespace ProductService.Data
{
    /// <summary>
    /// Product Repository Interface
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Get All Products
        /// </summary>
        /// <returns></returns>
        IEnumerable<Product> GetAll();
        /// <summary>
        /// Get Product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Product Get(string id);
        /// <summary>
        /// Add Product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Product Add(Product product);
        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Product Update(Product product);
        /// <summary>
        /// Delete Product
        /// </summary>
        /// <param name="id"></param>
        void Delete(string id);
        /// <summary>
        /// Get Products by filter
        /// </summary>
        /// <param name="model"></param>
        /// <param name="description"></param>
        /// <param name="brand"></param>
        /// <returns>Collection of products</returns>
        IEnumerable<Product> GetByFilter(string model, string description, string brand);
    }
}