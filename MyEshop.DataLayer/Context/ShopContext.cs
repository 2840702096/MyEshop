using Microsoft.EntityFrameworkCore;
using MyEshop.DataLayer.Entities.AnswerToRepetitiveQuestions;
using MyEshop.DataLayer.Entities.Boxes;
using MyEshop.DataLayer.Entities.Categories;
using MyEshop.DataLayer.Entities.DiscountCode;
using MyEshop.DataLayer.Entities.Orders;
using MyEshop.DataLayer.Entities.Other;
using MyEshop.DataLayer.Entities.Products;
using MyEshop.DataLayer.Entities.Rules;
using MyEshop.DataLayer.Entities.SliderAndBaners;
using MyEshop.DataLayer.Entities.Users;
using MyEshop.DataLayer.Entities.Weblogs;
using MyEShop.DataLayer.Entities.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEShop.DataLayer.Context
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {

        }

        #region QueryFilters

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;


            modelBuilder.Entity<ProductCategories>().HasQueryFilter(p => !p.IsDelete);

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<WebSiteCategories>().HasQueryFilter(w => !w.IsDelete);

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Sliders>().HasQueryFilter(s => !s.IsDelete);

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Banners>().HasQueryFilter(b => !b.IsDelete);

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<WeblogCategories>().HasQueryFilter(w => !w.IsDelete);

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Weblog>().HasQueryFilter(w => !w.IsDelete);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductBrands>().HasQueryFilter(p => !p.IsDelete);

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Products>().HasQueryFilter(p => !p.IsDelete);

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<ColorList>().HasQueryFilter(c => !c.IsDelete);

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<ProductColors>().HasQueryFilter(p => !p.IsDelete);

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Comments_Questions_Answer>().HasQueryFilter(cqa => !cqa.IsRefused);

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<RepetitiveQuestionCategories>().HasQueryFilter(r => !r.IsDelete);

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Users>().HasQueryFilter(u => !u.IsDelete);

            base.OnModelCreating(modelBuilder);
        }

        #endregion

        #region Contexts

        #region Categories
        public DbSet<ProductCategories> ProductCategories { get; set; }
        public DbSet<WebSiteCategories> WebSiteCategories { get; set; }
        public DbSet<WeblogCategories> WeblogCategories { get; set; }
        public DbSet<SpecificationsInCategories> SpecificationsInCategories { get; set; }
        public DbSet<RepetitiveQuestionCategories> RepetitiveQuestionCategories { get; set; }

        #endregion

        #region SliderAndBaners

        public DbSet<Sliders> Sliders { get; set; }
        public DbSet<Banners> Banners { get; set; }

        #endregion

        #region Weblogs

        public DbSet<WeblogComments> WeblogComments { get; set; }
        public DbSet<Weblog> Weblogs { get; set; }

        #endregion

        #region Products

        public DbSet<Products> Products { get; set; }
        public DbSet<ProductBrands> ProductBrands { get; set; }
        public DbSet<ProductSpecifications> ProductSpecifications { get; set; }
        public DbSet<ColorList> ColorList { get; set; }
        public DbSet<ProductColors> ProductColors { get; set; }
        public DbSet<ImageGallery> ImageGallery { get; set; }
        public DbSet<ProductProperties> ProductProperties { get; set; }
        public DbSet<Comments_Questions_Answer> Comments_Questions_Answers { get; set; }
        public DbSet<SlideCommentTitles> SlideCommentTitles { get; set; }
        public DbSet<SlideComments> SlideComments { get; set; }
        public DbSet<StrengthsOrWeaknesses> StrengthsOrWeaknesses { get; set; }

        #endregion

        #region DiscountCode

        public DbSet<DiscountCode> DiscountCodes { get; set; }

        #endregion

        #region RepetitiveQuestions

        public DbSet<RepetitiveQuestions> RepetitiveQuestions { get; set; }

        #endregion

        #region Other

        public DbSet<Logoes> Logoes { get; set; }
        public DbSet<SocialApps> SocialApps { get; set; }

        #endregion

        #region Rules


        public DbSet<PrivacyRules> PrivacyRules { get; set; }
        public DbSet<PublishContentRules> PublishContentRules { get; set; }

        #endregion

        #region Orders

        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        public DbSet<AboutUs> AboutUs { get; set; }

        #endregion

        #region Users

        public DbSet<Users> Users { get; set; }

        #endregion

        #region Boxes

        public DbSet<Boxes> Boxes { get; set; }
        public DbSet<NamesOfBoxes> NamesOfBoxes { get; set; }


        #endregion

        #endregion
    }
}
