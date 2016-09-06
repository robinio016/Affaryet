using BOL;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    public class AnnonceEntityConfiguration:EntityTypeConfiguration<Annonce>
    {
        public AnnonceEntityConfiguration()
        {
            this.ToTable("Annonce");

            this.HasKey<int>(s => s.AnnonceID);

            this.Property(p => p.AnnonceTitle)
                .HasMaxLength(50);

            this.HasOptional<User>(u => u.User)
                .WithMany(a=>a.Annonces)
                .HasForeignKey(f=>f.UserID)
                .WillCascadeOnDelete(true);

            this.HasMany<Region>(s => s.Regions)
                .WithMany(c => c.Annonces)
                .Map(cs =>
                {
                    cs.MapLeftKey("AnnonceID");
                    cs.MapRightKey("RegionID");
                    cs.ToTable("Annonce_Region");
                });
        }
    }
}
