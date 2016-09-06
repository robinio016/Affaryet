using BOL;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    public class RegionEntityConfiguration:EntityTypeConfiguration<Region>
    {
        public RegionEntityConfiguration()
        {
            this.ToTable("Region");

            this.HasKey<int>(c=>c.RegionID);

            this.Property(s => s.RegionName)
                .HasMaxLength(50);

            
        }
    }
}
