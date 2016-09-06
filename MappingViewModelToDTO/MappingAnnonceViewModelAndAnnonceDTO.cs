using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VientVendreMVC.Areas.User.Models;

namespace VientVendreMVC.MappingViewModelToDTO
{
    public class MappingAnnonceViewModelAndAnnonceDTO
    {
        public MappingAnnonceViewModelAndAnnonceDTO()
        {

        }

        public AnnonceDTO MapAnnonceViewModelToAnnonceDTO(AnnonceViewModel annonceViewModel)
        {
            AnnonceDTO annonceDTO = new AnnonceDTO();

            annonceDTO.AnnonceTitle = annonceViewModel.AnnonceTitle;
            annonceDTO.CreatedDate = DateTime.Now;
            annonceDTO.Description = annonceViewModel.Description;
            //annonceDTO.UserDTOID 
            foreach(int i in annonceViewModel.RegionsID)
            {
                annonceViewModel.RegionsID.Add(i);
            }
                         
            return annonceDTO;


        }
    }
}
