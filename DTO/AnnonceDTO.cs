using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    [DataContract]
    public class AnnonceDTO
    {
        public AnnonceDTO()
        {
            UserDTO = new UserDTO();
            Products = new List<ProductDTO>();
            Regions = new List<RegionDTO>();
            RegionsDTOID = new List<int>();
        }
        [DataMember]
        public int AnnonceID { get; set; }
        [DataMember]
        public string AnnonceTitle { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int? UserDTOID { get; set; }
        [DataMember]
        public  UserDTO UserDTO { get; set; }
        [DataMember]

        public List<int> RegionsDTOID { get; set; }
        [DataMember]
        public List<ProductDTO> Products { get; set; }
        [DataMember]
        public  List<RegionDTO> Regions { get; set; }

    }
}
