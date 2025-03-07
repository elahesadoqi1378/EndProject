using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Achareh.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RequestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Inventory = table.Column<double>(type: "float", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HomeServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SubCategoryId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisitCount = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeServices_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Admins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Experts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpertHomeService",
                columns: table => new
                {
                    ExpertsId = table.Column<int>(type: "int", nullable: false),
                    HomeServicesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertHomeService", x => new { x.ExpertsId, x.HomeServicesId });
                    table.ForeignKey(
                        name: "FK_ExpertHomeService_Experts_ExpertsId",
                        column: x => x.ExpertsId,
                        principalTable: "Experts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpertHomeService_HomeServices_HomeServicesId",
                        column: x => x.HomeServicesId,
                        principalTable: "HomeServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsAccept = table.Column<bool>(type: "bit", nullable: false),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ExpertId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Experts_ExpertId",
                        column: x => x.ExpertId,
                        principalTable: "Experts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestForTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RequestStatus = table.Column<int>(type: "int", nullable: false),
                    WinnerId = table.Column<int>(type: "int", nullable: false),
                    ReviewId = table.Column<int>(type: "int", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    HomeServiceId = table.Column<int>(type: "int", nullable: false),
                    RequestImages = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Requests_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Requests_HomeServices_HomeServiceId",
                        column: x => x.HomeServiceId,
                        principalTable: "HomeServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requests_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExpertOffers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SuggestedPrice = table.Column<int>(type: "int", nullable: false),
                    OfferDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    OfferStatusEnum = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    ExpertId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertOffers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpertOffers_Experts_ExpertId",
                        column: x => x.ExpertId,
                        principalTable: "Experts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExpertOffers_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, null, "Admin", "ADMIN" },
                    { 2, null, "Expert", "EXPERT" },
                    { 3, null, "Customer", "CUSTOMER" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "ImagePath", "IsDeleted", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/category/tamizkari.jpg", false, "تمیزکاری" },
                    { 2, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/category/sakhteman.jpg", false, "ساختمان" },
                    { 3, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/category/tamirat_ashya.jpg", false, "تعمیرات اشیا" },
                    { 4, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/category/asbabkeshi.jpg", false, "اسباب کشی و حمل بار" },
                    { 5, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/category/khodro.jpg", false, "خودرو" },
                    { 6, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/category/salamat_zibayi.jpg", false, "سلامت و زیبایی" },
                    { 7, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/category/sazmanha_va_mojtamha.jpg", false, "سازمان ها و مجتمع ها" },
                    { 8, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/category/sayer.jpg", false, "سایر" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "تهران" },
                    { 2, "مشهد" },
                    { 3, "اصفهان" },
                    { 4, "شیراز" },
                    { 5, "تبریز" },
                    { 6, "کرج" },
                    { 7, "قم" },
                    { 8, "اهواز" },
                    { 9, "اردبیل" },
                    { 10, "کرمانشاه" },
                    { 11, "زاهدان" },
                    { 12, "ارومیه" },
                    { 13, "یزد" },
                    { 14, "همدان" },
                    { 15, "قزوین" },
                    { 16, "سنندج" },
                    { 17, "بندرعباس" },
                    { 18, "زنجان" },
                    { 19, "ساری" },
                    { 20, "بوشهر" },
                    { 21, "مازندران" },
                    { 22, "گرگان" },
                    { 23, "کرمان" },
                    { 24, "خرم آباد" },
                    { 25, "سمنان" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "CityId", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "FirstName", "ImagePath", "Inventory", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "Tehran, Iran", 1, "c5cb3857-4123-4264-a7f8-102237c0460d", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "ela@gmail.com", false, "Ela", null, 50000.0, "Sdq", false, null, "ELA@GMAIL.COM", "ELA", "AQAAAAIAAYagAAAAEOoK7GbPhDIRwRP4dYB6TRTsi5+jaL/qaVwYkrVAhjChOm16mn/S/0I9jXDbPQ9TKQ==", "093689162292", false, "Guid.NewGuid().ToString()", false, "ela" },
                    { 2, 0, "Tehran, Iran", 1, "a5b9aefb-a077-4f1e-addd-c6face77a99b", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "sara@gmail.com", false, "Sara", null, 5000.0, "Sdq", false, null, "SARA@GMAIL.COM", "SARA", "AQAAAAIAAYagAAAAEEnXXUOnUxB8q+6/2JGGbh0CYtEE4Qn5id+2aOisYGD/Xu7YC5JE9G7i7U6oVoff6Q==", "09124361938", false, "Guid.NewGuid().ToString()", false, "sara" },
                    { 3, 0, "Tehran, Iran", 1, "d9f1ace0-5856-4c7c-8e07-ff4e76247ce1", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "amir@gmail.com", false, "Amir", null, 5000.0, "Sdq", false, null, "AMIR@GMAIL.COM", "AMIR", "AQAAAAIAAYagAAAAEISuHnM4W70PesamkU7G+SHHYuPUrCdly7tE3s9+JAfGpX7qmDYStzFxs7sDHKiuHw==", "09128361939", false, "Guid.NewGuid().ToString()", false, "amir" },
                    { 4, 0, "Tehran, Iran", 1, "5d408648-3f55-4f6d-a90f-8c6e11723921", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "leila@gmail.com", false, "Leila", null, 5000.0, "Sdq", false, null, "LEILA@GMAIL.COM", "LEILA", "AQAAAAIAAYagAAAAELejAmyhLPHQYfVXXm2WYEDQaeciAWK6PQlPqySZodXFSTTk7TNj2lJb+7cIG8XPNw==", "09016308704", false, "Guid.NewGuid().ToString()", false, "leila" },
                    { 5, 0, "Tehran, Iran", 1, "94b10ae3-2bc7-46bc-8926-6098a5dfd3a2", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "miko@gmail.com", false, "Miko", null, 5000.0, "Sdq", false, null, "MIKO@GMAIL.COM", "MIKO", "AQAAAAIAAYagAAAAEIloKiT141kT3YPWm5wS/7/OF13C0BPDA+uDCpuhDgN8xaoVAs/KUuSSMHKZdJNj6w==", "09059073557", false, "Guid.NewGuid().ToString()", false, "miko" },
                    { 6, 0, "Tehran, Iran", 1, "f2efd490-c7d0-456b-ac66-6466301a3cb3", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "zahra@gmail.com", false, "Zahra", null, 5000.0, "Sdq", false, null, "ZAHRA@GMAIL.COM", "ZAHRA", "AQAAAAIAAYagAAAAEIAO9BsuKpoxw+zJ+dXt9+HEqWVxGf+I8E2RqjiCljl9Xj2CdBvQMVqeaQMflGO5Fw==", "09388383857", false, "Guid.NewGuid().ToString()", false, "zahra" },
                    { 7, 0, "Tehran, Iran", 1, "795ef9cb-55d2-4cd3-ab8b-c8706389dca0", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "amin@gmail.com", false, "Amin", null, 5000.0, "Sdq", false, null, "AMIN@GMAIL.COM", "AMIN", "AQAAAAIAAYagAAAAEIqncBsCWFt5MR1gttepJVtFOhqhYsDDEOgiZpgXUeHN7ShBhf4irAFWzM58X7sNuA==", "09059073557", false, "Guid.NewGuid().ToString()", false, "amin" }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "ImagePath", "IsDeleted", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/subcategory/nezafat_pazirayi.jpg", false, "نظافت و پذیرایی" },
                    { 2, 1, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/subcategory/shostosho.jpg", false, "شستشو" },
                    { 3, 1, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/subcategory/karvash_detailing", false, "کارواش و دیتیلینگ" },
                    { 4, 2, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/subcategory/sarmayesh_garmayesh", false, "سرمایش و گرمایش" },
                    { 5, 2, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/subcategory/tamirat_sakhteman", false, "تعمیرات ساختمان" },
                    { 6, 2, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/subcategory/lolekeshi", false, "لوله کشی" },
                    { 7, 2, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/subcategory/tarahi_bazsazi.jpg", false, "طراحی و بازسازی ساختمان" },
                    { 8, 2, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/subcategory/baqbani_fazayesabz.jpg", false, "باغبانی و فضای سبز" },
                    { 9, 2, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/subcategory/choob_kabinet.jpg", false, "چوب و کابینت" },
                    { 10, 3, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/subcategory/nasab_tamir_lavazem.jpg", false, "نصب و تعمیر لوازم خانگی" },
                    { 11, 3, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/subcategory/khadamt_cp.jpg", false, "خدمات کامپیوتری" },
                    { 12, 3, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/subcategory/tamirat_mobile.jpg", false, "تعمیرات موبایل" },
                    { 13, 4, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/subcategory/barbari_jabejayi.jpg", false, "باربری و جابجایی" },
                    { 14, 5, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/subcategory/khadamat_khodro.jpg", false, "خدمات و تعمیرات خودرو" },
                    { 15, 6, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/subcategory/zibayi_banovan.jpg", false, "زیبایی بانوان" },
                    { 16, 6, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/subcategory/pezeshki_parastari.jpg", false, "پزشکی و پرستاری" },
                    { 17, 6, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/subcategory/heyvanat_khanegi.jpg", false, "حیوانات خانگی" },
                    { 18, 6, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/subcategory/tandorosti_varzesh.jpg", false, "تندرستی و ورزش" },
                    { 19, 7, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/subcategory/khadamat_sherkati.jpg", false, "خدمات شرکتی" },
                    { 20, 7, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/subcategory/tamin_niroye_ensani.jpg", false, "تامین نیروی انسانی" },
                    { 21, 8, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/subcategory/khayati_tamirat.jpg", false, "خیاطی و تعمیرات لباس" },
                    { 22, 8, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/subcategory/majales_roydad.jpg", false, "مجالس و رویدادها" },
                    { 23, 8, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/subcategory/amozesh.jpg", false, "آموزش" },
                    { 24, 8, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/subcategory/kodak.jpg", false, "کودک" }
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "UserId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 2 },
                    { 2, 4 },
                    { 3, 5 },
                    { 2, 6 },
                    { 3, 7 }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "UserId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 2, 5 },
                    { 3, 7 }
                });

            migrationBuilder.InsertData(
                table: "Experts",
                columns: new[] { "Id", "UserId" },
                values: new object[,]
                {
                    { 1, 3 },
                    { 2, 4 },
                    { 3, 6 }
                });

            migrationBuilder.InsertData(
                table: "HomeServices",
                columns: new[] { "Id", "CreatedAt", "Description", "ImagePath", "IsDeleted", "Price", "SubCategoryId", "Title", "VisitCount" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/nezafat_manzel.jpg", false, 2000, 1, "خدمات نظافت منزل", 210 },
                    { 2, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/nezafat_rahpele.jpg", false, 2000, 1, "نظافت راه پله", 210 },
                    { 3, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/ghalishoyi.jpg", false, 2000, 2, "قالیشویی", 210 },
                    { 4, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/pardeshoyi.jpg", false, 2000, 2, "پرده شویی", 210 },
                    { 5, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/seramik_khodro.jpg", false, 2000, 3, "سرامیک خودرو", 210 },
                    { 6, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/sefrshoyi_khodro.jpg", false, 2000, 3, "صفرشویی خودرو", 210 },
                    { 7, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/tamir_coolerabi.jpg", false, 2000, 4, "تعمیر و سرویس کولر آبی", 210 },
                    { 8, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/kanalsazi_cooler.jpg", false, 2000, 4, "کانال سازی کولر", 210 },
                    { 9, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/tamir_motorkhane.jpg", false, 2000, 4, "تعمیر و نگهداری موتورخانه", 210 },
                    { 10, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/sangkari.jpg", false, 2000, 5, "سنگ کاری", 210 },
                    { 11, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/banayi.jpg", false, 2000, 5, "بنایی", 210 },
                    { 12, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/klidsazi.jpg", false, 2000, 5, "کلیدسازی", 210 },
                    { 13, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/kafsabi.jpg", false, 2000, 5, "کفسابی", 210 },
                    { 14, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/lolekeshi.jpg", false, 2000, 6, "خدمات لوله کشی ساختمان", 210 },
                    { 15, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/lolebazkoni.jpg", false, 2000, 6, "تخلیه چاه و لوله بازکنی", 210 },
                    { 16, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/lolekeshi_fazelab.jpg", false, 2000, 6, "لوله کشی آب و فاضلاب", 210 },
                    { 17, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/moshavere_bazsazi_sakhteman.jpg", false, 2000, 7, "مشاوره و بازسازی ساختمان", 210 },
                    { 18, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/dekorasion_sakhteman.jpg", false, 2000, 7, "دکوراسیون و طراحی ساختمان", 210 },
                    { 19, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/khadamat_baqbani.jpg", false, 2000, 8, "خدمات باغبانی", 210 },
                    { 20, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/kasht_goldan.jpg", false, 2000, 8, "کاشت و تعویض گلدان", 210 },
                    { 21, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/tamirat_mobleman.jpg", false, 2000, 9, "تعمیرات مبلمان", 210 },
                    { 22, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/tamirat_mobleman_edari.jpg", false, 2000, 9, "تعمیرات مبلمان اداری", 210 },
                    { 23, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/tamir_panke.jpg", false, 2000, 10, "تعمیر پنکه", 210 },
                    { 24, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/nasb_va_tamir_fer.jpg", false, 2000, 10, "نصب و تعمیر فر", 210 },
                    { 25, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/tamir_laptop.jpg", false, 2000, 11, "تعمیر کامپیوتر و لپ تاپ", 210 },
                    { 26, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/modem_va_internet.jpg", false, 2000, 11, "مودم و اینترنت", 210 },
                    { 27, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/tamirat_mobile.jpg", false, 2000, 12, "خدمات تعمیر موبایل", 210 },
                    { 28, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/khadamt_kharid_mobile.jpg", false, 2000, 12, "خدمات خرید موبایل و کالاهای دیجیتال", 210 },
                    { 29, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/khadamat_dorbin.jpg", false, 2000, 12, "خدمات دوربین", 210 },
                    { 30, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/asbabkeshi_ba_khavar.jpg", false, 2000, 13, "اسباب کشی با خاور و کامیون", 210 },
                    { 31, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/asabkeshi_ba_neysan.jpg", false, 2000, 13, "اسباب کشی با وانت و نیسان", 210 },
                    { 32, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/kargar_jabejayi.jpg", false, 2000, 13, "کارگر جابه جایی", 210 },
                    { 33, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/taviz_batri_khodro.jpg", false, 2000, 14, "تعویض باتری خودرو", 210 },
                    { 34, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/batri_be_batri.jpg", false, 2000, 14, "باتری به باتری", 210 },
                    { 35, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/haml_khodro.jpg", false, 2000, 14, "حمل خودرو", 210 },
                    { 36, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/taviz_vayer_sham_khodro.jpg", false, 2000, 14, "تعویض وایر و شمع خودرو", 210 },
                    { 37, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/berashing_moye_banovan.jpg", false, 2000, 15, "براشینگ موی بانوان", 210 },
                    { 38, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/kotahi_moye_banovan.jpg", false, 2000, 15, "کوتاهی موی بانوان", 210 },
                    { 39, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/baft_moye_banovan.jpg", false, 2000, 15, "بافت موی بانوان در خانه", 210 },
                    { 40, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/moraqebat_negahdari.jpg", false, 2000, 16, "مراقبت و نگهداری", 210 },
                    { 41, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/moayene_pezeshki.jpg", false, 2000, 16, "معاینه پزشکی", 210 },
                    { 42, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/pirapezeshki.jpg", false, 2000, 16, "پیراپزشکی", 210 },
                    { 43, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/petshop.jpg", false, 2000, 17, "پت شاپ", 210 },
                    { 44, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/khadamat_dampezshki.jpg", false, 2000, 17, "خدمات دامپزشکی در محل", 210 },
                    { 45, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/khadamt_yoga.jpg", false, 2000, 18, "کلاس یوگا در خانه", 210 },
                    { 46, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/kelas_polates.jpg", false, 2000, 18, "کلاس پیلاتس در خانه", 210 },
                    { 47, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/khadamat_achare.jpg", false, 2000, 19, "پیشنهاد فروش خدمات آچاره به شرکت ها", 210 },
                    { 48, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/estekhdam_niroye_khedmatkar.jpg", false, 2000, 20, "استخدام نیروی خدمتکار", 210 },
                    { 49, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/tamirat_lebas.jpg", false, 2000, 21, "تعمیرات لباس", 210 },
                    { 50, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/dokht_lebas_zanane.jpg", false, 2000, 21, "دوخت لباس زنانه", 210 },
                    { 51, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/tamir_kifokafsh.jpg", false, 2000, 21, "تعمیر کیف و کفش", 210 },
                    { 52, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/keyko_shirini.jpg", false, 2000, 22, "کیک و شیرینی", 210 },
                    { 53, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/dekor_tavalod.jpg", false, 2000, 22, "دکور تولد", 210 },
                    { 54, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/gol_arayi.jpg", false, 2000, 22, "گل آرایی", 210 },
                    { 55, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/finger_food.jpg", false, 2000, 22, "فینگرفود", 210 },
                    { 56, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/amozesh_zaban_khareji.jpg", false, 2000, 23, "آموزش زبان های خارجی", 210 },
                    { 57, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/ebtedayi_motevasete.jpg", false, 2000, 23, "آموزش ابتدایی تا متوسطه", 210 },
                    { 58, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/kotahi_moye_kodak.jpg", false, 2000, 24, "کوتاهی موی کودک", 210 },
                    { 59, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " به هزینه سفارش بعد از ساعت 21، مبلغ 150.000 تومان کمک هزینه رفت و آمد افزوده خواهد شد.", "/images/homeservice/tarahi_otaq_kodak.jpg", false, 2000, 24, "طراحی و دیزاین اتاق کودک", 210 }
                });

            migrationBuilder.InsertData(
                table: "ExpertHomeService",
                columns: new[] { "ExpertsId", "HomeServicesId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 59 },
                    { 2, 2 },
                    { 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "Requests",
                columns: new[] { "Id", "ApprovedAt", "CityId", "CreatedAt", "CustomerId", "Description", "HomeServiceId", "IsDeleted", "RequestForTime", "RequestImages", "RequestStatus", "ReviewId", "Title", "WinnerId" },
                values: new object[,]
                {
                    { 2, null, 1, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "کودک ", 59, false, new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 5, null, "طراحی اتاق کودک", 0 },
                    { 3, null, 1, new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "نظافت راه پله ساختمان های اداری ", 2, false, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, null, "نظافت راه پله", 0 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Comment", "CreatedAt", "CustomerId", "ExpertId", "IsAccept", "IsDeleted", "Rating", "RequestId", "Title" },
                values: new object[] { 1, "از این کار راضی بودم", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, false, false, 4, 0, "عالی" });

            migrationBuilder.InsertData(
                table: "ExpertOffers",
                columns: new[] { "Id", "CreatedAt", "Description", "ExpertId", "IsDeleted", "OfferDate", "OfferStatusEnum", "RequestId", "SuggestedPrice" },
                values: new object[,]
                {
                    { 2, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "پیشنهاد برای طراحی اتاق کودک", 1, false, new DateTime(2025, 3, 9, 2, 40, 52, 687, DateTimeKind.Local).AddTicks(3177), 1, 2, 2300 },
                    { 3, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "من این راه پله ها را به خوبی تمیز میکنم با قیمت مناسب", 2, false, new DateTime(2025, 3, 9, 2, 40, 52, 687, DateTimeKind.Local).AddTicks(3181), 2, 3, 2400 },
                    { 4, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " من این راه پله ها را به خوبی تمیز میکنم", 3, false, new DateTime(2025, 3, 12, 2, 40, 52, 687, DateTimeKind.Local).AddTicks(3185), 2, 3, 2000 }
                });

            migrationBuilder.InsertData(
                table: "Requests",
                columns: new[] { "Id", "ApprovedAt", "CityId", "CreatedAt", "CustomerId", "Description", "HomeServiceId", "IsDeleted", "RequestForTime", "RequestImages", "RequestStatus", "ReviewId", "Title", "WinnerId" },
                values: new object[] { 1, null, 1, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, " تمیزکاری خانه", 1, false, new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 5, 1, "نظافت", 0 });

            migrationBuilder.InsertData(
                table: "ExpertOffers",
                columns: new[] { "Id", "CreatedAt", "Description", "ExpertId", "IsDeleted", "OfferDate", "OfferStatusEnum", "RequestId", "SuggestedPrice" },
                values: new object[] { 1, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "من این خانه را به خوبی نظافت می کنم", 1, false, new DateTime(2025, 3, 10, 2, 40, 52, 687, DateTimeKind.Local).AddTicks(3170), 3, 1, 2200 });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_UserId",
                table: "Admins",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CityId",
                table: "AspNetUsers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserId",
                table: "Customers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpertHomeService_HomeServicesId",
                table: "ExpertHomeService",
                column: "HomeServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertOffers_ExpertId",
                table: "ExpertOffers",
                column: "ExpertId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertOffers_RequestId",
                table: "ExpertOffers",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Experts_UserId",
                table: "Experts",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HomeServices_SubCategoryId",
                table: "HomeServices",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_CityId",
                table: "Requests",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_CustomerId",
                table: "Requests",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_HomeServiceId",
                table: "Requests",
                column: "HomeServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ReviewId",
                table: "Requests",
                column: "ReviewId",
                unique: true,
                filter: "[ReviewId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CustomerId",
                table: "Reviews",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ExpertId",
                table: "Reviews",
                column: "ExpertId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_CategoryId",
                table: "SubCategories",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ExpertHomeService");

            migrationBuilder.DropTable(
                name: "ExpertOffers");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "HomeServices");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Experts");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
