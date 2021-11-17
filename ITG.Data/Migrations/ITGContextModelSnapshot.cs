﻿// <auto-generated />
using System;
using ITG.Data.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ITG.Data.Migrations
{
    [DbContext(typeof(ITGContext))]
    partial class ITGContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ITG.Entities.Concrete.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<int>("CommentCount")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(MAX)");

                    b.Property<string>("CreatedByName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedByName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Note")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("PlaceId")
                        .HasColumnType("int");

                    b.Property<string>("SeoAuthor")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SeoDescription")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("SeoTags")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("Thumbnail")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("ViewCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CityId");

                    b.HasIndex("PlaceId");

                    b.HasIndex("UserId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("ITG.Entities.Concrete.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedByName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedByName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("Note")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CityId = 1,
                            CreatedByName = "InitialCreate",
                            CreatedDate = new DateTime(2021, 11, 16, 22, 26, 31, 103, DateTimeKind.Local).AddTicks(6983),
                            Description = "Yemek yenilebilecek yerler ile ilgili oluşturulmuş kategoridir.",
                            IsActive = true,
                            IsDeleted = false,
                            ModifiedByName = "InitialCreate",
                            ModifiedDate = new DateTime(2021, 11, 16, 22, 26, 31, 103, DateTimeKind.Local).AddTicks(7457),
                            Name = "Yemek",
                            Note = "Yemek Turist Rehberi Kategorisi"
                        },
                        new
                        {
                            Id = 2,
                            CityId = 1,
                            CreatedByName = "InitialCreate",
                            CreatedDate = new DateTime(2021, 11, 16, 22, 26, 31, 103, DateTimeKind.Local).AddTicks(7919),
                            Description = "Müze ve tarihsel yerler için oluşturulmuş kategoridir.",
                            IsActive = true,
                            IsDeleted = false,
                            ModifiedByName = "InitialCreate",
                            ModifiedDate = new DateTime(2021, 11, 16, 22, 26, 31, 103, DateTimeKind.Local).AddTicks(7920),
                            Name = "Tarihi Gezi",
                            Note = "Tarihi Gezi Turist Rehberi Kategorisi"
                        },
                        new
                        {
                            Id = 3,
                            CityId = 1,
                            CreatedByName = "InitialCreate",
                            CreatedDate = new DateTime(2021, 11, 16, 22, 26, 31, 103, DateTimeKind.Local).AddTicks(7925),
                            Description = "Doğal Parklar için oluşturulmuş kategoridir.",
                            IsActive = true,
                            IsDeleted = false,
                            ModifiedByName = "InitialCreate",
                            ModifiedDate = new DateTime(2021, 11, 16, 22, 26, 31, 103, DateTimeKind.Local).AddTicks(7926),
                            Name = "Doğa Gezisi",
                            Note = "Doğal Parklar Turist Rehberi Kategorisi"
                        });
                });

            modelBuilder.Entity("ITG.Entities.Concrete.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(MAX)");

                    b.Property<string>("CreatedByName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedByName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("Note")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Thumbnail")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Content = "Adana, Türkiye'nin bir ili ve en kalabalık altıncı şehridir. 2019 yılı verilerine göre 2.258.718 nüfusa sahiptir. İlin yüz ölçümü 13.844 km²dir. İlde km²ye 160 kişi düşmektedir. 01.02.2018 TÜİK verilerine göre 5'i merkez ilçe olmak üzere toplam 15 ilçesi ve belediyesi vardır. Bu ilçelerde 831 mahalle bulunmaktadır.",
                            CreatedByName = "InitialCreate",
                            CreatedDate = new DateTime(2021, 11, 16, 22, 26, 31, 106, DateTimeKind.Local).AddTicks(4132),
                            IsActive = true,
                            IsDeleted = false,
                            ModifiedByName = "InitialCreate",
                            ModifiedDate = new DateTime(2021, 11, 16, 22, 26, 31, 106, DateTimeKind.Local).AddTicks(4145),
                            Name = "Adana",
                            Note = "01 Plakalı il Adana.",
                            Thumbnail = "Default.jpg"
                        },
                        new
                        {
                            Id = 2,
                            Content = "Adıyaman, aynı isimli ilin merkez ilçesidir. Adıyaman merkez ilçesinin nüfusu 2020 istatistiklerine 310.644'dür. ",
                            CreatedByName = "InitialCreate",
                            CreatedDate = new DateTime(2021, 11, 16, 22, 26, 31, 106, DateTimeKind.Local).AddTicks(4157),
                            IsActive = true,
                            IsDeleted = false,
                            ModifiedByName = "InitialCreate",
                            ModifiedDate = new DateTime(2021, 11, 16, 22, 26, 31, 106, DateTimeKind.Local).AddTicks(4158),
                            Name = "Adıyaman",
                            Note = "02 Plakalı il Adıyaman.",
                            Thumbnail = "Default.jpg"
                        },
                        new
                        {
                            Id = 3,
                            Content = "Afyonkarahisar veya eski ve halk arasındaki ismiyle Afyon, aynı isimli ilin merkezidir. Mermercilik ve gıda sektöründe Türkiye içinde ve dışında isim yapmıştır. Şehrin Afyon olan ismi, 2005 yılında Afyonkarahisar olarak değiştirilmiştir. ",
                            CreatedByName = "InitialCreate",
                            CreatedDate = new DateTime(2021, 11, 16, 22, 26, 31, 106, DateTimeKind.Local).AddTicks(4162),
                            IsActive = true,
                            IsDeleted = false,
                            ModifiedByName = "InitialCreate",
                            ModifiedDate = new DateTime(2021, 11, 16, 22, 26, 31, 106, DateTimeKind.Local).AddTicks(4163),
                            Name = "Afyon",
                            Note = "03 Plakalı il Afyon.",
                            Thumbnail = "Default.jpg"
                        });
                });

            modelBuilder.Entity("ITG.Entities.Concrete.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ArticleId")
                        .HasColumnType("int");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedByName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedByName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Note")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("PlaceId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("CityId");

                    b.HasIndex("PlaceId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("ITG.Entities.Concrete.Place", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedByName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedByName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("Note")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("PlacePicture")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CityId");

                    b.ToTable("Places");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Adana Merkez,Adana Kebapçısı",
                            CategoryId = 1,
                            CityId = 1,
                            CreatedByName = "InitialCreate",
                            CreatedDate = new DateTime(2021, 11, 16, 22, 26, 31, 110, DateTimeKind.Local).AddTicks(9904),
                            IsActive = true,
                            IsDeleted = false,
                            ModifiedByName = "InitialCreate",
                            ModifiedDate = new DateTime(2021, 11, 16, 22, 26, 31, 110, DateTimeKind.Local).AddTicks(9921),
                            Name = "Adana Kebapçısı",
                            Note = "Adana'da yer alan kebapçı",
                            PlacePicture = "Default.jpg"
                        },
                        new
                        {
                            Id = 2,
                            Address = "Adıyaman Ev Yemekler, Merkez-Adıyaman",
                            CategoryId = 1,
                            CityId = 2,
                            CreatedByName = "InitialCreate",
                            CreatedDate = new DateTime(2021, 11, 16, 22, 26, 31, 111, DateTimeKind.Local).AddTicks(370),
                            IsActive = true,
                            IsDeleted = false,
                            ModifiedByName = "InitialCreate",
                            ModifiedDate = new DateTime(2021, 11, 16, 22, 26, 31, 111, DateTimeKind.Local).AddTicks(371),
                            Name = "Adıyaman Ev Yemekleri",
                            Note = "Adıyaman'da faaliyer gösteren ev yemekleri restoranı.",
                            PlacePicture = "Default.jpg"
                        },
                        new
                        {
                            Id = 3,
                            Address = "Adana Varda Köprüsü,Merkez Adana",
                            CategoryId = 2,
                            CityId = 3,
                            CreatedByName = "InitialCreate",
                            CreatedDate = new DateTime(2021, 11, 16, 22, 26, 31, 111, DateTimeKind.Local).AddTicks(375),
                            IsActive = true,
                            IsDeleted = false,
                            ModifiedByName = "InitialCreate",
                            ModifiedDate = new DateTime(2021, 11, 16, 22, 26, 31, 111, DateTimeKind.Local).AddTicks(376),
                            Name = "Adana Varda Köprüsü",
                            Note = "Adana'da bulunan tarihi Varda Köprüsü.",
                            PlacePicture = "Default.jpg"
                        });
                });

            modelBuilder.Entity("ITG.Entities.Concrete.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ConcurrencyStamp = "8d8ba6de-af72-4172-b1ec-48e2214d33c7",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = 2,
                            ConcurrencyStamp = "2a653dd7-a6ae-4470-a972-dbd6b5a4598c",
                            Name = "PoweUser",
                            NormalizedName = "POWERUSER"
                        });
                });

            modelBuilder.Entity("ITG.Entities.Concrete.RoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("ITG.Entities.Concrete.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Picture")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "fe0f8182-a2fc-435e-81c2-dd27acd5614d",
                            Email = "adminuser@kocaeli.edu.tr",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMINUSER@KOCAELI.EDU.TR",
                            NormalizedUserName = "ADMINUSER",
                            PasswordHash = "AQAAAAEAACcQAAAAEMrE24mzf6KQNbdW0uJmtoEl1UqxdS1nRJMyI2c0DtN5vp+rhIRw84HI1+D17OOiEA==",
                            PhoneNumber = "+905553332211",
                            PhoneNumberConfirmed = true,
                            Picture = "defaultUser.png",
                            SecurityStamp = "e7f4362a-2dd3-4e92-93ce-9d44b591efb8",
                            TwoFactorEnabled = false,
                            UserName = "adminuser"
                        },
                        new
                        {
                            Id = 2,
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "ddde49dd-430b-4cf5-bf45-6197af6f6b42",
                            Email = "poweruser@kocaeli.edu.tr",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "POWERUSER@KOCAELI.EDU.TR",
                            NormalizedUserName = "POWERUSER",
                            PasswordHash = "AQAAAAEAACcQAAAAEOYbaDMaxpgndE5EL+G9M0kfdRlWFk4zmhm3BOcF1cRKjlTAm4+fThA5gFk7oRFxaQ==",
                            PhoneNumber = "+905553332211",
                            PhoneNumberConfirmed = true,
                            Picture = "defaultUser.png",
                            SecurityStamp = "522e28a7-96c0-4af9-8704-f8dc3079414f",
                            TwoFactorEnabled = false,
                            UserName = "poweruser"
                        });
                });

            modelBuilder.Entity("ITG.Entities.Concrete.UserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("ITG.Entities.Concrete.UserLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("ITG.Entities.Concrete.UserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            RoleId = 1
                        },
                        new
                        {
                            UserId = 2,
                            RoleId = 2
                        });
                });

            modelBuilder.Entity("ITG.Entities.Concrete.UserToken", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ITG.Entities.Concrete.Article", b =>
                {
                    b.HasOne("ITG.Entities.Concrete.Category", "Category")
                        .WithMany("Articles")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ITG.Entities.Concrete.City", "City")
                        .WithMany("Articles")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ITG.Entities.Concrete.Place", "Place")
                        .WithMany("Articles")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ITG.Entities.Concrete.User", "User")
                        .WithMany("Articles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("City");

                    b.Navigation("Place");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ITG.Entities.Concrete.Category", b =>
                {
                    b.HasOne("ITG.Entities.Concrete.City", "City")
                        .WithMany("Categories")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("ITG.Entities.Concrete.Comment", b =>
                {
                    b.HasOne("ITG.Entities.Concrete.Article", "Article")
                        .WithMany("Comments")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ITG.Entities.Concrete.City", "City")
                        .WithMany("Comments")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ITG.Entities.Concrete.Place", "Place")
                        .WithMany("Comments")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Article");

                    b.Navigation("City");

                    b.Navigation("Place");
                });

            modelBuilder.Entity("ITG.Entities.Concrete.Place", b =>
                {
                    b.HasOne("ITG.Entities.Concrete.Category", "Category")
                        .WithMany("Places")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ITG.Entities.Concrete.City", "City")
                        .WithMany("Places")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("City");
                });

            modelBuilder.Entity("ITG.Entities.Concrete.RoleClaim", b =>
                {
                    b.HasOne("ITG.Entities.Concrete.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ITG.Entities.Concrete.UserClaim", b =>
                {
                    b.HasOne("ITG.Entities.Concrete.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ITG.Entities.Concrete.UserLogin", b =>
                {
                    b.HasOne("ITG.Entities.Concrete.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ITG.Entities.Concrete.UserRole", b =>
                {
                    b.HasOne("ITG.Entities.Concrete.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ITG.Entities.Concrete.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ITG.Entities.Concrete.UserToken", b =>
                {
                    b.HasOne("ITG.Entities.Concrete.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ITG.Entities.Concrete.Article", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("ITG.Entities.Concrete.Category", b =>
                {
                    b.Navigation("Articles");

                    b.Navigation("Places");
                });

            modelBuilder.Entity("ITG.Entities.Concrete.City", b =>
                {
                    b.Navigation("Articles");

                    b.Navigation("Categories");

                    b.Navigation("Comments");

                    b.Navigation("Places");
                });

            modelBuilder.Entity("ITG.Entities.Concrete.Place", b =>
                {
                    b.Navigation("Articles");

                    b.Navigation("Comments");
                });

            modelBuilder.Entity("ITG.Entities.Concrete.User", b =>
                {
                    b.Navigation("Articles");
                });
#pragma warning restore 612, 618
        }
    }
}
