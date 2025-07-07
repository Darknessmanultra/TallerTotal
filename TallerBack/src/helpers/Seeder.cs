using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TallerBack.src.data;
using TallerBack.src.models;

namespace TallerBack.src.helpers
{
    public static class IdentityDataSeeder
    {
        public static async Task SeedAdminAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            var adminName = "Ignacio_Mancilla";
            var adminEmail = "ignacio.mancilla@gmail.com";
            var adminPassword = "Pa$$word2025";

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>("Admin"));
            }
            else{Console.WriteLine("DEATHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH");}

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var faker = new Bogus.Faker("en");
                var phone = faker.Phone.PhoneNumber("9########");
                adminUser = new ApplicationUser
                {
                    Id = Guid.NewGuid(),
                    UserName = adminName,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    PhoneNumber = phone,
                    BirthDate = DateOnly.FromDateTime(new DateTime(1990, 1, 1))
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
                else
                {
                    Console.WriteLine("❌ Admin creation failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
        }
        public static async Task SeedUsersAsync(IServiceProvider serviceProvider, int count = 10)
        {
            using var scope = serviceProvider.CreateScope();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var faker = new Bogus.Faker("en");
            for (int i = 0; i < count; i++)
            {
                var email = faker.Internet.Email();
                var phone = faker.Phone.PhoneNumber("9########");
                var birthdate = faker.Date.Between(new DateTime(1980, 1, 1), new DateTime(2005, 12, 31));

                if (await userManager.FindByEmailAsync(email) != null)
                    continue;

                var user = new ApplicationUser
                {
                    Id = Guid.NewGuid(),
                    UserName = email,
                    Email = email,
                    PhoneNumber = phone,
                    BirthDate = DateOnly.FromDateTime(birthdate),
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(user, "User123*");
            }
        }
    }
    public class DbSeeder
    {
        public static async Task SeedAsync(ApiDbContext context)
        {
            if (context.Products.Any())
                return;

            var random = new Random();
            var products = new List<Product>();

            for (int i = 1; i <= 10; i++)
            {
                var product = new Product
                {
                    Name = $"Product {i}",
                    Description = $"Description for product {i}",
                    Price = 1000 + i * 100,
                    Stock = 5 + i,
                    IsDeleted = false,
                    CreatedAt = DateTime.UtcNow,
                    Condition = i % 2 == 0 ? ProductCondition.New : ProductCondition.Used,
                    Brand=BrandNames[random.Next(BrandNames.Count)],
                    Category=CategoryNames[random.Next(CategoryNames.Count)],
                    Images = new List<ProductImage>
                    {
                        new ProductImage { Url = $"https://res.cloudinary.com/demo/product{i}_1.jpg" },
                        new ProductImage { Url = $"https://res.cloudinary.com/demo/product{i}_2.jpg" }
                    }
                };

                products.Add(product);
            }

            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }
        
        private static readonly List<string> BrandNames = new()
        {
            "Acme", "Globex", "Cabbage Corp", "Holy Nightmare", "Umbrella", "WarioWare"
        };

        private static readonly List<string> CategoryNames = new()
        {
            "Electronicos", "Libros", "Ropa", "Juguetes", "Hogar"
        };
    }


}