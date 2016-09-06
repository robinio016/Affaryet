using BOL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AnnonceProvider
    {
        VientVendreDBContext db;
        public AnnonceProvider()
        {

        }

        public List<Annonce> GetAllAnnonce()
        {
            try
            {

                using (db = new VientVendreDBContext())
                {
                    //without a linq method
                    var query = from a in db.Annonces
                                select a;

                    return query.ToList();
                }
                    
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Annonce GetAnnonceByID(int id)
        {
            try
            {

                using (db = new VientVendreDBContext())
                {
                    //without a linq method
                    var query = from a in db.Annonces
                                where a.AnnonceID==id
                                select a;

                    return query.First();
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Annonce> GetAnnonceByUser(int idUser)
        {
            try
            {

                using (db = new VientVendreDBContext())
                {
                    //without a linq method
                    var query = from u in db.Users
                                from a in u.Annonces
                                where u.UserID == idUser
                                select a;

                    return query.ToList();
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //this method is not logic
        //public List<Annonce> GetAnnonceByCategory(int idCategory)
        //{
        //    //develop code linq
        //    throw new NotImplementedException();
        //}

        public List<Annonce> GetAnnonceByRegion(int idRegion)
        {
            try
            {

                using (db = new VientVendreDBContext())
                {
                    //without a linq method
                    var query = from a in db.Annonces
                                from r in a.Regions
                                where r.RegionID == idRegion
                                select a;

                    return query.ToList();
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        //this method is not logic unless i add a relation between category and region
        //public List<Annonce> GetAnnonceByCategoryAndRegion(int idCategory, int idRegion)
        //{
        //    //develop code linq
        //    throw new NotImplementedException();
        //}

        public void CreateAnnonce(Annonce newAnnonce)
        {
           
            using (db = new VientVendreDBContext())
            {

                try
                {

                    db.Users.Attach(newAnnonce.User);
                    foreach (var regInGraph in newAnnonce.Regions)
                    {
                        db.Regions.Attach(regInGraph);
                    }
                    foreach (var prodInNewAnnonce in newAnnonce.Products)
                    {
                        foreach (var catInGraph in prodInNewAnnonce.Categories)

                        {
                            db.Categories.Attach(catInGraph);
                        }
                        db.Entry(prodInNewAnnonce).State = EntityState.Modified;
                        db.Products.Add(prodInNewAnnonce);
                    }



                   // db.Entry(newAnnonce).State = EntityState.Modified;
                    db.Annonces.Add(newAnnonce);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw;
                }

            }
        }

        public void DeleteAnnonce(int id)
        {
            using (VientVendreDBContext db = new VientVendreDBContext())
            {
                var annonce = db.Annonces.FirstOrDefault(a => a.AnnonceID == id);
                db.Annonces.Remove(annonce);
                db.SaveChanges();
            }
        }

        public void UpdateAnnonce(Annonce annonce)
        {
            using (VientVendreDBContext db = new VientVendreDBContext())
            {
                try
                {
                    db.Entry(annonce).State = EntityState.Modified;
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
