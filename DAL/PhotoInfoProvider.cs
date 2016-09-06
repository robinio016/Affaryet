using BOL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PhotoInfoProvider
    {
        private ProductProvider productProvider;
        public PhotoInfoProvider()
        {
            productProvider = new ProductProvider();
        }

        public List<PhotoInfo> GetAllPhoto()
        {
            using (VientVendreDBContext db = new VientVendreDBContext())
            {
                var res = db.PhotoInfos.ToList();
                return res;
            }
        }

        public PhotoInfo GetPhotoByID(int id)
        {
            using (VientVendreDBContext db = new VientVendreDBContext())
            {
                return db.PhotoInfos.Find(id);
            }
        }

        public List<PhotoInfo> GetPhotoByProduct(int ProductID)
        {
            using (VientVendreDBContext db = new VientVendreDBContext())
            {
                try
                {

                    var query = from p in db.Products
                                from ph in p.PhotoInfos
                                where ph.ProductID == ProductID
                                select ph;

                    return query.ToList();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public void CreatePhoto(PhotoInfo photoInfo)
        {
            int a = photoInfo.ProductID == null ? default(int):1;
            using (VientVendreDBContext db = new VientVendreDBContext())
            {
               // db.Products.Attach(productProvider.GetProductByID(a));
                db.PhotoInfos.Add(photoInfo);
                db.SaveChanges();
            }
        }

        public void DeletePhoto(int id)
        {
            using (VientVendreDBContext db = new VientVendreDBContext())
            {
                var photoInfo = db.PhotoInfos.Find(id);
                db.PhotoInfos.Remove(photoInfo);
                db.SaveChanges();
            }
        }

        public void UpdatePhoto(PhotoInfo photoInfo)
        {
            using (VientVendreDBContext db = new VientVendreDBContext())
            {
                try
                {
                    db.Entry(photoInfo).State = EntityState.Modified;
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
