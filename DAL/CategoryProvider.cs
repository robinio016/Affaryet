using BOL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CategoryProvider
    {
        public CategoryProvider()
        {
        }

        public List<Category> GetAllCategory()
        {
            using (VientVendreDBContext db = new VientVendreDBContext())
            {
                var res = db.Categories.ToList();
                return res;
            }
        }
        public List<Category> GetCategoryByProduct(int productID)
        {
            using (VientVendreDBContext db = new VientVendreDBContext())
            {
                try
                {
                    var query = from p in db.Products
                                from c in p.Categories
                                where p.ProductID == productID
                                select c;

                    return query.ToList();
                }
                catch
                {
                    throw;
                }
            }
        }

        public Category GetCategoryByID(int id)
        {
            using (VientVendreDBContext db = new VientVendreDBContext())
            {
                return db.Categories.Find(id);
            }
        }

        public void CreateCategory(Category category)
        {
            using (VientVendreDBContext db = new VientVendreDBContext())
            {
                db.Categories.Add(category);
                db.SaveChanges();
            }
        }

        public void DeleteCategory(int id)
        {
            using (VientVendreDBContext db = new VientVendreDBContext())
            {
                var category = db.Categories.Find(id);
                db.Categories.Remove(category);
                db.SaveChanges();
            }
        }

        public void UpdateCategory(Category category)
        {
            using (VientVendreDBContext db = new VientVendreDBContext())
            {
                try
                {
                    db.Entry(category).State = EntityState.Modified;
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
