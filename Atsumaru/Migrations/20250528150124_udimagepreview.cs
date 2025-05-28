using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Atsumaru.Migrations
{
    /// <inheritdoc />
    public partial class udimagepreview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image1",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image2",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image3",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image4",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image5",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Image1", "Image2", "Image3", "Image4", "Image5", "Lastupdate" },
                values: new object[] { "https://de7i3bh7bgh0d.cloudfront.net/drupal/field/image/onepiece_61_C4_CC_splash.jpg", "https://static0.gamerantimages.com/wordpress/wp-content/uploads/2025/02/gunko-devil-fruit-shamrock-one-piece-1139.png", "https://pbs.twimg.com/media/GfusMUTWcAAg3Xu?format=jpg&name=large", "https://static1.cbrimages.com/wordpress/wp-content/uploads/2024/10/one-piece-chapter-1130-with-brogy-and-dorry-having-bounties.jpg", "https://www.dexerto.com/cdn-image/wp-content/uploads/2024/07/23/one-piece-saturn.jpeg?width=1200&quality=75&format=auto", true });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Image1", "Image2", "Image3", "Image4", "Image5", "Newproduct" },
                values: new object[] { "https://sm.ign.com/t/ign_ap/screenshot/default/solo-leveling_vd6m.1280.jpg", "https://miro.medium.com/v2/resize:fit:1400/0*eZ-Fy51fr0EiCIUB", "https://m.media-amazon.com/images/I/51qK3mESJ6L._AC_UF1000,1000_QL80_.jpg", "https://m.media-amazon.com/images/I/51rXu-qVhoL._AC_UF1000,1000_QL80_DpWeblab_.jpg", "https://i.ebayimg.com/images/g/1UMAAOSwedxmrQdk/s-l400.jpg", true });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Image1", "Image2", "Image3", "Image4", "Image5", "Lastupdate", "top10" },
                values: new object[] { "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQIOpew6EBglN5QMJcsRIWh5AC575kbUsy6-A&s", "https://i0.wp.com/aiptcomics.com/wp-content/uploads/2025/03/VAGABOND-PREVIEW-2.jpg?fit=740%2C555&ssl=1", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQLKWnMUPRwh2nxSMTI2AGm8ZXroD4x84-ZPQ&s", "https://gaijinexplorer.wordpress.com/wp-content/uploads/2014/03/vagabond3.jpg", "https://static.toiimg.com/thumb/imgsize-23456,msid-120777020,width-600,resizemode-4/a-samurais-journey-like-no-other-credits-viz.jpg", true, true });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Hotupdate", "Image1", "Image2", "Image3", "Image4", "Image5", "Newproduct" },
                values: new object[] { true, "https://static1.srcdn.com/wordpress/wp-content/uploads/2024/06/berserk-guts-introspection.jpg", "https://preview.redd.it/just-got-berserk-deluxe-vol-1-my-favorite-panel-v0-rpmqh1aucz9a1.jpg?width=640&crop=smart&auto=webp&s=13f4deb46e5a07ab07596e8a8bdd148d2e408a20", "https://m.media-amazon.com/images/I/610ilX1GN1L._AC_UF1000,1000_QL80_.jpg", "https://img.asmedia.epimg.net/resizer/v2/EF3C24GVEFCETHAGNREH67BURQ.jpg?auth=e4d087a1f0b77f23fe244b0018fd7910826649177a2e11a203ac6821c5b5cc44&width=1472&height=828&smart=true", "https://c.files.bbci.co.uk/18D6/production/_118585360_gettyimages-1131401035.jpg", true });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Image1", "Image2", "Image3", "Image4", "Image5", "top10" },
                values: new object[] { "https://imgg.mangaina.com/f34f3de3a846fe31083d8556e0e10281.png", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS3L6J_eNV180kEcB3B3LzsMGNevQ2Mla1udg&s", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcREW1EihhKdw4p7y57IauVgB5VnIm_WExe3zA&s", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRhqJb7qbaA37U_VT9wGNtuhTLdJGZ_A52UaA&s", "https://vekinnotebook.wordpress.com/wp-content/uploads/2010/07/cielsebastien.jpg?w=788", true });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Hotupdate", "Image1", "Image2", "Image3", "Image4", "Image5" },
                values: new object[] { true, "https://static0.gamerantimages.com/wordpress/wp-content/uploads/2024/07/omniscient-reader-s-viewpoint-key-visual-7-july-cropped.jpg", "https://m.media-amazon.com/images/S/aplus-media-library-service-media/efc6beec-fab1-471a-8128-05762f6c885d.__CR0,0,2048,2048_PT0_SX300_V1___.jpg", "https://miro.medium.com/v2/resize:fit:1400/1*TPp_2YMbq46VreENZ2b8Gg.png", "https://pbs.twimg.com/media/Fz_z4dnXoAEEjqq?format=jpg&name=4096x4096", "https://miro.medium.com/v2/resize:fit:1400/1*JQkJ4McywYY3FZKvFhIYyg.png" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Image1", "Image2", "Image3", "Image4", "Image5", "Lastupdate" },
                values: new object[] { "https://i0.wp.com/www.comicbookrevolution.com/wp-content/uploads/2022/12/One-Punch-Man-174-1.jpg", "https://fanfiaddict.com/wp-content/uploads/2021/08/1_image-8.png", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTBYRso17dEvtBjl_InpBsz_mGY3RB0BcoE_A&s", "https://bigcomicpage.com/wp-content/uploads/2016/08/opm01.jpg?w=699&h=466", "https://64.media.tumblr.com/a7126b82dc456e995060f098562d9c91/03d62f72253da1fa-c8/s1280x1920/260853ea7740ce3b35b7bae2709eb17e071c953c.png", true });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Image1", "Image2", "Image3", "Image4", "Image5", "Newproduct" },
                values: new object[] { "https://i.ytimg.com/vi/eHWTeyXqnG4/maxresdefault.jpg", "https://m.media-amazon.com/images/I/71t0Uf6ytWL._AC_UF894,1000_QL80_.jpg", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTIGAZO8A2McE8hxKGXiyCWVBGg8yEdKX1fJQ&s", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSwx7qlYA7PsnHQfXHMSsSZxDOf6Yf_JrIuXQ&s", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTFJyrcR1MnrTFy1b2rR195hmYOR-Nd6oJAGg&s", true });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Image1", "Image2", "Image3", "Image4", "Image5", "top10" },
                values: new object[] { "https://i.ytimg.com/vi/2pJXDic6Sp8/maxresdefault.jpg", "https://i0.wp.com/booksofbrilliance.com/wp-content/uploads/2024/05/Jujutsu-Kaisen-Sukuna-vs-Gojo.png?fit=816%2C459&ssl=1", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQTpVbe5o-Wp0Vmz7_yOMcFg-fZ8bkOXHuGEg&s", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTly2xajDNHe1hG9u2lBUJgzA5iqgyIHk4yYQ&s", "https://us.oricon-group.com/upimg/sns/2000/2877/img1200/Jujutsu-Kaisen-manga-final.jpg", true });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Hotupdate", "Image1", "Image2", "Image3", "Image4", "Image5" },
                values: new object[] { true, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTmUl0i8vv9DyunDoj_w4IZ4OBHICSy6_425w&s", "https://www.novelupdatesforum.com/attachments/eade6de5-6434-4913-ba32-7670f66369b6-jpeg.58599/", "https://down-my.img.susercontent.com/file/id-11134207-7r98o-lqgzq124q79s9a", "https://image-cdn-ak.spotifycdn.com/image/ab67706c0000da840eb728bead44d16676be2e33", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcShS94bfl9lPBnrVBzCvVqm1S98tTK6rqS09A&s" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Image2",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Image3",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Image4",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Image5",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "Lastupdate",
                value: false);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "Newproduct",
                value: false);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Lastupdate", "top10" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Hotupdate", "Newproduct" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "top10",
                value: false);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "Hotupdate",
                value: false);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                column: "Lastupdate",
                value: false);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                column: "Newproduct",
                value: false);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                column: "top10",
                value: false);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                column: "Hotupdate",
                value: false);
        }
    }
}
