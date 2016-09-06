using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    [DataContract]
    public class User
    {
        public User()
        {
            Annonces = new List<Annonce>();
            CommentUserProds = new List<CommentUserProd>();
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

        public string Role { get; set; }

        //relationship (user, Annonce)=(1:m)
        [DataMember]
        public virtual ICollection<Annonce> Annonces { get; set; } //must be virtual so entityframe work override it
        [DataMember]
        public virtual ICollection<CommentUserProd> CommentUserProds { get; set; }
    }
}
