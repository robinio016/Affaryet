using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    [DataContract]
    public class CategoryDTO
    {
        public CategoryDTO()
        {
            Products = new List<ProductDTO>();
        }
        [DataMember]
        public int CategoryID { get; set; }
        [DataMember]
        public string CategoryName { get; set; }
        [DataMember]
        public List<ProductDTO> Products { get; set; }
    }
}
