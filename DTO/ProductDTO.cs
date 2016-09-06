using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    [DataContract]
    public class ProductDTO
    {
        public ProductDTO()
        {
            Annonce = new AnnonceDTO();
            Categories = new List<CategoryDTO>();
            PhotoInfos = new List<PhotoInfoDTO>();
            CategoriesID = new List<int>();
            CommentUserProds = new List<CommentUserProdDTO>();
        }
        [DataMember]
        public int ProductID { get; set; }
        [DataMember]
        public string ProductName { get; set; }
        [DataMember]
        public double ProductPrice { get; set; }
        [DataMember]
        public  AnnonceDTO Annonce { get; set; }
        [DataMember]
        public int AnnonceDTOID { get; set; }
        [DataMember]
        public  List<CategoryDTO> Categories { get; set; }
        [DataMember]
        public List<int> CategoriesID { get; set; } 
        [DataMember]
        public List<PhotoInfoDTO> PhotoInfos { get; set; }
        [DataMember]
        public string IsApproved { get; set; }

        public List<CommentUserProdDTO> CommentUserProds { get; set; }

    }
}
