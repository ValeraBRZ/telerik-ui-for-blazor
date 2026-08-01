using System;
using System.ComponentModel.DataAnnotations;
using TelerikBlazorDemos.DataAccess.Models;

namespace TelerikBlazorDemos.DataAccess.Dto
{
    public class ProductDto
    {
        public int ProductId { get; set; }

        [Required]
        [StringLength(40)]
        public string ProductName { get; set; }

        public int? SupplierId { get; set; }

        public int? CategoryId { get; set; }

        [StringLength(20)]
        public string QuantityPerUnit { get; set; }

        public decimal? UnitPrice { get; set; }

        public short? UnitsInStock { get; set; }

        public short? UnitsOnOrder { get; set; }

        public short? ReorderLevel { get; set; }

        public bool? Discontinued { get; set; }

        public string CategoryName { get; set; }

        public string CategoryDescription { get; set; }

        public static Func<Product, ProductDto> ProductFunc = (product) =>
            new ProductDto
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                SupplierId = product.SupplierId,
                CategoryId = product.CategoryId,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                UnitsOnOrder = product.UnitsOnOrder,
                ReorderLevel = product.ReorderLevel,
                Discontinued = product.Discontinued,
                CategoryName = product.Category.CategoryName,
                CategoryDescription = product.Category.Description,
            };
    }
}
