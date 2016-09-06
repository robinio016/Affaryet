using BLL;
using BOL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingBOLAndDTO
{//pour le moment le mapping est assuré pour l'envoie de données vers la partie client uniquement.
    public class ProductMapping 
    {
        private Map map;
        private AdminAreaBs adminAreaBs;
        public ProductMapping()
        {
            
        }

        public ProductDTO ProductToProductDTO(Product p)
        {
            ProductDTO productDTO = new ProductDTO();
         
            productDTO.ProductID = p.ProductID;
            productDTO.ProductName = p.ProductName;
            productDTO.ProductPrice = p.ProductPrice;
            productDTO.IsApproved = p.IsApproved;
            if(p.AnnonceID != null)
                productDTO.AnnonceDTOID = p.AnnonceID.Value;
            //if (p.AnnonceID != null)
            // p.Annonce = map.baseBs.annonceBs.GetAnnonceByID(p.AnnonceID.Value);



            //foreach (var com in map.baseBs.commentUserProdBs.GetCommentByProduct(p.ProductID))
            //{
            //    CommentUserProdDTO commentUserProdDTO = new CommentUserProdDTO();
            //    commentUserProdDTO.CommentUserProdID = com.CommentUserProdID;
            //    commentUserProdDTO.ProductID = com.ProductID;
            //    commentUserProdDTO.UserID = com.UserID;
            //    commentUserProdDTO.Comment = com.Comment;
            //    commentUserProdDTO.PostDate = com.PostDate;

            //    productDTO.CommentUserProds.Add(commentUserProdDTO);
            //}




            ////////////////////////////////////////////////////////////////////////
            //retourner les categories auxquelles le produit appartient
            //////////////////////////////////////////////////////////////////////////
            //foreach (var cat in map.baseBs.categoryBs.GetCategoryByProduct(productDTO.ProductID).Distinct())

            //{
            //    CategoryDTO categoryDTO = new CategoryDTO();
            //    categoryDTO.CategoryName = cat.CategoryName;
            //    categoryDTO.CategoryID = cat.CategoryID;

            //    productDTO.Categories.Add(categoryDTO);
            //}
            ////////////////////////////////////////////////////////////////////////////
            return productDTO;
        }

        public Product ProductDTOToProduct(ProductDTO productDto)
        {
            adminAreaBs = new AdminAreaBs();
            Product _product =new Product();
            map = new Map();
            //mapping
            _product.ProductName= productDto.ProductName;
            _product.ProductPrice= productDto.ProductPrice;
            //_product.AnnonceID = adminAreaBs.annonceBs.GetAnnonceByID(id).AnnonceID;
            _product.AnnonceID = adminAreaBs.annonceBs.GetAllAnnonce().Last().AnnonceID+1; //pour creation car ce nest pas une transaction j'ajoute une annonce puis je relie chaque produit au dernier annonce
            //_product.AnnonceID = adminAreaBs.annonceBs.GetAllAnnonce().Last().AnnonceID;
            _product.IsApproved = productDto.IsApproved;
            foreach (int  catID in productDto.CategoriesID)
            {
                   Category category = adminAreaBs.categoryBs.GetCategoryByID(catID);
                _product.Categories.Add(category);
            }

            foreach (var photoDTO in productDto.PhotoInfos)
            {
                
                PhotoInfo photo = map.photoInfoMapping.PhotoInfoDTOToPhotoInfo(photoDTO);
                _product.PhotoInfos.Add(photo);
            }



            return _product;
        }

    } 
}
