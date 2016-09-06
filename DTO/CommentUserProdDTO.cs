using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    [DataContract]
    public class CommentUserProdDTO
    {
        [DataMember]
        public int CommentUserProdID { get; set; }
        [DataMember]
        public int? UserID { get; set; }
        [DataMember]
        public virtual UserDTO User { get; set; }
        [DataMember]
        public int? ProductID { get; set; }
        [DataMember]
        public virtual ProductDTO Product { get; set; }
        [DataMember]
        public string Comment { get; set; }
        [DataMember]
        public DateTime? PostDate { get; set; }
    }
}
