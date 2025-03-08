using Achareh.Domain.Core.Entities.Request;
using Achareh.Domain.Core.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Achareh.Infrastructure.EfCore.Configurations
{
    public class UserConfiguration
    {
        public static void SeedUsers(ModelBuilder builder)
        {
            var passwordHasher = new PasswordHasher<User>();

            var users = new List<User>
            {
                new User
                {
                    Id = 1,
                    UserName = "ela",
                    NormalizedUserName = "ELA",
                    Email = "ela@gmail.com",
                    NormalizedEmail = "ELA@GMAIL.COM",
                    FirstName = "Ela",
                    LastName = "Sdq",
                    PhoneNumber = "093689162292",
                    Address = "Tehran, Iran",
                    LockoutEnabled = false,
                    SecurityStamp = "Guid.NewGuid().ToString()",
                    CreatedAt=new DateTime(2025,2,2),
                    Inventory = 50000,
                    CityId = 1,

                },
                //customer
                new User
                {
                    Id = 2,
                    UserName = "sara",
                    NormalizedUserName = "SARA",
                    Email = "sara@gmail.com",
                    NormalizedEmail = "SARA@GMAIL.COM",
                    FirstName = "Sara",
                    LastName = "Sdq",
                    PhoneNumber = "09124361938",
                    Address = "Tehran, Iran",
                    LockoutEnabled = false,
                    SecurityStamp = "Guid.NewGuid().ToString()",
                    Inventory = 5000,
                    CreatedAt=new DateTime(2025,2,2),
                    CityId = 1,

                },

                //expert
                  new User
                {
                    Id = 3,
                    UserName = "amir",
                    NormalizedUserName = "AMIR",
                    Email = "amir@gmail.com",
                    NormalizedEmail = "AMIR@GMAIL.COM",
                    FirstName = "Amir",
                    LastName = "Sdq",
                    PhoneNumber = "09128361939",
                    Address = "Tehran, Iran",
                     LockoutEnabled = false,
                    SecurityStamp = "Guid.NewGuid().ToString()",
                    Inventory = 5000,
                    CreatedAt=new DateTime(2025,2,2),
                    CityId = 1,

                },
                  //expert
                     new User
                {
                    Id = 4,
                    UserName = "leila",
                    NormalizedUserName = "LEILA",
                    Email = "leila@gmail.com",
                    NormalizedEmail = "LEILA@GMAIL.COM",
                    FirstName = "Leila",
                    LastName = "Sdq",
                    PhoneNumber = "09016308704",
                    Address = "Tehran, Iran",
                    LockoutEnabled = false,
                    SecurityStamp = "Guid.NewGuid().ToString()",
                    Inventory = 5000,
                    CreatedAt=new DateTime(2025,2,2),
                    CityId = 1,

                },

                     //Customer

                      new User
                {
                    Id = 5,
                    UserName = "miko",
                    NormalizedUserName = "MIKO",
                    Email = "miko@gmail.com",
                    NormalizedEmail = "MIKO@GMAIL.COM",
                    FirstName = "Miko",
                    LastName = "Sdq",
                    PhoneNumber = "09059073557",
                    Address = "Tehran, Iran",
                    LockoutEnabled = false,
                    SecurityStamp = "Guid.NewGuid().ToString()",
                    Inventory = 5000,
                    CreatedAt=new DateTime(2025,2,2),
                    CityId = 1,

                },
                        //expert
                     new User
                {
                    Id = 6,
                    UserName = "zahra",
                    NormalizedUserName = "ZAHRA",
                    Email = "zahra@gmail.com",
                    NormalizedEmail = "ZAHRA@GMAIL.COM",
                    FirstName = "Zahra",
                    LastName = "Sdq",
                    PhoneNumber = "09388383857",
                    Address = "Tehran, Iran",
                    LockoutEnabled = false,
                    SecurityStamp = "Guid.NewGuid().ToString()",
                    Inventory = 5000,
                    CreatedAt=new DateTime(2025,2,2),
                    CityId = 1,

                },
                     //Customer

                     new User
                {
                    Id = 7,
                    UserName = "amin",
                    NormalizedUserName = "AMIN",
                    Email = "amin@gmail.com",
                    NormalizedEmail = "AMIN@GMAIL.COM",
                    FirstName = "Amin",
                    LastName = "Sdq",
                    PhoneNumber = "09059073557",
                    Address = "Tehran, Iran",
                    LockoutEnabled = false,
                    SecurityStamp = "Guid.NewGuid().ToString()",
                    Inventory = 5000,
                    CreatedAt=new DateTime(2025,2,2),
                    CityId = 1,

                },


            };

            foreach (var user in users)
            {
                user.PasswordHash = passwordHasher.HashPassword(user, "654321");
                builder.Entity<User>().HasData(user);
            }


            builder.Entity<IdentityRole<int>>().HasData
            (
              new IdentityRole<int>() { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
              new IdentityRole<int>() { Id = 2, Name = "Expert", NormalizedName = "EXPERT" },
              new IdentityRole<int>() { Id = 3, Name = "Customer", NormalizedName = "CUSTOMER" }
            );

            builder.Entity<IdentityUserRole<int>>().HasData
            (
              new IdentityUserRole<int>() { RoleId = 1, UserId = 1 },
              new IdentityUserRole<int>() { RoleId = 3, UserId = 2 },
              new IdentityUserRole<int>() { RoleId = 2, UserId = 3 },
              new IdentityUserRole<int>() { RoleId = 2, UserId = 4 },
              new IdentityUserRole<int>() { RoleId = 3, UserId = 5 },
              new IdentityUserRole<int>() { RoleId = 2, UserId = 6 },
              new IdentityUserRole<int>() { RoleId = 3, UserId = 7 }
            );
        }
    }
}
