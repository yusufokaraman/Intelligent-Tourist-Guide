using ITG.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITG.Data.Concrete.EntityFramework.Mappings
{
    public class CommentMapping : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Text).IsRequired();
            builder.Property(c => c.Text).HasMaxLength(1000);
            builder.HasOne<Article>(c => c.Article).WithMany(a => a.Comments).HasForeignKey(c => c.ArticleId);
            builder.Property(c => c.CreatedByName).IsRequired();
            builder.Property(c => c.CreatedByName).HasMaxLength(50);
            builder.Property(c => c.ModifiedByName).IsRequired();
            builder.Property(c => c.ModifiedByName).HasMaxLength(50);
            builder.Property(c => c.CreatedDate).IsRequired();
            builder.Property(c => c.ModifiedDate).IsRequired();
            builder.Property(c => c.IsActive).IsRequired();
            builder.Property(c => c.IsDeleted).IsRequired();
            builder.Property(c => c.Note).HasMaxLength(500);
            builder.HasOne<Place>(c => c.Place).WithMany(a => a.Comments).HasForeignKey(c => c.PlaceId).OnDelete(DeleteBehavior.NoAction); ;
            builder.HasOne<City>(c => c.City).WithMany(a => a.Comments).HasForeignKey(c => c.CityId).OnDelete(DeleteBehavior.NoAction); ;
            builder.ToTable("Comments");
            //builder.HasData(new Comment
            //{
            //    Id = 1,
            //    ArticleId = 1,
            //    PlaceId = 1,
            //    CityId = 1,
            //    Text = "Bu bir deneme yorumu olarak düşünülmüştür.",
            //    IsActive = true,
            //    IsDeleted = false,
            //    CreatedByName = "InitialCreate",
            //    CreatedDate = DateTime.Now,
            //    ModifiedByName = "InitialCreate",
            //    ModifiedDate = DateTime.Now,
            //    Note = "Adana Kebapçısı Yorumu"
            //},
            //new Comment
            //{
            //    Id = 2,
            //    ArticleId = 2,
            //    PlaceId = 2,
            //    CityId = 2,
            //    Text = "Adıyaman Ev Yemekleri üzerine deneme yorumu.",
            //    IsActive = true,
            //    IsDeleted = false,
            //    CreatedByName = "InitialCreate",
            //    CreatedDate = DateTime.Now,
            //    ModifiedByName = "InitialCreate",
            //    ModifiedDate = DateTime.Now,
            //    Note = "Adıyaman Ev Yemekleri Yorumu",
            //},
            // new Comment
            // {
            //     Id = 3,
            //     ArticleId = 3,
            //     PlaceId = 3,
            //     CityId = 1,
            //     Text = "Adana Vanda Köprüsü üzerine bir deneme yorumudur.",
            //     IsActive = true,
            //     IsDeleted = false,
            //     CreatedByName = "InitialCreate",
            //     CreatedDate = DateTime.Now,
            //     ModifiedByName = "InitialCreate",
            //     ModifiedDate = DateTime.Now,
            //     Note = "Adana Vanda Köprüsü Yorumu Yorumu",
            // }
            //);
        }
    }
}
