using BLL;
using BOL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingBOLAndDTO
{
    //pour le moment le mapping est assuré pour l'envoie de données vers la partie client uniquement.
    public class UserMapping 
    {

        private Map map;
        public UserMapping()
        {
           
        }
        //cette fonction retourne un utilisateur avec sa liste d'annonces et sa liste de commentaire
        public UserDTO UserToUserDTO(User u)
        {
            map = new Map();
            UserDTO userDTO = new UserDTO();
            userDTO.UserID = u.UserID;
            userDTO.UserName = u.UserName;
            userDTO.Password = u.Password;
            userDTO.DateOfBirth = u.DateOfBirth;
            userDTO.Email = u.Email;
            userDTO.Address = u.Address;
            userDTO.sex = u.sex;
            userDTO.PhoneNumber = u.PhoneNumber;
            userDTO.UserLastName = u.UserLastName;
            userDTO.IsApproved = u.IsApproved;

            foreach (var ann in map.baseBs.annonceBs.GetAnnonceByUser(u.UserID))
            {
                AnnonceDTO annonceDTO = new AnnonceDTO();
                annonceDTO.AnnonceTitle = ann.AnnonceTitle;
                annonceDTO.CreatedDate = ann.CreatedDate;
                annonceDTO.Description = ann.Description;

                userDTO.Annonces.Add(annonceDTO);
            }

            foreach (var com in map.baseBs.commentUserProdBs.GetCommentByUser(u.UserID))
            {
                CommentUserProdDTO commentUserProdDTO = new CommentUserProdDTO();
                commentUserProdDTO.CommentUserProdID = com.CommentUserProdID;
                commentUserProdDTO.ProductID = com.ProductID;
                commentUserProdDTO.UserID = com.UserID;
                commentUserProdDTO.Comment = com.Comment;
                commentUserProdDTO.PostDate = com.PostDate;

                userDTO.CommentUserProds.Add(commentUserProdDTO);
            }

            return userDTO;
        }


        public User UserDTOToUser(UserDTO userDto)
        {
            User _user = new User();
            //mapping

            _user.UserID = userDto.UserID;
            _user.UserName = userDto.UserName;
            _user.Password = userDto.Password;
            _user.DateOfBirth = userDto.DateOfBirth;
            _user.Email = userDto.Email;
            _user.Address = userDto.Address;
            _user.sex = userDto.sex;
            _user.PhoneNumber = userDto.PhoneNumber;
            _user.UserLastName = userDto.UserLastName;
            _user.IsApproved = userDto.IsApproved;
            //maybe there is other mapping

            return _user;
        }

    }
}
