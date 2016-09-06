using BOL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RegionProvider
    {
        public RegionProvider()
        {
        }

        public List<Region> GetAllRegion()
        {
            using (VientVendreDBContext db = new VientVendreDBContext())
            {
                var res = db.Regions.ToList();
                return res;
            }
        }

        public Region GetRegionByID(int id)
        {
            using (VientVendreDBContext db = new VientVendreDBContext())
            {
                return db.Regions.Find(id);
            }
        }
        public List<Region> GetRegionByAnnonce(int annID)
        {
            using (VientVendreDBContext db = new VientVendreDBContext())
            {
                try
                {
                    var query = from a in db.Annonces
                                from r in a.Regions
                                where a.AnnonceID == annID
                                select r;

                    return query.ToList();
                }
                catch
                {
                    throw;
                }
            }
        }

        public void CreateRegion(Region region)
        {
            using (VientVendreDBContext db = new VientVendreDBContext())
            {
                db.Regions.Add(region);
                db.SaveChanges();
            }
        }

        public void DeleteRegion(int id)
        {
            using (VientVendreDBContext db = new VientVendreDBContext())
            {
                var region = db.Regions.Find(id);
                db.Regions.Remove(region);
                db.SaveChanges();
            }
        }

        public void UpdateRegion(Region region)
        {           
                using (VientVendreDBContext db = new VientVendreDBContext())
                {
                    try
                    {
                        db.Entry(region).State = EntityState.Modified;
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
