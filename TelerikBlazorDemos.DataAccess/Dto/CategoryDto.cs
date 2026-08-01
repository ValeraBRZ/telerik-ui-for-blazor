using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TelerikBlazorDemos.DataAccess.Models;

namespace TelerikBlazorDemos.DataAccess.Dto
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }

        [Required]
        [StringLength(15)]
        public string CategoryName { get; set; }

        public string Description { get; set; }

        [StringLength(150)]
        public string PictureUrl { get; set; }

        public List<ProductDto> Products { get; set; }

        public static Func<Category, CategoryDto> CategoryFunc = (category) =>
            new CategoryDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Description = category.Description,
                PictureUrl = category.PictureUrl,
                Products = category.Products.Select(ProductDto.ProductFunc).ToList(),
            };
    }
}
