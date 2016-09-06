using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VientVendreMVC.Areas.Security.Models;

namespace VientVendreMVC.MappingViewModelToDTO
{
    public class MappingUserViewModelAndUserDTO
    {
        public MappingUserViewModelAndUserDTO()
        {

        }

        public UserDTO MapUserViewModelToUserDTO(UserViewModel userViewModel)
        {
            UserDTO userDTO = new UserDTO();

            userDTO.UserName = userViewModel.FirstName + userViewModel.LastName;
            userDTO.Password = userViewModel.Password;
            //userViewModel.ConfirmPassword;
            userDTO.DateOfBirth = userViewModel.DateOfBirth;
            userDTO.Email = userViewModel.Email;
            userDTO.Address = userViewModel.Address;
            //userViewModel.Sexe;

            return userDTO;

        }

    }
}
