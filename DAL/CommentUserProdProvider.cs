using BOL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CommentUserProdProvider
    {
        public CommentUserProdProvider()
        {
        }

        public List<CommentUserProd> GetAllComment()
        {
            using (VientVendreDBContext db = new VientVendreDBContext())
            {
                var res = db.CommentUserProds.ToList();
                return res;
            }
        }

        public CommentUserProd GetCommentByID(int id)
        {
            using (VientVendreDBContext db = new VientVendreDBContext())
            {
                return db.CommentUserProds.Find(id);
            }
        }

        public List<CommentUserProd> GetCommentByUser(int UserId)
        {
            using (VientVendreDBContext db = new VientVendreDBContext())
            {

                try
                {
                    var query = from u in db.Users
                                from com in u.CommentUserProds
                                where com.UserID == UserId
                                select com;
                    return query.ToList();
                }
                catch
                {
                    throw;
                }
            }
        }
        public List<CommentUserProd> GetCommentByProduct(int ProductId)
        {
            using (VientVendreDBContext db = new VientVendreDBContext())
            {

                try
                {
                    var query = from u in db.Products
                                from com in u.CommentUserProds
                                where com.UserID == ProductId
                                select com;
                    return query.ToList();
                }
                catch
                {
                    throw;
                }
            }
        }

        public void CreateComment(CommentUserProd commentUserProd)
        {
            using (VientVendreDBContext db = new VientVendreDBContext())
            {
                db.Users.Attach(commentUserProd.User);
                db.Products.Attach(commentUserProd.Product);
                db.CommentUserProds.Add(commentUserProd);
                db.SaveChanges();
            }
        }

        public void DeleteComment(int id)
        {
            using (VientVendreDBContext db = new VientVendreDBContext())
            {
                var commentUserProd = db.CommentUserProds.Find(id);
                db.CommentUserProds.Remove(commentUserProd);
                db.SaveChanges();
            }
        }

        public void UpdateComment(CommentUserProd commentUserProd)
        {
            using (VientVendreDBContext db = new VientVendreDBContext())
            {
                try
                {
                    db.Entry(commentUserProd).State = EntityState.Modified;
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
