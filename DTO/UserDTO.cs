using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    [DataContract]
    public class UserDTO
    {
        public UserDTO()
        {
            Annonces = new List<AnnonceDTO>();
            CommentUserProds = new List<CommentUserProdDTO>();
        }
        [DataMember]
        public int UserID { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string UserLastName { get; set; }
        [DataMember]
        public char sex { get; set; }
        [DataMember]
        public int PhoneNumber { get; set; }
        [DataMember]
        public DateTime DateOfBirth { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string IsApproved { get; set; }
        [DataMember]
        public  List<AnnonceDTO> Annonces { get; set; }
        [DataMember]
        public List<CommentUserProdDTO> CommentUserProds { get; set; }
    }
}
