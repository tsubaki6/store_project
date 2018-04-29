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
                new CatalogBrand() { Brand = "AGH"}
            };
        }

        static IEnumerable<CatalogType> GetPreconfiguredCatalogTypes()
        {
            return new List<CatalogType>()
            {
				new CatalogType() { Type = "Sweatshirt"},
                new CatalogType() { Type = "T-Shirt" },
				new CatalogType() { Type = "Mug" }
            };
        }

        static IEnumerable<CatalogItem> GetPreconfiguredItems()
        {
            return new List<CatalogItem>()
            {
				new CatalogItem() { CatalogTypeId=1,CatalogBrandId=1, Description = "AGH Black Sweatshirt", Name = "AGH Black Sweatshirt", Price = 19.5M, PictureUri = "http://catalogbaseurltobereplaced/images/products/1.jpeg" },
				new CatalogItem() { CatalogTypeId=1,CatalogBrandId=1, Description = "AGH Grey Sweatshirt", Name = "AGH Grey Sweatshirt", Price= 18.50M, PictureUri = "http://catalogbaseurltobereplaced/images/products/2.jpeg" },
				new CatalogItem() { CatalogTypeId=2,CatalogBrandId=1, Description = "AGH Black T-Shirt", Name = "AGH Black T-Shirt", Price = 12, PictureUri = "http://catalogbaseurltobereplaced/images/products/3.jpeg" },
				new CatalogItem() { CatalogTypeId=2,CatalogBrandId=1, Description = "AGH White T-Shirt", Name = "AGH White T-Shirt", Price = 12, PictureUri = "http://catalogbaseurltobereplaced/images/products/4.jpeg" },
				new CatalogItem() { CatalogTypeId=3,CatalogBrandId=1, Description = "AGH Steel Mug", Name = "AGH Steel Mug", Price = 8.5M, PictureUri = "http://catalogbaseurltobereplaced/images/products/5.jpeg" },
				new CatalogItem() { CatalogTypeId=3,CatalogBrandId=1, Description = "AGH Ceramic Mug", Name = "AGH Ceramic Mug", Price = 10, PictureUri = "http://catalogbaseurltobereplaced/images/products/6.jpeg" },
				new CatalogItem() { CatalogTypeId=3,CatalogBrandId=1, Description = "AGH Hot Mug", Name = "AGH Hot Mug", Price = 13, PictureUri = "http://catalogbaseurltobereplaced/images/products/7.jpeg"  }
            };
        }
    }
}
