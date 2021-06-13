using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly InventoryContext _inventoryContext;
        public ProductRepository(InventoryContext inventoryContext)
        {
            _inventoryContext = inventoryContext;
        }
        public async Task<int> DeleteProduct(int productId)
        {
            var product = await _inventoryContext.Product.Where(x => x.ProductId == productId).FirstOrDefaultAsync();
            if (product != null)
            {
                _inventoryContext.Product.Remove(product);
                _inventoryContext.SaveChanges();
                return 1;
            }
            return 0;

        }

        public async Task<Product> ReadProduct(int productId)
        {
            var product = await _inventoryContext.Product.Where(x => x.ProductId == productId).FirstOrDefaultAsync();
            return product;
        }

        public async Task<List<Product>> ReadProducts()
        {
            try
            {
                var result = await _inventoryContext.Product.ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> Upsert(Product product)
        {
            try
            {
                if (product.ProductId == 0)
                {
                    await _inventoryContext.Product.AddAsync(product);
                    var result = await _inventoryContext.SaveChangesAsync();
                    return result;
                }
                else
                {
                    if (_inventoryContext.Product.Any(x => x.ProductId == product.ProductId))
                    {
                        _inventoryContext.Product.Update(product);
                        var result = await _inventoryContext.SaveChangesAsync();
                        return result;
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
