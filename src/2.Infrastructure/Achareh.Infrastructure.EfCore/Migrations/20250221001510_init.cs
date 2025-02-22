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
                    ReviewId = table.Column<int>(type: "int", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    HomeServiceId = table.Column<int>(type: "int", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_Images_Requests_RequestId",
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
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "تمیزکاری" },
                    { 2, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "ساختمان" },
                    { 3, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "تعمیرات اشیا" },
                    { 4, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "اسباب کشی و حمل بار" },
                    { 5, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "خودرو" },
                    { 6, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "سلامت و زیبایی" },
                    { 7, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "سازمان ها و مجتمع ها" },
                    { 8, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "سایر" }
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
                    { 1, 0, "Tehran, Iran", 1, "85649339-61d0-4e5c-bac8-573a800c1722", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "ela@gmail.com", false, "Ela", null, 50000.0, "Sdq", false, null, "ELA@GMAIL.COM", "ELA", "AQAAAAIAAYagAAAAEI8DUUI8rWHOe/PHVQ8OXBolY70qZVMGLghxbEZN6O4LdbwONjpRxEv/m27Aw7Y/ZQ==", "093689162292", false, "Guid.NewGuid().ToString()", false, "ela" },
                    { 2, 0, "Tehran, Iran", 1, "63f095c1-7514-46bd-8047-c3aa9e849639", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "sara@gmail.com", false, "Sara", null, 5000.0, "Sdq", false, null, "SARA@GMAIL.COM", "SARA", "AQAAAAIAAYagAAAAEPn9mUZ9/WBHY2JFnxhelsQqgm0RwQb2OWpzGNKOIaW3K7iN8dWkmUidvrhm5Yc/Pw==", "09124361938", false, null, false, "sara" },
                    { 3, 0, "Tehran, Iran", 1, "9324da09-77c4-4454-a600-bf913d01753b", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "amir@gmail.com", false, "Amir", null, 5000.0, "Sdq", false, null, "AMIR@GMAIL.COM", "AMIR", "AQAAAAIAAYagAAAAEDp3VLrpsBzWgavv1VgSusFS0uyWZ4X56aOhmMfdbOuLKkiApOCEHbuociSS1WdtJw==", "09016308704", false, null, false, "amir" }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "ImagePath", "IsDeleted", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نظافت و پذیرایی" },
                    { 2, 1, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "شستشو" },
                    { 3, 1, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "کارواش و دیتیلینگ" },
                    { 4, 2, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "سرمایش و گرمایش" },
                    { 5, 2, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعمیرات ساختمان" },
                    { 6, 2, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "لوله کشی" },
                    { 7, 2, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "طراحی و بازسازی ساختمان" },
                    { 8, 2, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "باغبانی و فضای سبز" },
                    { 9, 2, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "چوب و کابینت" },
                    { 10, 3, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب و تعمیر لوازم خانگی" },
                    { 11, 3, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "خدمات کامپیوتری" },
                    { 12, 3, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعمیرات موبایل" },
                    { 13, 4, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "باربری و جابجایی" },
                    { 14, 5, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "خدمات و تعمیرات خودرو" },
                    { 15, 6, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "زیبایی بانوان" },
                    { 16, 6, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "پزشکی و پرستاری" },
                    { 17, 6, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "حیوانات خانگی" },
                    { 18, 6, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تندرستی و ورزش" },
                    { 19, 7, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "خدمات شرکتی" },
                    { 20, 7, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تامین نیروی انسانی" },
                    { 21, 8, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "خیاطی و تعمیرات لباس" },
                    { 22, 8, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "مجالس و رویدادها" },
                    { 23, 8, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "آموزش" },
                    { 24, 8, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "کودک" }
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
                    { 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "UserId" },
                values: new object[] { 1, 2 });

            migrationBuilder.InsertData(
                table: "Experts",
                columns: new[] { "Id", "UserId" },
                values: new object[] { 1, 3 });

            migrationBuilder.InsertData(
                table: "HomeServices",
                columns: new[] { "Id", "CreatedAt", "Description", "ImagePath", "IsDeleted", "Price", "SubCategoryId", "Title", "VisitCount" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 1, "خدمات نظافت منزل", 210 },
                    { 2, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 1, "نظافت راه پله", 210 },
                    { 3, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 2, "قالیشویی", 210 },
                    { 4, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 2, "پرده شویی", 210 },
                    { 5, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 3, "سرامیک خودرو", 210 },
                    { 6, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 3, "صفرشویی خودرو", 210 },
                    { 7, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 4, "تعمیر و سرویس کولر آبی", 210 },
                    { 8, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 4, "کانال سازی کولر", 210 },
                    { 9, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 4, "تعمیر و نگهداری موتورخانه", 210 },
                    { 10, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 5, "سنگ کاری", 210 },
                    { 11, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 5, "بنایی", 210 },
                    { 12, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 5, "کلیدسازی", 210 },
                    { 13, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 5, "کفسابی", 210 },
                    { 14, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 6, "خدمات لوله کشی ساختمان", 210 },
                    { 15, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 6, "تخلیه چاه و لوله بازکنی", 210 },
                    { 16, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 6, "لوله کشی آب و فاضلاب", 210 },
                    { 17, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 7, "مشاوره و بازسازی ساختمان", 210 },
                    { 18, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 7, "دکوراسیون و طراحی ساختمان", 210 },
                    { 19, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 8, "خدمات باغبانی", 210 },
                    { 20, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 8, "کاشت و تعویض گلدان", 210 },
                    { 21, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 9, "تعمیرات مبلمان", 210 },
                    { 22, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 9, "تعمیرات مبلمان اداری", 210 },
                    { 23, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 10, "تعمیر پنکه", 210 },
                    { 24, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 10, "نصب و تعمیر فر", 210 },
                    { 25, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 11, "تعمیر کامپیوتر و لپ تاپ", 210 },
                    { 26, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 11, "مودم و اینترنت", 210 },
                    { 27, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 12, "خدمات تعمیر موبایل", 210 },
                    { 28, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 12, "خدمات خرید موبایل و کالاهای دیجیتال", 210 },
                    { 29, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 12, "خدمات دوربین", 210 },
                    { 30, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 13, "اسباب کشی با خاور و کامیون", 210 },
                    { 31, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 13, "اسباب کشی با وانت و نیسان", 210 },
                    { 32, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 13, "کارگر جابه جایی", 210 },
                    { 33, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 14, "تعویض باتری خودرو", 210 },
                    { 34, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 14, "باتری به باتری", 210 },
                    { 35, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 14, "حمل خودرو", 210 },
                    { 36, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 14, "تعویض وایر و شمع خودرو", 210 },
                    { 37, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 15, "براشینگ موی بانوان", 210 },
                    { 38, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 15, "کوتاهی موی بانوان", 210 },
                    { 39, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 15, "بافت موی بانوان در خانه", 210 },
                    { 40, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 16, "مراقبت و نگهداری", 210 },
                    { 41, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 16, "معاینه پزشکی", 210 },
                    { 42, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 16, "پیراپزشکی", 210 },
                    { 43, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 17, "پت شاپ", 210 },
                    { 44, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 17, "خدمات دامپزشکی در محل", 210 },
                    { 45, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 18, "کلاس یوگا در خانه", 210 },
                    { 46, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 18, "کلاس پیلاتس در خانه", 210 },
                    { 47, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 19, "پیشنهاد فروش خدمات آچاره به شرکت ها", 210 },
                    { 48, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 20, "استخدام نیروی خدمتکار", 210 },
                    { 49, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 21, "تعمیرات لباس", 210 },
                    { 50, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 21, "دوخت لباس زنانه", 210 },
                    { 51, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 21, "تعمیر کیف و کفش", 210 },
                    { 52, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 22, "کیک و شیرینی", 210 },
                    { 53, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 22, "دکور تولد", 210 },
                    { 54, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 22, "گل آرایی", 210 },
                    { 55, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 22, "فینگرفود", 210 },
                    { 56, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 23, "آموزش زبان های خارجی", 210 },
                    { 57, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 23, "آموزش ابتدایی تا متوسطه", 210 },
                    { 58, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 24, "کوتاهی موی کودک", 210 },
                    { 59, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, false, 2000, 24, "طراحی و دیزاین اتاق کودک", 210 }
                });

            migrationBuilder.InsertData(
                table: "ExpertHomeService",
                columns: new[] { "ExpertsId", "HomeServicesId" },
                values: new object[,]
                {
                    { 1, 6 },
                    { 1, 8 }
                });

            migrationBuilder.InsertData(
                table: "Requests",
                columns: new[] { "Id", "ApprovedAt", "CityId", "CreatedAt", "CustomerId", "Description", "HomeServiceId", "IsDeleted", "RequestForTime", "RequestStatus", "ReviewId", "Title" },
                values: new object[] { 2, null, 1, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "کودک ", 59, false, new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, "طراحی اتاق کودک" });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Comment", "CreatedAt", "CustomerId", "ExpertId", "IsAccept", "IsDeleted", "Rating", "Title" },
                values: new object[] { 1, "از این کار راضی بودم", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, false, false, 4, "عالی" });

            migrationBuilder.InsertData(
                table: "ExpertOffers",
                columns: new[] { "Id", "CreatedAt", "Description", "ExpertId", "IsDeleted", "OfferDate", "OfferStatusEnum", "RequestId", "SuggestedPrice" },
                values: new object[] { 2, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "پیشنهاد برای تعمیر کولر", 1, false, new DateTime(2025, 2, 22, 16, 15, 9, 672, DateTimeKind.Local).AddTicks(5980), 1, 2, 750000 });

            migrationBuilder.InsertData(
                table: "Requests",
                columns: new[] { "Id", "ApprovedAt", "CityId", "CreatedAt", "CustomerId", "Description", "HomeServiceId", "IsDeleted", "RequestForTime", "RequestStatus", "ReviewId", "Title" },
                values: new object[] { 1, null, 1, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, " تمیزکاری خانه", 1, false, new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 1, "نظافت" });

            migrationBuilder.InsertData(
                table: "ExpertOffers",
                columns: new[] { "Id", "CreatedAt", "Description", "ExpertId", "IsDeleted", "OfferDate", "OfferStatusEnum", "RequestId", "SuggestedPrice" },
                values: new object[] { 1, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "من این خانه را به خوبی نظافت می کنم", 1, false, new DateTime(2025, 2, 23, 16, 15, 9, 672, DateTimeKind.Local).AddTicks(5969), 4, 1, 500000 });

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
                name: "IX_Images_RequestId",
                table: "Images",
                column: "RequestId");

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
