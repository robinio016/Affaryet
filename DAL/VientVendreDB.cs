using BOL;
using DAL.Configurations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class VientVendreDBContext : DbContext
    {
        public VientVendreDBContext() : base("name=VientVendreDBConnectionString")
        {

            //add the initializer class when model change
            Database.SetInitializer<VientVendreDBContext>(new VientVendreDBInitializer());

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<VientVendreDBContext, DAL.Migrations.Configuration>("VientVendreDBConnectionString"));
        }
        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Annonce> Annonces { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PhotoInfo> PhotoInfos { get; set; }
        public DbSet<CommentUserProd> CommentUserProds { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            //add configuration to table (mapping)
            modelBuilder.Configurations.Add(new ProductEntityConfiguration());
            modelBuilder.Configurations.Add(new UserEntityConfiguration());
            modelBuilder.Configurations.Add(new AnnonceEntityConfiguration());
            modelBuilder.Configurations.Add(new CategoryEntityConfiguration());
            modelBuilder.Configurations.Add(new RegionEntityConfiguration());
            //Configure doamin classes using Fluent Api
            base.OnModelCreating(modelBuilder);
        }
    }

}
