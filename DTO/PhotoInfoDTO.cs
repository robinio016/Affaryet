using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PhotoInfoDTO
    {
        [DataMember]
        public int PhotoInfoID { get; set; }

        [DataMember]
        public string PhotoName { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public DateTime UploadedOn { get; set; }

        [DataMember]
        public string Path { get; set; }

        [DataMember]
        public int? ProductID { get; set; }
        
    }
}
