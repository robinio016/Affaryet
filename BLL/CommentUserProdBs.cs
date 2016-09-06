using BOL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CommentUserProdBs
    {
        private CommentUserProdProvider commentUserProdProvider;
        public CommentUserProdBs()
        {
            commentUserProdProvider = new CommentUserProdProvider();

        }

        public List<CommentUserProd> GetAllComment()
        {
            return commentUserProdProvider.GetAllComment();
        }

        public CommentUserProd GetCommentByID(int id)
        {
            return commentUserProdProvider.GetCommentByID(id);
        }
        public List<CommentUserProd> GetCommentByUser(int userId)
        {
            return commentUserProdProvider.GetCommentByUser(userId);
        }
        public List<CommentUserProd> GetCommentByProduct(int productId)
        {
            return commentUserProdProvider.GetCommentByProduct(productId);
        }

        public void CreateComment(CommentUserProd comment)
        {
            commentUserProdProvider.CreateComment(comment);
        }

        public void DeleteComment(int id)
        {
            commentUserProdProvider.DeleteComment(id);
        }

        public void UpdateComment(CommentUserProd comment)
        {
            commentUserProdProvider.UpdateComment(comment);
        }
    }
}
