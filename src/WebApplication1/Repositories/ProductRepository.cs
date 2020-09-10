using Catalog.API.Data.Interfaces;
using Catalog.API.Entities;
using Catalog.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogContext _context;

        public ProductRepository(CatalogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            // return await _context
            //                 .Products
            //                 .Find(p => true)
            //                 .ToListAsync();
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProduct(string id)
        {
            // return await _context
            //                 .Products
            //                 .Find(p => p.Id == id)
            //                 .FirstOrDefaultAsync();
            return null;
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            // FilterDefinition<Product> filter = Builders<Product>.Filter.ElemMatch(p => p.Name, name);
            //
            // return await _context
            //               .Products
            //               .Find(filter)
            //               .ToListAsync();
            return null;
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            // FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);
            //
            // return await _context
            //               .Products
            //               .Find(filter)
            //               .ToListAsync();
            return null;
        }

        public async Task Create(Product product)
        {
            // await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> Update(Product product)
        {
            // var updateResult = await _context
            //                             .Products
            //                             .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);
            //
            // return updateResult.IsAcknowledged
            //         && updateResult.ModifiedCount > 0;
            return false;
        }

        public async Task<bool> Delete(string id)
        {
            // FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(m => m.Id, id);
            // DeleteResult deleteResult = await _context
            //                                     .Products
            //                                     .DeleteOneAsync(filter);
            //
            // return deleteResult.IsAcknowledged
            //     && deleteResult.DeletedCount > 0;
            return false;
        }        
    }
}
