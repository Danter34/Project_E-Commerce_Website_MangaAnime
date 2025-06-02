using Atsumaru.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Atsumaru.Data
{
    public class AtsumaruContextDB : DbContext
    {
        public AtsumaruContextDB(DbContextOptions<AtsumaruContextDB> options) : base(options)
        {
        }

        public DbSet<category> categories { get; set; }
        public DbSet<type> types { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.categoryId);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.type)
                .WithMany(t => t.Products)
                .HasForeignKey(p => p.typeId);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<category>().HasData(
                new category
                {
                    categoryId = 11,
                    categoryname = "Shounen",
                },
                new category
                {
                    categoryId = 21,
                    categoryname = "Seinen",
                },
                new category
                {
                    categoryId = 31,
                    categoryname = "Kodomo",
                },
                new category
                {
                    categoryId = 41,
                    categoryname = "Shoujo",
                }
                );
            modelBuilder.Entity<type>().HasData(
                new type
                {
                    typeId = 12,
                    typename = "Manga",
                },
                new type
                {
                    typeId = 22,
                    typename = "Manhua",
                },
                new type
                {
                    typeId = 32,
                    typename = "Manhwa"
                }
                );
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "ONE PIECE",
                    tag = "action • adventure • Manga • drama • fantasy • shounen",
                    Detail = "Khi còn nhỏ, Monkey D. Luffy đã được truyền cảm hứng để trở thành một tên cướp biển khi nghe những câu chuyện của\r\ntên cướp biển \"Tóc đỏ\" Shanks. Nhưng cuộc đời của cậu đã thay đổi khi Luffy vô tình ăn phải Trái ác quỷ Gum-Gum và có được sức mạnh co giãn như cao su... với cái giá phải trả là không bao giờ có thể\r\nbơi được nữa! Nhiều năm sau, vẫn thề sẽ trở thành vua của những tên cướp biển, Luffy bắt đầu\r\ncuộc phiêu lưu của mình... một mình trên một chiếc thuyền chèo, để tìm kiếm \"One Piece\" huyền thoại, được cho là\r\nkho báu vĩ đại nhất trên thế giới...",
                    Price = 100,
                    categoryId = 11,
                    typeId = 12,
                    Banner = true,
                    Hotupdate = false,
                    top10 = false,
                    Newproduct = false,
                    Lastupdate = true,
                    Image = "https://image.lag.vn/upload/news/22/06/27/one-piece-wano-arc_EQCU.jpg",
                    Image1 = "https://de7i3bh7bgh0d.cloudfront.net/drupal/field/image/onepiece_61_C4_CC_splash.jpg",
                    Image2 = "https://static0.gamerantimages.com/wordpress/wp-content/uploads/2025/02/gunko-devil-fruit-shamrock-one-piece-1139.png",
                    Image3 = "https://pbs.twimg.com/media/GfusMUTWcAAg3Xu?format=jpg&name=large",
                    Image4 = "https://static1.cbrimages.com/wordpress/wp-content/uploads/2024/10/one-piece-chapter-1130-with-brogy-and-dorry-having-bounties.jpg",
                    Image5 = "https://www.dexerto.com/cdn-image/wp-content/uploads/2024/07/23/one-piece-saturn.jpeg?width=1200&quality=75&format=auto",
                },
                new Product
                {
                    Id = 2,
                    Name = "SOLO LEVELING",
                    tag = "action • adventure • Manhwua • drama • fantasy • shounen",
                    Detail = "Solo Leveling, còn được gọi là Only I Level Up, là một tiểu thuyết web của Hàn Quốc. Truyện có sự góp mặt của Sung \r\n                Jinwoo điều hướng một thế giới bị đe dọa bởi quái vật và cái ác. Một số người mô tả truyện như một cây cầu kết nối \r\n                người hâm mộ manga/anime với thế giới Manhwa. Đây là một câu chuyện viễn tưởng về cánh cổng với sự nhấn mạnh vào hành động và tiến trình. \r\n                Khái niệm cốt lõi xoay quanh hành trình phát triển và sức mạnh của nhân vật chính trong một hệ thống \r\n                lên cấp...",
                    Price = 100,
                    categoryId = 11,
                    typeId = 32,
                    Banner = true,
                    Hotupdate = false,
                    top10 = false,
                    Newproduct = true,
                    Lastupdate = false,
                    Image = "https://homepage.momocdn.net/blogscontents/momo-amazone-s3-api-250109080059-638720064591930063.jpg",
                    Image1 = "https://sm.ign.com/t/ign_ap/screenshot/default/solo-leveling_vd6m.1280.jpg",
                    Image2 = "https://miro.medium.com/v2/resize:fit:1400/0*eZ-Fy51fr0EiCIUB",
                    Image3 = "https://m.media-amazon.com/images/I/51qK3mESJ6L._AC_UF1000,1000_QL80_.jpg",
                    Image4 = "https://m.media-amazon.com/images/I/51rXu-qVhoL._AC_UF1000,1000_QL80_DpWeblab_.jpg",
                    Image5 = "https://i.ebayimg.com/images/g/1UMAAOSwedxmrQdk/s-l400.jpg",
                },
                new Product
                {
                    Id = 3,
                    Name = "VANGABOND",
                    tag = "action • adventure • samurai • Manga • 18+ • seinen",
                    Detail = "Kẻ lang thang chủ yếu là người không có nhà cố định, đi từ nơi này đến nơi khác. Lối sống này thường bao gồm việc khám phá, dành thời gian ở ngoài trời và có thể bao gồm cả việc đi du lịch. Thuật ngữ này cũng có thể mô tả một người sống cuộc sống tạm bợ, có khả năng không có việc làm và thường được liên tưởng đến người lang thang hoặc lang thang. Lối sống của kẻ lang thang nhấn mạnh đến sự tự do di chuyển và tách biệt khỏi các cấu trúc xã hội truyền thống. Đó là một lối sống được xác định bởi tính di động và độc lập, thường liên quan đến sự kết nối với thiên nhiên và sự tách biệt khỏi các sắp xếp cuộc sống thông thường....\r\n",
                    Price = 100,
                    categoryId = 21,
                    typeId = 12,
                    Banner = true,
                    Hotupdate = false,
                    top10 = true,
                    Newproduct = false,
                    Lastupdate = true,
                    Image = "https://wallpapersok.com/images/hd/vagabond-wild-hair-2n8samzjxhwtcg6a.jpg",
                    Image1 = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQIOpew6EBglN5QMJcsRIWh5AC575kbUsy6-A&s",
                    Image2 = "https://i0.wp.com/aiptcomics.com/wp-content/uploads/2025/03/VAGABOND-PREVIEW-2.jpg?fit=740%2C555&ssl=1",
                    Image3 = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQLKWnMUPRwh2nxSMTI2AGm8ZXroD4x84-ZPQ&s",
                    Image4 = "https://gaijinexplorer.wordpress.com/wp-content/uploads/2014/03/vagabond3.jpg",
                    Image5 = "https://static.toiimg.com/thumb/imgsize-23456,msid-120777020,width-600,resizemode-4/a-samurais-journey-like-no-other-credits-viz.jpg",
                },
                new Product
                {
                    Id = 4,
                    Name = "BERSERK",
                    tag = "action • adventure • Dark fantasy • Manga • 18+ • seinen",
                    Detail = "Berserk là một bộ truyện tranh và phim hoạt hình giả tưởng đen tối do Kentaro Miura sáng tác. Bộ truyện kể về câu chuyện của \r\nGuts, một lính đánh thuê đơn độc có quá khứ bi thảm, người sử dụng một thanh kiếm lớn và chiến đấu với quỷ dữ và những kẻ thù khác trong một thế giới thời trung cổ tàn khốc. Chủ đề về số phận, sự trả thù và cuộc đấu tranh chống lại những tỷ lệ cược áp đảo \r\nlà trọng tâm của câu chuyện. Bộ truyện được biết đến với tác phẩm nghệ thuật phức tạp, sự phát triển nhân vật sâu sắc,\r\nvà khám phá bản chất con người, thường đi sâu vào chủ đề về chấn thương và hậu quả của tham vọng...",
                    Price = 100,
                    categoryId = 21,
                    typeId = 12,
                    Banner = true,
                    Hotupdate = true,
                    top10 = false,
                    Newproduct = true,
                    Lastupdate = false,
                    Image = "https://i.ytimg.com/vi/FcoCVGv0DwE/maxresdefault.jpg",
                    Image1 = "https://static1.srcdn.com/wordpress/wp-content/uploads/2024/06/berserk-guts-introspection.jpg",
                    Image2 = "https://preview.redd.it/just-got-berserk-deluxe-vol-1-my-favorite-panel-v0-rpmqh1aucz9a1.jpg?width=640&crop=smart&auto=webp&s=13f4deb46e5a07ab07596e8a8bdd148d2e408a20",
                    Image3 = "https://m.media-amazon.com/images/I/610ilX1GN1L._AC_UF1000,1000_QL80_.jpg",
                    Image4 = "https://img.asmedia.epimg.net/resizer/v2/EF3C24GVEFCETHAGNREH67BURQ.jpg?auth=e4d087a1f0b77f23fe244b0018fd7910826649177a2e11a203ac6821c5b5cc44&width=1472&height=828&smart=true",
                    Image5 = "https://c.files.bbci.co.uk/18D6/production/_118585360_gettyimages-1131401035.jpg",
                },
                new Product
                {
                    Id = 5,
                    Name = "THE DEVIL BUTLER",
                    tag = "action • adventure • comedy • drama • fantasy • shounen",
                    Detail = "Bộ phim kể lại câu chuyện có thật và đáng kinh ngạc về bạo lực vô nhân đạo và hành hạ dã man đối với một thanh niên vô gia cư bị các thành viên của câu lạc bộ xe máy Satan's Angels ở Vancouver giam giữ như một \"quản gia\", và kể chi tiết về vụ bắt giữ, xét xử và kết án những kẻ hành hạ anh ta...",
                    Price = 100,
                    categoryId = 41,
                    typeId = 22,
                    Banner = true,
                    Hotupdate = false,
                    top10 = true,
                    Newproduct = false,
                    Lastupdate = false,
                    Image = "https://media.comikey.com/gazo/600/jpg/comics/AkaBDZ/9cfb4631927d.png",
                    Image1 = "https://imgg.mangaina.com/f34f3de3a846fe31083d8556e0e10281.png",
                    Image2 = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS3L6J_eNV180kEcB3B3LzsMGNevQ2Mla1udg&s",
                    Image3 = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcREW1EihhKdw4p7y57IauVgB5VnIm_WExe3zA&s",
                    Image4 = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRhqJb7qbaA37U_VT9wGNtuhTLdJGZ_A52UaA&s",
                    Image5 = "https://vekinnotebook.wordpress.com/wp-content/uploads/2010/07/cielsebastien.jpg?w=788",
                },
                new Product
                {
                    Id = 6,
                    Name = "Omniscient Reader's Viewpoint",
                    tag = "action • adventure • Manhwa • drama • fantasy • shounen",
                    Detail = "Cuối cùng, Omniscient Reader's Viewpoint cũng chủ yếu nói về cách độc giả thổi hồn vào những câu chuyện. Bộ truyện khám phá mối quan hệ giữa tác giả, độc giả và chính câu chuyện. Khoảnh khắc độc giả yêu thích một câu chuyện cũng là lúc câu chuyện đó sống động, thở và có tiềm năng trở nên bất tử...\r\n",
                    Price = 100,
                    categoryId = 41,
                    typeId = 22,
                    Banner = true,
                    Hotupdate = true,
                    top10 = false,
                    Newproduct = false,
                    Lastupdate = false,
                    Image = "https://scontent.fsgn7-2.fna.fbcdn.net/v/t1.6435-9/202375629_849549325672557_6935373389281706032_n.jpg?_nc_cat=103&ccb=1-7&_nc_sid=2285d6&_nc_ohc=rglyFLhcd9wQ7kNvwHmUPTJ&_nc_oc=AdnYU6LJzAZayjHuicnysM-DB4yiTYXt6Qu-b2jg8jIlmjlwy22qOYELB6wGDMiSUhCaCH2LrMdHXBMUKgUZcCnh&_nc_zt=23&_nc_ht=scontent.fsgn7-2.fna&_nc_gid=52YlLLFcgzm_StFomGkplA&oh=00_AfLtPto5CHvtWINE9BtuYwBHPs9sG_lHha2IVCn7Pdn7cA&oe=685E67D9",
                    Image1 = "https://static0.gamerantimages.com/wordpress/wp-content/uploads/2024/07/omniscient-reader-s-viewpoint-key-visual-7-july-cropped.jpg",
                    Image2 = "https://m.media-amazon.com/images/S/aplus-media-library-service-media/efc6beec-fab1-471a-8128-05762f6c885d.__CR0,0,2048,2048_PT0_SX300_V1___.jpg",
                    Image3 = "https://miro.medium.com/v2/resize:fit:1400/1*TPp_2YMbq46VreENZ2b8Gg.png",
                    Image4 = "https://pbs.twimg.com/media/Fz_z4dnXoAEEjqq?format=jpg&name=4096x4096",
                    Image5 = "https://miro.medium.com/v2/resize:fit:1400/1*JQkJ4McywYY3FZKvFhIYyg.png",
                },
                new Product
                {
                    Id = 7,
                    Name = "ONE PUNCH MAN",
                    tag = "action • adventure • comedy • manga • fantasy • shounen",
                    Detail = "Câu chuyện về Saitama, một anh hùng làm mọi thứ chỉ để vui và có thể đánh bại kẻ thù chỉ bằng một cú đấm. Trong thế giới siêu anh hùng, Saitama là một anh hùng độc đáo, anh có thể đánh bại kẻ thù chỉ bằng một cú đấm....\r\n",
                    Price = 100,
                    categoryId = 11,
                    typeId = 12,
                    Banner = true,
                    Hotupdate = false,
                    top10 = false,
                    Newproduct = false,
                    Lastupdate = true,
                    Image = "https://genk.mediacdn.vn/139269124445442048/2020/4/22/1-15875299157179953930.jpg",
                    Image1 = "https://i0.wp.com/www.comicbookrevolution.com/wp-content/uploads/2022/12/One-Punch-Man-174-1.jpg",
                    Image2 = "https://fanfiaddict.com/wp-content/uploads/2021/08/1_image-8.png",
                    Image3 = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTBYRso17dEvtBjl_InpBsz_mGY3RB0BcoE_A&s",
                    Image4 = "https://bigcomicpage.com/wp-content/uploads/2016/08/opm01.jpg?w=699&h=466",
                    Image5 = "https://64.media.tumblr.com/a7126b82dc456e995060f098562d9c91/03d62f72253da1fa-c8/s1280x1920/260853ea7740ce3b35b7bae2709eb17e071c953c.png",
                },
                new Product
                {
                    Id = 8,
                    Name = "MY DARLING DRESS UP",
                    tag = "emotional• Manga • comedy • drama • fantasy • 16+",
                    Detail = "Tiền đề. Niềm đam mê chế tạo búp bê hina của Wakana Gojo khiến anh phải che giấu sở thích của mình vì chấn thương xã hội. Tuy nhiên, khi người bạn cùng lớp xinh đẹp và nổi tiếng Marin Kitagawa phát hiện ra tài năng của anh, cô ấy nhìn thấu những nét lập dị rõ ràng của anh và khuyến khích anh tạo ra trang phục cosplay...\r\n",
                    Price = 100,
                    categoryId = 31,
                    typeId = 12,
                    Banner = true,
                    Hotupdate = false,
                    top10 = false,
                    Newproduct = true,
                    Lastupdate = false,
                    Image = "https://cdn.oneesports.vn/cdn-data/sites/4/2022/03/anime-my-dress-up-darling-nen-xem.jpg",
                    Image1 = "https://i.ytimg.com/vi/eHWTeyXqnG4/maxresdefault.jpg",
                    Image2 = "https://m.media-amazon.com/images/I/71t0Uf6ytWL._AC_UF894,1000_QL80_.jpg",
                    Image3 = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTIGAZO8A2McE8hxKGXiyCWVBGg8yEdKX1fJQ&s",
                    Image4 = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSwx7qlYA7PsnHQfXHMSsSZxDOf6Yf_JrIuXQ&s",
                    Image5 = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTFJyrcR1MnrTFy1b2rR195hmYOR-Nd6oJAGg&s",
                },
                new Product
                {
                    Id = 9,
                    Name = "JUJUTSU KAISEN",
                    tag = "action • adventure • comedy • manga • fantasy • shounen",
                    Detail = "Yuji Itadori, một thiếu niên tốt bụng, tham gia Câu lạc bộ Huyền bí của trường mình để giải trí, nhưng phát hiện ra rằng các thành viên của câu lạc bộ thực sự là những pháp sư có thể điều khiển năng lượng giữa các sinh vật để phục vụ cho mục đích riêng của họ. Cậu nghe nói về một lá bùa bị nguyền rủa - ngón tay của Sukuna, một con quỷ - và nó bị những sinh vật bị nguyền rủa khác nhắm tới. Yuji ăn ngón tay để bảo vệ bạn bè của mình, và cuối cùng trở thành vật chủ của Sukuna...\r\n",
                    Price = 100,
                    categoryId = 11,
                    typeId = 12,
                    Banner = true,
                    Hotupdate = false,
                    top10 = true,
                    Newproduct = false,
                    Lastupdate = false,
                    Image = "https://hiepsibaotap.com/wp-content/uploads/2023/08/jujutsu-kaisen-4k-fan-art-poster-097r2cgl542i5eez.jpg",
                    Image1 = "https://i.ytimg.com/vi/2pJXDic6Sp8/maxresdefault.jpg",
                    Image2 = "https://i0.wp.com/booksofbrilliance.com/wp-content/uploads/2024/05/Jujutsu-Kaisen-Sukuna-vs-Gojo.png?fit=816%2C459&ssl=1",
                    Image3 = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQTpVbe5o-Wp0Vmz7_yOMcFg-fZ8bkOXHuGEg&s",
                    Image4 = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTly2xajDNHe1hG9u2lBUJgzA5iqgyIHk4yYQ&s",
                    Image5 = "https://us.oricon-group.com/upimg/sns/2000/2877/img1200/Jujutsu-Kaisen-manga-final.jpg",
                },
                new Product
                {
                    Id = 10,
                    Name = "Lout OF COUNT FAMILY",
                    tag = "action • adventure • comedy • manhua • fantasy • shounen",
                    Detail = "Kim Roksu thức dậy vào một buổi sáng và phát hiện ra rằng anh đã chuyển sinh vào một bộ tiểu thuyết mà anh đã đọc năm tập đầu tiên - như một nhân vật phản diện phụ. Roksu hoàn toàn ổn khi là Cale, một kẻ thô lỗ, nhưng anh muốn đảm bảo rằng việc anh bị đánh bại bởi nhân vật chính Choi Han (bản thân anh cũng bị isekai) sẽ không bao giờ xảy ra...\r\n",
                    Price = 100,
                    Banner = true,
                    categoryId = 21,
                    typeId = 32,
                    Hotupdate = true,
                    top10 = false,
                    Newproduct = false,
                    Lastupdate = false,
                    Image = "https://miro.medium.com/v2/resize:fit:1400/1*vAt2X5_qz6gseSZHkWuT5w.jpeg",
                    Image1 = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTmUl0i8vv9DyunDoj_w4IZ4OBHICSy6_425w&s",
                    Image2 = "https://www.novelupdatesforum.com/attachments/eade6de5-6434-4913-ba32-7670f66369b6-jpeg.58599/",
                    Image3 = "https://down-my.img.susercontent.com/file/id-11134207-7r98o-lqgzq124q79s9a",
                    Image4 = "https://image-cdn-ak.spotifycdn.com/image/ab67706c0000da840eb728bead44d16676be2e33",
                    Image5 = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcShS94bfl9lPBnrVBzCvVqm1S98tTK6rqS09A&s",
                }
            );
        }
    }
}
