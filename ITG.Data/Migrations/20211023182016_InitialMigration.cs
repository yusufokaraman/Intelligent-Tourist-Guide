using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ITG.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Content = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    Thumbnail = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "VARBINARY(500)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PlacePicture = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Places_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Places_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Content = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    Thumbnail = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ViewCount = table.Column<int>(type: "int", nullable: false),
                    CommentCount = table.Column<int>(type: "int", nullable: false),
                    SeoAuthor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SeoDescription = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SeoTags = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    PlaceId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Articles_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Articles_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Articles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    PlaceId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Content", "CreatedByName", "CreatedDate", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Name", "Note", "Thumbnail" },
                values: new object[,]
                {
                    { 1, "Adana, Türkiye'nin bir ili ve en kalabalık altıncı şehridir. 2019 yılı verilerine göre 2.258.718 nüfusa sahiptir. İlin yüz ölçümü 13.844 km²dir. İlde km²ye 160 kişi düşmektedir. 01.02.2018 TÜİK verilerine göre 5'i merkez ilçe olmak üzere toplam 15 ilçesi ve belediyesi vardır. Bu ilçelerde 831 mahalle bulunmaktadır.", "InitialCreate", new DateTime(2021, 10, 23, 21, 20, 16, 440, DateTimeKind.Local).AddTicks(3454), true, false, "InitialCreate", new DateTime(2021, 10, 23, 21, 20, 16, 440, DateTimeKind.Local).AddTicks(3464), "Adana", "01 Plakalı il Adana.", "Default.jpg" },
                    { 2, "Adıyaman, aynı isimli ilin merkez ilçesidir. Adıyaman merkez ilçesinin nüfusu 2020 istatistiklerine 310.644'dür. ", "InitialCreate", new DateTime(2021, 10, 23, 21, 20, 16, 440, DateTimeKind.Local).AddTicks(3475), true, false, "InitialCreate", new DateTime(2021, 10, 23, 21, 20, 16, 440, DateTimeKind.Local).AddTicks(3476), "Adıyaman", "02 Plakalı il Adıyaman.", "Default.jpg" },
                    { 3, "Afyonkarahisar veya eski ve halk arasındaki ismiyle Afyon, aynı isimli ilin merkezidir. Mermercilik ve gıda sektöründe Türkiye içinde ve dışında isim yapmıştır. Şehrin Afyon olan ismi, 2005 yılında Afyonkarahisar olarak değiştirilmiştir. ", "InitialCreate", new DateTime(2021, 10, 23, 21, 20, 16, 440, DateTimeKind.Local).AddTicks(3480), true, false, "InitialCreate", new DateTime(2021, 10, 23, 21, 20, 16, 440, DateTimeKind.Local).AddTicks(3481), "Afyon", "03 Plakalı il Afyon.", "Default.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedByName", "CreatedDate", "Description", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Name", "Note" },
                values: new object[,]
                {
                    { 1, "InitialCreate", new DateTime(2021, 10, 23, 21, 20, 16, 446, DateTimeKind.Local).AddTicks(2455), "The admin role has all the rights.", true, false, "InitialCreate", new DateTime(2021, 10, 23, 21, 20, 16, 446, DateTimeKind.Local).AddTicks(2465), "AdminUser", "This is Administrator." },
                    { 2, "InitialCreate", new DateTime(2021, 10, 23, 21, 20, 16, 446, DateTimeKind.Local).AddTicks(2475), "The PowerUser has certain rights.", true, false, "InitialCreate", new DateTime(2021, 10, 23, 21, 20, 16, 446, DateTimeKind.Local).AddTicks(2476), "PowerUser", "This is PowerUser." },
                    { 3, "InitialCreate", new DateTime(2021, 10, 23, 21, 20, 16, 446, DateTimeKind.Local).AddTicks(2480), "The GuestUser is the least privileged role class", true, false, "InitialCreate", new DateTime(2021, 10, 23, 21, 20, 16, 446, DateTimeKind.Local).AddTicks(2481), "GuestUser", "This is GuestUser." }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CityId", "CreatedByName", "CreatedDate", "Description", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Name", "Note" },
                values: new object[,]
                {
                    { 1, 1, "InitialCreate", new DateTime(2021, 10, 23, 21, 20, 16, 438, DateTimeKind.Local).AddTicks(7466), "Yemek yenilebilecek yerler ile ilgili oluşturulmuş kategoridir.", true, false, "InitialCreate", new DateTime(2021, 10, 23, 21, 20, 16, 438, DateTimeKind.Local).AddTicks(7477), "Yemek", "Yemek Turist Rehberi Kategorisi" },
                    { 2, 1, "InitialCreate", new DateTime(2021, 10, 23, 21, 20, 16, 438, DateTimeKind.Local).AddTicks(7489), "Müze ve tarihsel yerler için oluşturulmuş kategoridir.", true, false, "InitialCreate", new DateTime(2021, 10, 23, 21, 20, 16, 438, DateTimeKind.Local).AddTicks(7491), "Tarihi Gezi", "Tarihi Gezi Turist Rehberi Kategorisi" },
                    { 3, 1, "InitialCreate", new DateTime(2021, 10, 23, 21, 20, 16, 438, DateTimeKind.Local).AddTicks(7494), "Doğal Parklar için oluşturulmuş kategoridir.", true, false, "InitialCreate", new DateTime(2021, 10, 23, 21, 20, 16, 438, DateTimeKind.Local).AddTicks(7495), "Doğa Gezisi", "Doğal Parklar Turist Rehberi Kategorisi" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedByName", "CreatedDate", "Description", "Email", "FirstName", "IsActive", "IsDeleted", "LastName", "ModifiedByName", "ModifiedDate", "Note", "PasswordHash", "Picture", "RoleId", "Username" },
                values: new object[] { 1, "InitialCreate", new DateTime(2021, 10, 23, 21, 20, 16, 455, DateTimeKind.Local).AddTicks(5429), "First Admin User.", "yusufokaraman@gmail.com", "Yusuf", true, false, "Karaman", "InitialCreate", new DateTime(2021, 10, 23, 21, 20, 16, 455, DateTimeKind.Local).AddTicks(5440), "Administrator", new byte[] { 48, 49, 57, 50, 48, 50, 51, 97, 55, 98, 98, 100, 55, 51, 50, 53, 48, 53, 49, 54, 102, 48, 54, 57, 100, 102, 49, 56, 98, 53, 48, 48 }, "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcSX4wVGjMQ37PaO4PdUVEAliSLi8-c2gJ1zvQ&usqp=CAU", 1, "yusufkaraman" });

            migrationBuilder.InsertData(
                table: "Places",
                columns: new[] { "Id", "Address", "CategoryId", "CityId", "CreatedByName", "CreatedDate", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Name", "Note", "PlacePicture" },
                values: new object[] { 1, "Adana Merkez,Adana Kebapçısı", 1, 1, "InitialCreate", new DateTime(2021, 10, 23, 21, 20, 16, 444, DateTimeKind.Local).AddTicks(7749), true, false, "InitialCreate", new DateTime(2021, 10, 23, 21, 20, 16, 444, DateTimeKind.Local).AddTicks(7759), "Adana Kebapçısı", "Adana'da yer alan kebapçı", "Default.jpg" });

            migrationBuilder.InsertData(
                table: "Places",
                columns: new[] { "Id", "Address", "CategoryId", "CityId", "CreatedByName", "CreatedDate", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Name", "Note", "PlacePicture" },
                values: new object[] { 2, "Adıyaman Ev Yemekler, Merkez-Adıyaman", 1, 2, "InitialCreate", new DateTime(2021, 10, 23, 21, 20, 16, 444, DateTimeKind.Local).AddTicks(8184), true, false, "InitialCreate", new DateTime(2021, 10, 23, 21, 20, 16, 444, DateTimeKind.Local).AddTicks(8185), "Adıyaman Ev Yemekleri", "Adıyaman'da faaliyer gösteren ev yemekleri restoranı.", "Default.jpg" });

            migrationBuilder.InsertData(
                table: "Places",
                columns: new[] { "Id", "Address", "CategoryId", "CityId", "CreatedByName", "CreatedDate", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Name", "Note", "PlacePicture" },
                values: new object[] { 3, "Adana Varda Köprüsü,Merkez Adana", 2, 3, "InitialCreate", new DateTime(2021, 10, 23, 21, 20, 16, 444, DateTimeKind.Local).AddTicks(8189), true, false, "InitialCreate", new DateTime(2021, 10, 23, 21, 20, 16, 444, DateTimeKind.Local).AddTicks(8191), "Adana Varda Köprüsü", "Adana'da bulunan tarihi Varda Köprüsü.", "Default.jpg" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "CityId", "CommentCount", "Content", "CreatedByName", "CreatedDate", "Date", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "PlaceId", "SeoAuthor", "SeoDescription", "SeoTags", "Thumbnail", "Title", "UserId", "ViewCount" },
                values: new object[] { 1, 1, 1, 1, "Lorem Ipsum, dizgi ve baskı endüstrisinde kullanılan mıgır metinlerdir. Lorem Ipsum, adı bilinmeyen bir matbaacının bir hurufat numune kitabı oluşturmak üzere bir yazı galerisini alarak karıştırdığı 1500'lerden beri endüstri standardı sahte metinler olarak kullanılmıştır. Beşyüz yıl boyunca varlığını sürdürmekle kalmamış, aynı zamanda pek değişmeden elektronik dizgiye de sıçramıştır. 1960'larda Lorem Ipsum pasajları da içeren Letraset yapraklarının yayınlanması ile ve yakın zamanda Aldus PageMaker gibi Lorem Ipsum sürümleri içeren masaüstü yayıncılık yazılımları ile popüler olmuştur.", "InitialCreate", new DateTime(2021, 10, 23, 21, 20, 16, 436, DateTimeKind.Local).AddTicks(557), new DateTime(2021, 10, 23, 21, 20, 16, 435, DateTimeKind.Local).AddTicks(9723), true, false, "InitialCreate", new DateTime(2021, 10, 23, 21, 20, 16, 436, DateTimeKind.Local).AddTicks(993), "Adana Yemek Kültürü Tanıtımı", 1, "Yusuf Karaman", "Adana Yemek Kültürü", "Adana, Kebap, Yemek", "Default.jpg", "Adana Yemek Kültürü", 1, 100 });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "CityId", "CommentCount", "Content", "CreatedByName", "CreatedDate", "Date", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "PlaceId", "SeoAuthor", "SeoDescription", "SeoTags", "Thumbnail", "Title", "UserId", "ViewCount" },
                values: new object[] { 2, 1, 2, 1, "Lorem Ipsum pasajlarının birçok çeşitlemesi vardır. Ancak bunların büyük bir çoğunluğu mizah katılarak veya rastgele sözcükler eklenerek değiştirilmişlerdir. Eğer bir Lorem Ipsum pasajı kullanacaksanız, metin aralarına utandırıcı sözcükler gizlenmediğinden emin olmanız gerekir. İnternet'teki tüm Lorem Ipsum üreteçleri önceden belirlenmiş metin bloklarını yineler. Bu da, bu üreteci İnternet üzerindeki gerçek Lorem Ipsum üreteci yapar. Bu üreteç, 200'den fazla Latince sözcük ve onlara ait cümle yapılarını içeren bir sözlük kullanır. Bu nedenle, üretilen Lorem Ipsum metinleri yinelemelerden, mizahtan ve karakteristik olmayan sözcüklerden uzaktır.", "InitialCreate", new DateTime(2021, 10, 23, 21, 20, 16, 436, DateTimeKind.Local).AddTicks(1965), new DateTime(2021, 10, 23, 21, 20, 16, 436, DateTimeKind.Local).AddTicks(1963), true, false, "InitialCreate", new DateTime(2021, 10, 23, 21, 20, 16, 436, DateTimeKind.Local).AddTicks(1966), "Adıyaman Yemek Kültürü Tanıtımı", 2, "Yusuf Karaman", "Adıyaman Yemek Kültürü", "Adıyaman, Kebap, Yemek", "Default.jpg", "Adıyaman Yemek Kültürü", 1, 100 });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "CityId", "CommentCount", "Content", "CreatedByName", "CreatedDate", "Date", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "PlaceId", "SeoAuthor", "SeoDescription", "SeoTags", "Thumbnail", "Title", "UserId", "ViewCount" },
                values: new object[] { 3, 2, 1, 1, "Lorem Ipsum pasajlarının birçok çeşitlemesi vardır. Ancak bunların büyük bir çoğunluğu mizah katılarak veya rastgele sözcükler eklenerek değiştirilmişlerdir. Eğer bir Lorem Ipsum pasajı kullanacaksanız, metin aralarına utandırıcı sözcükler gizlenmediğinden emin olmanız gerekir. İnternet'teki tüm Lorem Ipsum üreteçleri önceden belirlenmiş metin bloklarını yineler. Bu da, bu üreteci İnternet üzerindeki gerçek Lorem Ipsum üreteci yapar. Bu üreteç, 200'den fazla Latince sözcük ve onlara ait cümle yapılarını içeren bir sözlük kullanır. Bu nedenle, üretilen Lorem Ipsum metinleri yinelemelerden, mizahtan ve karakteristik olmayan sözcüklerden uzaktır.", "InitialCreate", new DateTime(2021, 10, 23, 21, 20, 16, 436, DateTimeKind.Local).AddTicks(1973), new DateTime(2021, 10, 23, 21, 20, 16, 436, DateTimeKind.Local).AddTicks(1971), true, false, "InitialCreate", new DateTime(2021, 10, 23, 21, 20, 16, 436, DateTimeKind.Local).AddTicks(1974), "Adana Tarihi Mekanlar Tanıtımı", 3, "Yusuf Karaman", "Adana Tarihi Yerler", "Adana, Kültür,Tarih,Vanda,Kebap", "Default.jpg", "Adana Tarihi Yerler", 1, 100 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "ArticleId", "CityId", "CreatedByName", "CreatedDate", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "PlaceId", "Text" },
                values: new object[] { 1, 1, 1, "InitialCreate", new DateTime(2021, 10, 23, 21, 20, 16, 442, DateTimeKind.Local).AddTicks(6128), true, false, "InitialCreate", new DateTime(2021, 10, 23, 21, 20, 16, 442, DateTimeKind.Local).AddTicks(6139), "Adana Kebapçısı Yorumu", 1, "Bu bir deneme yorumu olarak düşünülmüştür." });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "ArticleId", "CityId", "CreatedByName", "CreatedDate", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "PlaceId", "Text" },
                values: new object[] { 2, 2, 2, "InitialCreate", new DateTime(2021, 10, 23, 21, 20, 16, 442, DateTimeKind.Local).AddTicks(6150), true, false, "InitialCreate", new DateTime(2021, 10, 23, 21, 20, 16, 442, DateTimeKind.Local).AddTicks(6151), "Adıyaman Ev Yemekleri Yorumu", 2, "Adıyaman Ev Yemekleri üzerine deneme yorumu." });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "ArticleId", "CityId", "CreatedByName", "CreatedDate", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "PlaceId", "Text" },
                values: new object[] { 3, 3, 1, "InitialCreate", new DateTime(2021, 10, 23, 21, 20, 16, 442, DateTimeKind.Local).AddTicks(6155), true, false, "InitialCreate", new DateTime(2021, 10, 23, 21, 20, 16, 442, DateTimeKind.Local).AddTicks(6156), "Adana Vanda Köprüsü Yorumu Yorumu", 3, "Adana Vanda Köprüsü üzerine bir deneme yorumudur." });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_CategoryId",
                table: "Articles",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_CityId",
                table: "Articles",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_PlaceId",
                table: "Articles",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_UserId",
                table: "Articles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CityId",
                table: "Categories",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ArticleId",
                table: "Comments",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CityId",
                table: "Comments",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PlaceId",
                table: "Comments",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Places_CategoryId",
                table: "Places",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Places_CityId",
                table: "Places",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
