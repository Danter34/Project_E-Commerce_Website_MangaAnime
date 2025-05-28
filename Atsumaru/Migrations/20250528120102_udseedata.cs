using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Atsumaru.Migrations
{
    /// <inheritdoc />
    public partial class udseedata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Banner", "Detail", "Hotupdate", "Image", "Lastupdate", "Name", "Newproduct", "Price", "tag", "top10" },
                values: new object[,]
                {
                    { 1, true, "Khi còn nhỏ, Monkey D. Luffy đã được truyền cảm hứng để trở thành một tên cướp biển khi nghe những câu chuyện của\r\ntên cướp biển \"Tóc đỏ\" Shanks. Nhưng cuộc đời của cậu đã thay đổi khi Luffy vô tình ăn phải Trái ác quỷ Gum-Gum và có được sức mạnh co giãn như cao su... với cái giá phải trả là không bao giờ có thể\r\nbơi được nữa! Nhiều năm sau, vẫn thề sẽ trở thành vua của những tên cướp biển, Luffy bắt đầu\r\ncuộc phiêu lưu của mình... một mình trên một chiếc thuyền chèo, để tìm kiếm \"One Piece\" huyền thoại, được cho là\r\nkho báu vĩ đại nhất trên thế giới...", false, "https://image.lag.vn/upload/news/22/06/27/one-piece-wano-arc_EQCU.jpg", false, "ONE PIECE", false, 100m, "action • adventure • Manga • drama • fantasy • shounen", false },
                    { 2, true, "Solo Leveling, còn được gọi là Only I Level Up, là một tiểu thuyết web của Hàn Quốc. Truyện có sự góp mặt của Sung \r\n                Jinwoo điều hướng một thế giới bị đe dọa bởi quái vật và cái ác. Một số người mô tả truyện như một cây cầu kết nối \r\n                người hâm mộ manga/anime với thế giới Manhwa. Đây là một câu chuyện viễn tưởng về cánh cổng với sự nhấn mạnh vào hành động và tiến trình. \r\n                Khái niệm cốt lõi xoay quanh hành trình phát triển và sức mạnh của nhân vật chính trong một hệ thống \r\n                lên cấp...", false, "https://homepage.momocdn.net/blogscontents/momo-amazone-s3-api-250109080059-638720064591930063.jpg", false, "SOLO LEVELING", false, 100m, "action • adventure • Manhwua • drama • fantasy • shounen", false },
                    { 3, true, "Kẻ lang thang chủ yếu là người không có nhà cố định, đi từ nơi này đến nơi khác. Lối sống này thường bao gồm việc khám phá, dành thời gian ở ngoài trời và có thể bao gồm cả việc đi du lịch. Thuật ngữ này cũng có thể mô tả một người sống cuộc sống tạm bợ, có khả năng không có việc làm và thường được liên tưởng đến người lang thang hoặc lang thang. Lối sống của kẻ lang thang nhấn mạnh đến sự tự do di chuyển và tách biệt khỏi các cấu trúc xã hội truyền thống. Đó là một lối sống được xác định bởi tính di động và độc lập, thường liên quan đến sự kết nối với thiên nhiên và sự tách biệt khỏi các sắp xếp cuộc sống thông thường....\r\n", false, "https://wallpapersok.com/images/hd/vagabond-wild-hair-2n8samzjxhwtcg6a.jpg", false, "VANGABOND", false, 100m, "action • adventure • samurai • Manga • 18+ • seinen", false },
                    { 4, true, "Berserk là một bộ truyện tranh và phim hoạt hình giả tưởng đen tối do Kentaro Miura sáng tác. Bộ truyện kể về câu chuyện của \r\nGuts, một lính đánh thuê đơn độc có quá khứ bi thảm, người sử dụng một thanh kiếm lớn và chiến đấu với quỷ dữ và những kẻ thù khác trong một thế giới thời trung cổ tàn khốc. Chủ đề về số phận, sự trả thù và cuộc đấu tranh chống lại những tỷ lệ cược áp đảo \r\nlà trọng tâm của câu chuyện. Bộ truyện được biết đến với tác phẩm nghệ thuật phức tạp, sự phát triển nhân vật sâu sắc,\r\nvà khám phá bản chất con người, thường đi sâu vào chủ đề về chấn thương và hậu quả của tham vọng...", false, "https://i.ytimg.com/vi/FcoCVGv0DwE/maxresdefault.jpg", false, "BERSERK", false, 100m, "action • adventure • Dark fantasy • Manga • 18+ • seinen", false },
                    { 5, true, "Bộ phim kể lại câu chuyện có thật và đáng kinh ngạc về bạo lực vô nhân đạo và hành hạ dã man đối với một thanh niên vô gia cư bị các thành viên của câu lạc bộ xe máy Satan's Angels ở Vancouver giam giữ như một \"quản gia\", và kể chi tiết về vụ bắt giữ, xét xử và kết án những kẻ hành hạ anh ta...", false, "https://media.comikey.com/gazo/600/jpg/comics/AkaBDZ/9cfb4631927d.png", false, "THE DEVIL BUTLER", false, 100m, "action • adventure • comedy • drama • fantasy • shounen", false },
                    { 6, true, "Cuối cùng, Omniscient Reader's Viewpoint cũng chủ yếu nói về cách độc giả thổi hồn vào những câu chuyện. Bộ truyện khám phá mối quan hệ giữa tác giả, độc giả và chính câu chuyện. Khoảnh khắc độc giả yêu thích một câu chuyện cũng là lúc câu chuyện đó sống động, thở và có tiềm năng trở nên bất tử...\r\n", false, "https://scontent.fsgn7-2.fna.fbcdn.net/v/t1.6435-9/202375629_849549325672557_6935373389281706032_n.jpg?_nc_cat=103&ccb=1-7&_nc_sid=2285d6&_nc_ohc=rglyFLhcd9wQ7kNvwHmUPTJ&_nc_oc=AdnYU6LJzAZayjHuicnysM-DB4yiTYXt6Qu-b2jg8jIlmjlwy22qOYELB6wGDMiSUhCaCH2LrMdHXBMUKgUZcCnh&_nc_zt=23&_nc_ht=scontent.fsgn7-2.fna&_nc_gid=52YlLLFcgzm_StFomGkplA&oh=00_AfLtPto5CHvtWINE9BtuYwBHPs9sG_lHha2IVCn7Pdn7cA&oe=685E67D9", false, "Omniscient Reader's Viewpoint", false, 100m, "action • adventure • Manhwa • drama • fantasy • shounen", false },
                    { 7, true, "Câu chuyện về Saitama, một anh hùng làm mọi thứ chỉ để vui và có thể đánh bại kẻ thù chỉ bằng một cú đấm. Trong thế giới siêu anh hùng, Saitama là một anh hùng độc đáo, anh có thể đánh bại kẻ thù chỉ bằng một cú đấm....\r\n", false, "https://genk.mediacdn.vn/139269124445442048/2020/4/22/1-15875299157179953930.jpg", false, "ONE PUNCH MAN", false, 100m, "action • adventure • comedy • manga • fantasy • shounen", false },
                    { 8, true, "Tiền đề. Niềm đam mê chế tạo búp bê hina của Wakana Gojo khiến anh phải che giấu sở thích của mình vì chấn thương xã hội. Tuy nhiên, khi người bạn cùng lớp xinh đẹp và nổi tiếng Marin Kitagawa phát hiện ra tài năng của anh, cô ấy nhìn thấu những nét lập dị rõ ràng của anh và khuyến khích anh tạo ra trang phục cosplay...\r\n", false, "https://cdn.oneesports.vn/cdn-data/sites/4/2022/03/anime-my-dress-up-darling-nen-xem.jpg", false, "MY DARLING DRESS UP", false, 100m, "emotional• Manga • comedy • drama • fantasy • 16+", false },
                    { 9, true, "Yuji Itadori, một thiếu niên tốt bụng, tham gia Câu lạc bộ Huyền bí của trường mình để giải trí, nhưng phát hiện ra rằng các thành viên của câu lạc bộ thực sự là những pháp sư có thể điều khiển năng lượng giữa các sinh vật để phục vụ cho mục đích riêng của họ. Cậu nghe nói về một lá bùa bị nguyền rủa - ngón tay của Sukuna, một con quỷ - và nó bị những sinh vật bị nguyền rủa khác nhắm tới. Yuji ăn ngón tay để bảo vệ bạn bè của mình, và cuối cùng trở thành vật chủ của Sukuna...\r\n", false, "https://hiepsibaotap.com/wp-content/uploads/2023/08/jujutsu-kaisen-4k-fan-art-poster-097r2cgl542i5eez.jpg", false, "JUJUTSU KAISEN", false, 100m, "action • adventure • comedy • manga • fantasy • shounen", false },
                    { 10, true, "Kim Roksu thức dậy vào một buổi sáng và phát hiện ra rằng anh đã chuyển sinh vào một bộ tiểu thuyết mà anh đã đọc năm tập đầu tiên - như một nhân vật phản diện phụ. Roksu hoàn toàn ổn khi là Cale, một kẻ thô lỗ, nhưng anh muốn đảm bảo rằng việc anh bị đánh bại bởi nhân vật chính Choi Han (bản thân anh cũng bị isekai) sẽ không bao giờ xảy ra...\r\n", false, "https://miro.medium.com/v2/resize:fit:1400/1*vAt2X5_qz6gseSZHkWuT5w.jpeg", false, "Lout OF COUNT FAMILY", false, 100m, "action • adventure • comedy • manhua • fantasy • shounen", false }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
