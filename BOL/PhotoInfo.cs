using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    [DataContract]
    public class PhotoInfo
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
        [DataMember]
        public virtual Product Product {get ; set ; }
    }
}
