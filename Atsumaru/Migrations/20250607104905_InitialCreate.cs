using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Atsumaru.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    categoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    categoryname = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.categoryId);
                });

            migrationBuilder.CreateTable(
                name: "Deliverys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeliveryPlaced = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliverys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "types",
                columns: table => new
                {
                    typeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    typename = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_types", x => x.typeId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
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
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Banner = table.Column<bool>(type: "bit", nullable: false),
                    Hotupdate = table.Column<bool>(type: "bit", nullable: false),
                    top10 = table.Column<bool>(type: "bit", nullable: false),
                    Newproduct = table.Column<bool>(type: "bit", nullable: false),
                    Lastupdate = table.Column<bool>(type: "bit", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    categoryId = table.Column<int>(type: "int", nullable: false),
                    typeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_categories_categoryId",
                        column: x => x.categoryId,
                        principalTable: "categories",
                        principalColumn: "categoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_types_typeId",
                        column: x => x.typeId,
                        principalTable: "types",
                        principalColumn: "typeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    Qty = table.Column<int>(type: "int", nullable: false),
                    CartId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DeliveryDetails",
                columns: table => new
                {
                    DeliveryDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    DeliveryId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryDetails", x => x.DeliveryDetailId);
                    table.ForeignKey(
                        name: "FK_DeliveryDetails_Deliverys_DeliveryId",
                        column: x => x.DeliveryId,
                        principalTable: "Deliverys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeliveryDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishlistItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishlistItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishlistItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "categoryId", "categoryname" },
                values: new object[,]
                {
                    { 11, "Shounen" },
                    { 21, "Seinen" },
                    { 31, "Kodomo" },
                    { 41, "Shoujo" }
                });

            migrationBuilder.InsertData(
                table: "types",
                columns: new[] { "typeId", "typename" },
                values: new object[,]
                {
                    { 12, "Manga" },
                    { 22, "Manhua" },
                    { 32, "Manhwa" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Banner", "Detail", "Hotupdate", "Image", "Image1", "Image2", "Image3", "Image4", "Image5", "Lastupdate", "Name", "Newproduct", "Price", "categoryId", "tag", "top10", "typeId" },
                values: new object[,]
                {
                    { 1, true, "Khi còn nhỏ, Monkey D. Luffy đã được truyền cảm hứng để trở thành một tên cướp biển khi nghe những câu chuyện của\r\ntên cướp biển \"Tóc đỏ\" Shanks. Nhưng cuộc đời của cậu đã thay đổi khi Luffy vô tình ăn phải Trái ác quỷ Gum-Gum và có được sức mạnh co giãn như cao su... với cái giá phải trả là không bao giờ có thể\r\nbơi được nữa! Nhiều năm sau, vẫn thề sẽ trở thành vua của những tên cướp biển, Luffy bắt đầu\r\ncuộc phiêu lưu của mình... một mình trên một chiếc thuyền chèo, để tìm kiếm \"One Piece\" huyền thoại, được cho là\r\nkho báu vĩ đại nhất trên thế giới...", false, "https://image.lag.vn/upload/news/22/06/27/one-piece-wano-arc_EQCU.jpg", "https://de7i3bh7bgh0d.cloudfront.net/drupal/field/image/onepiece_61_C4_CC_splash.jpg", "https://static0.gamerantimages.com/wordpress/wp-content/uploads/2025/02/gunko-devil-fruit-shamrock-one-piece-1139.png", "https://pbs.twimg.com/media/GfusMUTWcAAg3Xu?format=jpg&name=large", "https://static1.cbrimages.com/wordpress/wp-content/uploads/2024/10/one-piece-chapter-1130-with-brogy-and-dorry-having-bounties.jpg", "https://www.dexerto.com/cdn-image/wp-content/uploads/2024/07/23/one-piece-saturn.jpeg?width=1200&quality=75&format=auto", true, "ONE PIECE", false, 100m, 11, "action • adventure • Manga • drama • fantasy • shounen", false, 12 },
                    { 2, true, "Solo Leveling, còn được gọi là Only I Level Up, là một tiểu thuyết web của Hàn Quốc. Truyện có sự góp mặt của Sung \r\n                Jinwoo điều hướng một thế giới bị đe dọa bởi quái vật và cái ác. Một số người mô tả truyện như một cây cầu kết nối \r\n                người hâm mộ manga/anime với thế giới Manhwa. Đây là một câu chuyện viễn tưởng về cánh cổng với sự nhấn mạnh vào hành động và tiến trình. \r\n                Khái niệm cốt lõi xoay quanh hành trình phát triển và sức mạnh của nhân vật chính trong một hệ thống \r\n                lên cấp...", false, "https://homepage.momocdn.net/blogscontents/momo-amazone-s3-api-250109080059-638720064591930063.jpg", "https://sm.ign.com/t/ign_ap/screenshot/default/solo-leveling_vd6m.1280.jpg", "https://miro.medium.com/v2/resize:fit:1400/0*eZ-Fy51fr0EiCIUB", "https://m.media-amazon.com/images/I/51qK3mESJ6L._AC_UF1000,1000_QL80_.jpg", "https://m.media-amazon.com/images/I/51rXu-qVhoL._AC_UF1000,1000_QL80_DpWeblab_.jpg", "https://i.ebayimg.com/images/g/1UMAAOSwedxmrQdk/s-l400.jpg", false, "SOLO LEVELING", true, 100m, 11, "action • adventure • Manhwua • drama • fantasy • shounen", false, 32 },
                    { 3, true, "Kẻ lang thang chủ yếu là người không có nhà cố định, đi từ nơi này đến nơi khác. Lối sống này thường bao gồm việc khám phá, dành thời gian ở ngoài trời và có thể bao gồm cả việc đi du lịch. Thuật ngữ này cũng có thể mô tả một người sống cuộc sống tạm bợ, có khả năng không có việc làm và thường được liên tưởng đến người lang thang hoặc lang thang. Lối sống của kẻ lang thang nhấn mạnh đến sự tự do di chuyển và tách biệt khỏi các cấu trúc xã hội truyền thống. Đó là một lối sống được xác định bởi tính di động và độc lập, thường liên quan đến sự kết nối với thiên nhiên và sự tách biệt khỏi các sắp xếp cuộc sống thông thường....\r\n", false, "https://wallpapersok.com/images/hd/vagabond-wild-hair-2n8samzjxhwtcg6a.jpg", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQIOpew6EBglN5QMJcsRIWh5AC575kbUsy6-A&s", "https://i0.wp.com/aiptcomics.com/wp-content/uploads/2025/03/VAGABOND-PREVIEW-2.jpg?fit=740%2C555&ssl=1", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQLKWnMUPRwh2nxSMTI2AGm8ZXroD4x84-ZPQ&s", "https://gaijinexplorer.wordpress.com/wp-content/uploads/2014/03/vagabond3.jpg", "https://static.toiimg.com/thumb/imgsize-23456,msid-120777020,width-600,resizemode-4/a-samurais-journey-like-no-other-credits-viz.jpg", true, "VANGABOND", false, 100m, 21, "action • adventure • samurai • Manga • 18+ • seinen", true, 12 },
                    { 4, true, "Berserk là một bộ truyện tranh và phim hoạt hình giả tưởng đen tối do Kentaro Miura sáng tác. Bộ truyện kể về câu chuyện của \r\nGuts, một lính đánh thuê đơn độc có quá khứ bi thảm, người sử dụng một thanh kiếm lớn và chiến đấu với quỷ dữ và những kẻ thù khác trong một thế giới thời trung cổ tàn khốc. Chủ đề về số phận, sự trả thù và cuộc đấu tranh chống lại những tỷ lệ cược áp đảo \r\nlà trọng tâm của câu chuyện. Bộ truyện được biết đến với tác phẩm nghệ thuật phức tạp, sự phát triển nhân vật sâu sắc,\r\nvà khám phá bản chất con người, thường đi sâu vào chủ đề về chấn thương và hậu quả của tham vọng...", true, "https://i.ytimg.com/vi/FcoCVGv0DwE/maxresdefault.jpg", "https://static1.srcdn.com/wordpress/wp-content/uploads/2024/06/berserk-guts-introspection.jpg", "https://preview.redd.it/just-got-berserk-deluxe-vol-1-my-favorite-panel-v0-rpmqh1aucz9a1.jpg?width=640&crop=smart&auto=webp&s=13f4deb46e5a07ab07596e8a8bdd148d2e408a20", "https://m.media-amazon.com/images/I/610ilX1GN1L._AC_UF1000,1000_QL80_.jpg", "https://img.asmedia.epimg.net/resizer/v2/EF3C24GVEFCETHAGNREH67BURQ.jpg?auth=e4d087a1f0b77f23fe244b0018fd7910826649177a2e11a203ac6821c5b5cc44&width=1472&height=828&smart=true", "https://c.files.bbci.co.uk/18D6/production/_118585360_gettyimages-1131401035.jpg", false, "BERSERK", true, 100m, 21, "action • adventure • Dark fantasy • Manga • 18+ • seinen", false, 12 },
                    { 5, true, "Bộ phim kể lại câu chuyện có thật và đáng kinh ngạc về bạo lực vô nhân đạo và hành hạ dã man đối với một thanh niên vô gia cư bị các thành viên của câu lạc bộ xe máy Satan's Angels ở Vancouver giam giữ như một \"quản gia\", và kể chi tiết về vụ bắt giữ, xét xử và kết án những kẻ hành hạ anh ta...", false, "https://media.comikey.com/gazo/600/jpg/comics/AkaBDZ/9cfb4631927d.png", "https://imgg.mangaina.com/f34f3de3a846fe31083d8556e0e10281.png", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS3L6J_eNV180kEcB3B3LzsMGNevQ2Mla1udg&s", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcREW1EihhKdw4p7y57IauVgB5VnIm_WExe3zA&s", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRhqJb7qbaA37U_VT9wGNtuhTLdJGZ_A52UaA&s", "https://vekinnotebook.wordpress.com/wp-content/uploads/2010/07/cielsebastien.jpg?w=788", false, "THE DEVIL BUTLER", false, 100m, 41, "action • adventure • comedy • drama • fantasy • shounen", true, 22 },
                    { 6, true, "Cuối cùng, Omniscient Reader's Viewpoint cũng chủ yếu nói về cách độc giả thổi hồn vào những câu chuyện. Bộ truyện khám phá mối quan hệ giữa tác giả, độc giả và chính câu chuyện. Khoảnh khắc độc giả yêu thích một câu chuyện cũng là lúc câu chuyện đó sống động, thở và có tiềm năng trở nên bất tử...\r\n", true, "https://scontent.fsgn7-2.fna.fbcdn.net/v/t1.6435-9/202375629_849549325672557_6935373389281706032_n.jpg?_nc_cat=103&ccb=1-7&_nc_sid=2285d6&_nc_ohc=rglyFLhcd9wQ7kNvwHmUPTJ&_nc_oc=AdnYU6LJzAZayjHuicnysM-DB4yiTYXt6Qu-b2jg8jIlmjlwy22qOYELB6wGDMiSUhCaCH2LrMdHXBMUKgUZcCnh&_nc_zt=23&_nc_ht=scontent.fsgn7-2.fna&_nc_gid=52YlLLFcgzm_StFomGkplA&oh=00_AfLtPto5CHvtWINE9BtuYwBHPs9sG_lHha2IVCn7Pdn7cA&oe=685E67D9", "https://static0.gamerantimages.com/wordpress/wp-content/uploads/2024/07/omniscient-reader-s-viewpoint-key-visual-7-july-cropped.jpg", "https://m.media-amazon.com/images/S/aplus-media-library-service-media/efc6beec-fab1-471a-8128-05762f6c885d.__CR0,0,2048,2048_PT0_SX300_V1___.jpg", "https://miro.medium.com/v2/resize:fit:1400/1*TPp_2YMbq46VreENZ2b8Gg.png", "https://pbs.twimg.com/media/Fz_z4dnXoAEEjqq?format=jpg&name=4096x4096", "https://miro.medium.com/v2/resize:fit:1400/1*JQkJ4McywYY3FZKvFhIYyg.png", false, "Omniscient Reader's Viewpoint", false, 100m, 41, "action • adventure • Manhwa • drama • fantasy • shounen", false, 22 },
                    { 7, true, "Câu chuyện về Saitama, một anh hùng làm mọi thứ chỉ để vui và có thể đánh bại kẻ thù chỉ bằng một cú đấm. Trong thế giới siêu anh hùng, Saitama là một anh hùng độc đáo, anh có thể đánh bại kẻ thù chỉ bằng một cú đấm....\r\n", false, "https://genk.mediacdn.vn/139269124445442048/2020/4/22/1-15875299157179953930.jpg", "https://i0.wp.com/www.comicbookrevolution.com/wp-content/uploads/2022/12/One-Punch-Man-174-1.jpg", "https://fanfiaddict.com/wp-content/uploads/2021/08/1_image-8.png", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTBYRso17dEvtBjl_InpBsz_mGY3RB0BcoE_A&s", "https://bigcomicpage.com/wp-content/uploads/2016/08/opm01.jpg?w=699&h=466", "https://64.media.tumblr.com/a7126b82dc456e995060f098562d9c91/03d62f72253da1fa-c8/s1280x1920/260853ea7740ce3b35b7bae2709eb17e071c953c.png", true, "ONE PUNCH MAN", false, 100m, 11, "action • adventure • comedy • manga • fantasy • shounen", false, 12 },
                    { 8, true, "Tiền đề. Niềm đam mê chế tạo búp bê hina của Wakana Gojo khiến anh phải che giấu sở thích của mình vì chấn thương xã hội. Tuy nhiên, khi người bạn cùng lớp xinh đẹp và nổi tiếng Marin Kitagawa phát hiện ra tài năng của anh, cô ấy nhìn thấu những nét lập dị rõ ràng của anh và khuyến khích anh tạo ra trang phục cosplay...\r\n", false, "https://cdn.oneesports.vn/cdn-data/sites/4/2022/03/anime-my-dress-up-darling-nen-xem.jpg", "https://i.ytimg.com/vi/eHWTeyXqnG4/maxresdefault.jpg", "https://m.media-amazon.com/images/I/71t0Uf6ytWL._AC_UF894,1000_QL80_.jpg", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTIGAZO8A2McE8hxKGXiyCWVBGg8yEdKX1fJQ&s", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSwx7qlYA7PsnHQfXHMSsSZxDOf6Yf_JrIuXQ&s", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTFJyrcR1MnrTFy1b2rR195hmYOR-Nd6oJAGg&s", false, "MY DARLING DRESS UP", true, 100m, 31, "emotional• Manga • comedy • drama • fantasy • 16+", false, 12 },
                    { 9, true, "Yuji Itadori, một thiếu niên tốt bụng, tham gia Câu lạc bộ Huyền bí của trường mình để giải trí, nhưng phát hiện ra rằng các thành viên của câu lạc bộ thực sự là những pháp sư có thể điều khiển năng lượng giữa các sinh vật để phục vụ cho mục đích riêng của họ. Cậu nghe nói về một lá bùa bị nguyền rủa - ngón tay của Sukuna, một con quỷ - và nó bị những sinh vật bị nguyền rủa khác nhắm tới. Yuji ăn ngón tay để bảo vệ bạn bè của mình, và cuối cùng trở thành vật chủ của Sukuna...\r\n", false, "https://hiepsibaotap.com/wp-content/uploads/2023/08/jujutsu-kaisen-4k-fan-art-poster-097r2cgl542i5eez.jpg", "https://i.ytimg.com/vi/2pJXDic6Sp8/maxresdefault.jpg", "https://i0.wp.com/booksofbrilliance.com/wp-content/uploads/2024/05/Jujutsu-Kaisen-Sukuna-vs-Gojo.png?fit=816%2C459&ssl=1", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQTpVbe5o-Wp0Vmz7_yOMcFg-fZ8bkOXHuGEg&s", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTly2xajDNHe1hG9u2lBUJgzA5iqgyIHk4yYQ&s", "https://us.oricon-group.com/upimg/sns/2000/2877/img1200/Jujutsu-Kaisen-manga-final.jpg", false, "JUJUTSU KAISEN", false, 100m, 11, "action • adventure • comedy • manga • fantasy • shounen", true, 12 },
                    { 10, true, "Kim Roksu thức dậy vào một buổi sáng và phát hiện ra rằng anh đã chuyển sinh vào một bộ tiểu thuyết mà anh đã đọc năm tập đầu tiên - như một nhân vật phản diện phụ. Roksu hoàn toàn ổn khi là Cale, một kẻ thô lỗ, nhưng anh muốn đảm bảo rằng việc anh bị đánh bại bởi nhân vật chính Choi Han (bản thân anh cũng bị isekai) sẽ không bao giờ xảy ra...\r\n", true, "https://miro.medium.com/v2/resize:fit:1400/1*vAt2X5_qz6gseSZHkWuT5w.jpeg", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTmUl0i8vv9DyunDoj_w4IZ4OBHICSy6_425w&s", "https://www.novelupdatesforum.com/attachments/eade6de5-6434-4913-ba32-7670f66369b6-jpeg.58599/", "https://down-my.img.susercontent.com/file/id-11134207-7r98o-lqgzq124q79s9a", "https://image-cdn-ak.spotifycdn.com/image/ab67706c0000da840eb728bead44d16676be2e33", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcShS94bfl9lPBnrVBzCvVqm1S98tTK6rqS09A&s", false, "Lout OF COUNT FAMILY", false, 100m, 21, "action • adventure • comedy • manhua • fantasy • shounen", false, 32 }
                });

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
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ProductId",
                table: "Carts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryDetails_DeliveryId",
                table: "DeliveryDetails",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryDetails_ProductId",
                table: "DeliveryDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_categoryId",
                table: "Products",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_typeId",
                table: "Products",
                column: "typeId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItems_ProductId",
                table: "WishlistItems",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "Carts");

            migrationBuilder.DropTable(
                name: "DeliveryDetails");

            migrationBuilder.DropTable(
                name: "WishlistItems");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Deliverys");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "types");
        }
    }
}
