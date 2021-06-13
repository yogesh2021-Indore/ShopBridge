using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Data.Repository
{
    public interface IProductRepository
    {
        Task<int> Upsert(Product product);
        Task<int> DeleteProduct(int ProductId);
        Task<Product> ReadProduct(int ProductId);
        Task<List<Product>> ReadProducts();
    }
}
