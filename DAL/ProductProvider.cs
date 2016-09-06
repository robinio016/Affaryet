using BOL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ProductProvider
    {
        private VientVendreDBContext db;
        public ProductProvider()
        {
        }

        public List<Product> GetAllProduct ()
        {
            
                using (db = new VientVendreDBContext())
                {
                    try
                    {
                        return db.Products.ToList();
                    }
                    catch (Exception ex)
                    {
                       throw;
                    }
                }
            
            
        }

        public Product GetProductByID(int id)
        {
            //devolop code linq
            using (db = new VientVendreDBContext())
            {
                try
                {
                    var query = from c in db.Products where c.ProductID == id select c;

                    return query.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public List<Product> GetProductByCategory(int idCategory)
        {
            //develop code linq
            using (db = new VientVendreDBContext())
            {
                try
                {
                    // because i did not write the jonction table i can do like this (there is other format that include cat_prod table if i wrote this table in BOL)
                    //with a linq method

                    // var query = db.Products.Where(p => p.Categories.Any(c => c.CategoryID == idCategory));

                    //without a linq method
                    var query = from p in db.Products
                                from c in p.Categories
                                where c.CategoryID == idCategory
                                select p;

                    return query.ToList();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public List<Product> GetProductByRegion(int idRegion)
        {
            using (db = new VientVendreDBContext())
            {
                try
                {
                    

                    //without a linq method
                    var query = from p in db.Products
                                from a in db.Annonces
                                from r in a.Regions
                                where p.AnnonceID == a.AnnonceID && r.RegionID == idRegion
                                select p;

                    return query.ToList();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }


        /////////////////////////////
        // a voir la requete dans GetProductByCategoryAndRegion
        /// ////////////////

        public List<Product> GetProductByCategoryAndRegion(int idCategory, int idRegion)
        {
            //without a linq method
            try
            {
                using (db = new VientVendreDBContext())
                {
                    var query = from a in db.Annonces
                                from p in a.Products
                                from r in a.Regions
                                from c in p.Categories
                                where (c.CategoryID == idCategory) && (r.RegionID == idRegion)
                                select p;

                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Product> GetProductByAnnonce(int annonceID)
        {
            try
            {
                using (db = new VientVendreDBContext())
                {
                    var query = from a in db.Annonces
                                from p in a.Products
                                where a.AnnonceID == annonceID
                                select p;
                    return query.ToList();
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public List<Product> GetProductByPrice(double MinPrice,double MaxPrice)
        {
            using (db = new VientVendreDBContext())
            {
                try
                {


                    //without a linq method
                    var query = from p in db.Products                               
                                where p.ProductPrice>=MinPrice && p.ProductPrice<=MaxPrice 
                                select p;

                    return query.ToList();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public List<Product> GetProductByPriceAndRegion(double MinPrice, double MaxPrice, int idRegion)
        {
            using (db = new VientVendreDBContext())
            {
                try
                {


                    //without a linq method
                    var query = from a in db.Annonces
                                from r in a.Regions
                                from p in a.Products 
                                where (p.ProductPrice >= MinPrice) && (p.ProductPrice <= MaxPrice) && (r.RegionID==idRegion)
                                select p;

                    return query.ToList();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public List<Product> GetProductByPriceAndCategory(double MinPrice, double MaxPrice, int idCategory)
        {
            using (db = new VientVendreDBContext())
            {
                try
                {


                    //without a linq method
                    var query = from p in db.Products
                                from c in p.Categories
                                where (p.ProductPrice >= MinPrice) && (p.ProductPrice <= MaxPrice) && (c.CategoryID == idCategory)
                                select p;

                    return query.ToList();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public List<Product> GetProductByPriceAndRegionAndCategory(double MinPrice, double MaxPrice, int idRegion,int idCategory)
        {
            using (db = new VientVendreDBContext())
            {
                try
                {


                    //without a linq method
                    var query = from a in db.Annonces
                                from p in a.Products
                                from r in a.Regions
                                from c in p.Categories
                                where (p.ProductPrice >= MinPrice) && (p.ProductPrice <= MaxPrice) && (c.CategoryID == idCategory) && (r.RegionID==idRegion)
                                select p;

                    return query.ToList();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public void CreateProduct(Product newProduct)
        {
            using (VientVendreDBContext db = new VientVendreDBContext())
            {
                try
                {
                    foreach (var cat in newProduct.Categories)
                    {
                        db.Categories.Attach(cat);
                    }

                    db.Entry(newProduct).State = EntityState.Added;
                    //db.Products.Add(newProduct);
                    db.SaveChanges();
                }
                catch
                {
                    throw;
                }
               

            }
        }

        public void DeleteProduct(int id)
        {
            using (VientVendreDBContext db = new VientVendreDBContext())
            {
                var product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();
            }
        }

        public void UpdateProduct(Product product)
        {
            using (VientVendreDBContext db = new VientVendreDBContext())
            {
                try
                {
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }


    }
}
