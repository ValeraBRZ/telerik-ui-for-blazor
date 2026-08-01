using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TelerikBlazorDemos.DataAccess.Dto;

namespace TelerikBlazorDemos.DataAccess.Services
{
    public class ProductService
    {
        private readonly IDbContextFactory<DemoDBContext> _contextFactory;

        private List<ProductDto> _products;

        public ProductService(IDbContextFactory<DemoDBContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<ProductDto> GetProducts()
        {
            return GetProductsInternal();
        }

        public void CreateProduct(ProductDto product)
        {
            if (!_products.Any())
            {
                product.ProductId = 1;
            }
            else
            {
                product.ProductId = _products.Max(p => p.ProductId) + 1;
            }

            _products.Insert(0, product);
        }

        public void UpdateProduct(ProductDto product)
        {
            var target = _products.FirstOrDefault(p => p.ProductId == product.ProductId);
            if (target != null)
            {
                target.ProductName = product.ProductName;
                target.SupplierId = product.SupplierId;
                target.CategoryId = product.CategoryId;
                target.CategoryName = product.CategoryName;
                target.QuantityPerUnit = product.QuantityPerUnit;
                target.UnitPrice = product.UnitPrice;
                target.UnitsInStock = product.UnitsInStock;
                target.UnitsOnOrder = product.UnitsOnOrder;
                target.ReorderLevel = product.ReorderLevel;
                target.Discontinued = product.Discontinued;
            }
        }

        public void DeleteProduct(ProductDto product)
        {
            var target = _products.FirstOrDefault(p => p.ProductId == product.ProductId);
            if (target != null)
            {
                _products.Remove(target);
            }
        }

        private IEnumerable<ProductDto> GetProductsInternal()
        {
            if (_products == null)
            {
                using var dbContext = _contextFactory.CreateDbContext();
                // downloading the data before selecting fix a problem with closed db connection in sqlite.
                // The problem may be related to: https://github.com/dotnet/efcore/issues/24015
                _products = dbContext.Products.Include("Category").ToList().Select(ProductDto.ProductFunc).ToList();
            }

            return _products;
        }
    }
}
