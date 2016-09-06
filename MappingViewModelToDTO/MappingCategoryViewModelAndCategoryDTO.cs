using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VientVendreMVC.Areas.Admin.Models;

namespace VientVendreMVC.MappingViewModelToDTO
{
    public class MappingCategoryViewModelAndCategoryDTO 
    {
        public MappingCategoryViewModelAndCategoryDTO()
        {

        }

        public CategoryDTO MapCategoryViewModelToCategoryDTO(CategoryViewModel categoryViewModel)
        {
            CategoryDTO categoryDTO = new CategoryDTO();

            categoryDTO.CategoryName = categoryViewModel.CategoryName;
            return categoryDTO;
        }
    }
}
