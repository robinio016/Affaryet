using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VientVendreMVC.Areas.User.Models;

namespace VientVendreMVC.MappingViewModelToDTO
{
    public class MappingProductViewModelAndProductDTO
    {
        public MappingProductViewModelAndProductDTO()
        {

        }

        public ProductDTO MapProductViewModelToProductDTO(ProductViewModel productViewModel)
        {
            ProductDTO productDTO = new ProductDTO();

            productDTO.ProductName=productViewModel.ProductName;
            productDTO.ProductPrice = productViewModel.ProductPrice;
            //productDTO.Annonce.AnnonceID  must be complited to add product to annonce
            //foreach(int i in productViewModel.CategoriesID)
            //{
            //    productDTO.CategoriesID.Add(i);
            //}

            return productDTO;
        }
    }
}
