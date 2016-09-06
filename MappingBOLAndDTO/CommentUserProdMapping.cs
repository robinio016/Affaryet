using BLL;
using BOL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingBOLAndDTO
{
    public class CommentUserProdMapping
    {
        private Map map;
        private AdminAreaBs adminAreaBs;
        public CommentUserProdMapping()
        {

        }
        public CommentUserProdDTO CommentUserProdToCommentUserProdDDTO(CommentUserProd comment)
        {
            map = new Map();
            CommentUserProdDTO commentUserProdDTO = new CommentUserProdDTO();
            commentUserProdDTO.CommentUserProdID = comment.CommentUserProdID;
            commentUserProdDTO.ProductID = comment.ProductID;
            commentUserProdDTO.UserID = comment.UserID;
            commentUserProdDTO.Comment = comment.Comment;
            commentUserProdDTO.PostDate = comment.PostDate;

            return commentUserProdDTO;
        }

        public CommentUserProd CommentUserProdDTOToCommentUserProd(CommentUserProdDTO commentDTO)
        {
            CommentUserProd _comment = new CommentUserProd();
            _comment.CommentUserProdID = commentDTO.CommentUserProdID;
            _comment.ProductID = commentDTO.ProductID;
            _comment.UserID = commentDTO.UserID;
            _comment.Comment = commentDTO.Comment;
            _comment.PostDate = commentDTO.PostDate;

            return _comment;
        }


    }
}
