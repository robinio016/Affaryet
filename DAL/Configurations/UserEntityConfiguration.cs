using BOL;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//pour ne pas encombrer le code dans OnModelCreating method
namespace DAL.Configurations
{
    class UserEntityConfiguration : EntityTypeConfiguration<User>
    {
        public UserEntityConfiguration()
        {
            this.ToTable("User");
            this.HasKey<int>(s => s.UserID);

            this.Property(p => p.DateOfBirth)
                .HasColumnName("Date Of Birth")
                .HasColumnOrder(3)
                .HasColumnType("datetime2");

            this.Property(p => p.UserName)
                .HasMaxLength(50);


        }
    }
}
