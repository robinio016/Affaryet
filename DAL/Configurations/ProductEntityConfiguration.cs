using BOL;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    public class ProductEntityConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductEntityConfiguration()
        {
            this.ToTable("Product");

            this.HasKey<int>(s => s.ProductID);

            this.Property(s => s.ProductName)
                .HasMaxLength(50);

            ////
            this.HasOptional<Annonce>(s => s.Annonce)
                .WithMany(s => s.Products)
                .HasForeignKey(s => s.AnnonceID)
                .WillCascadeOnDelete(true);


             
        }
    }
}
