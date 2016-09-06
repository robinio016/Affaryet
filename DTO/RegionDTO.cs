using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    [DataContract]
    public class RegionDTO
    {
        public RegionDTO()
        {
            Annonces = new List<AnnonceDTO>();
        }
        [DataMember]
        public int RegionID { get; set; }
        [DataMember]
        public string RegionName { get; set; }
        [DataMember]
        public List<AnnonceDTO> Annonces { get; set; }
    }
}
