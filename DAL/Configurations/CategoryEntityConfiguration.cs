using BOL;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    class CategoryEntityConfiguration:EntityTypeConfiguration<Category>
    {
        public CategoryEntityConfiguration()
        {
            this.ToTable("Category");

            this.Property(s => s.CategoryName)
                .HasMaxLength(50);

            this.HasMany(s => s.Products)
                .WithMany(c => c.Categories)
                .Map(cs =>
                {
                    cs.MapLeftKey("CategoryID");
                    cs.MapRightKey("ProductID");
                    cs.ToTable("Category_Product");
                });
        }
    }
}
