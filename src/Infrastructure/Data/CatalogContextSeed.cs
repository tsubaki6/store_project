using Microsoft.AspNetCore.Builder;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class CatalogContextSeed
    {
        public static async Task SeedAsync(CatalogContext catalogContext,
            ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;
            try
            {
                // TODO: Only run this if using a real database
                // context.Database.Migrate();

                if (!catalogContext.CatalogBrands.Any())
                {
                    catalogContext.CatalogBrands.AddRange(
                        GetPreconfiguredCatalogBrands());

                    await catalogContext.SaveChangesAsync();
                }

                if (!catalogContext.CatalogTypes.Any())
                {
                    catalogContext.CatalogTypes.AddRange(
                        GetPreconfiguredCatalogTypes());

                    await catalogContext.SaveChangesAsync();
                }

                if (!catalogContext.CatalogItems.Any())
                {
                    catalogContext.CatalogItems.AddRange(
                        GetPreconfiguredItems());

                    await catalogContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<CatalogContextSeed>();
                    log.LogError(ex.Message);
                    await SeedAsync(catalogContext, loggerFactory, retryForAvailability);
                }
            }
        }

        static IEnumerable<CatalogBrand> GetPreconfiguredCatalogBrands()
        {
            return new List<CatalogBrand>()
            {
                new CatalogBrand() { Brand = "Nike"},
				new CatalogBrand() { Brand = "Adidas" },
                new CatalogBrand() { Brand = "Puma" }
            };
        }

        static IEnumerable<CatalogType> GetPreconfiguredCatalogTypes()
        {
            return new List<CatalogType>()
            {
                new CatalogType() { Type = "Mug"},
                new CatalogType() { Type = "T-Shirt" }
            };
        }

        static IEnumerable<CatalogItem> GetPreconfiguredItems()
        {
            return new List<CatalogItem>()
            {
				new CatalogItem() { CatalogTypeId=2,CatalogBrandId=2, Description = "Nike Bot Black Sweatshirt", Name = "Nike Bot Black Sweatshirt", Price = 19.5M, PictureUri = "http://catalogbaseurltobereplaced/images/products/1.png" },
				new CatalogItem() { CatalogTypeId=1,CatalogBrandId=2, Description = "Adidas Black & White Mug", Name = "Adidas Black & White Mug", Price= 8.50M, PictureUri = "http://catalogbaseurltobereplaced/images/products/2.png" },
				new CatalogItem() { CatalogTypeId=2,CatalogBrandId=5, Description = "Puma White T-Shirt", Name = "Puma White T-Shirt", Price = 12, PictureUri = "http://catalogbaseurltobereplaced/images/products/3.png" },
				new CatalogItem() { CatalogTypeId=2,CatalogBrandId=2, Description = "Nike Foundation Sweatshirt", Name = "Nike Foundation Sweatshirt", Price = 12, PictureUri = "http://catalogbaseurltobereplaced/images/products/4.png" },
				new CatalogItem() { CatalogTypeId=3,CatalogBrandId=5, Description = "Adidas Red Sheet", Name = "Adidas Red Sheet", Price = 8.5M, PictureUri = "http://catalogbaseurltobereplaced/images/products/5.png" },
				new CatalogItem() { CatalogTypeId=2,CatalogBrandId=2, Description = "Puma Blue Sweatshirt", Name = "Puma Blue Sweatshirt", Price = 12, PictureUri = "http://catalogbaseurltobereplaced/images/products/6.png" },
				new CatalogItem() { CatalogTypeId=2,CatalogBrandId=5, Description = "Nike Red T-Shirt", Name = "Nike Red T-Shirt", Price = 12, PictureUri = "http://catalogbaseurltobereplaced/images/products/7.png"  },
				new CatalogItem() { CatalogTypeId=2,CatalogBrandId=5, Description = "Adidas Purple Sweatshirt", Name = "Adidas Purple Sweatshirt", Price = 8.5M, PictureUri = "http://catalogbaseurltobereplaced/images/products/8.png" },
				new CatalogItem() { CatalogTypeId=1,CatalogBrandId=5, Description = "Puma White Mug", Name = "Puma White Mug", Price = 12, PictureUri = "http://catalogbaseurltobereplaced/images/products/9.png" },
				new CatalogItem() { CatalogTypeId=3,CatalogBrandId=2, Description = "Nike Foundation Sheet", Name = "Nike Foundation Sheet", Price = 12, PictureUri = "http://catalogbaseurltobereplaced/images/products/10.png" },
				new CatalogItem() { CatalogTypeId=3,CatalogBrandId=2, Description = "Adidas Sheet", Name = "Adidas Sheet", Price = 8.5M, PictureUri = "http://catalogbaseurltobereplaced/images/products/11.png" },
				new CatalogItem() { CatalogTypeId=2,CatalogBrandId=5, Description = "Puma White TShirt", Name = "Puma White TShirt", Price = 12, PictureUri = "http://catalogbaseurltobereplaced/images/products/12.png" }
            };
        }
    }
}
