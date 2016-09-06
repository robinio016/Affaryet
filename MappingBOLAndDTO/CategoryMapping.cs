using BLL;
using BOL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingBOLAndDTO
{//pour le moment le mapping est assuré pour l'envoie de données vers la partie client uniquement.
    public class CategoryMapping
    {
        private Map map;
        public CategoryMapping()
        {
           
        }

        public CategoryDTO CategoryToCategoryDTO(Category c)
        {
            map = new Map();
            CategoryDTO categoryDTO = new CategoryDTO();
            categoryDTO.CategoryID = c.CategoryID;
            categoryDTO.CategoryName = c.CategoryName;

            foreach(var prod in map.baseBs.productBs.GetProductByCategory(c.CategoryID))
            {
                ProductDTO productDTO = new ProductDTO();
                productDTO.ProductID = prod.ProductID;
                productDTO.ProductName = prod.ProductName;
                productDTO.ProductPrice = prod.ProductPrice;

                categoryDTO.Products.Add(productDTO);
            }
            return categoryDTO;
        }

        public Category categoryDTOTocategory(CategoryDTO categoryDto)
        {
            Category _category = new Category();
            //mapping
            return _category;
        }
    }
}
