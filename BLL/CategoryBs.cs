using BOL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CategoryBs
    {
        private CategoryProvider categoryProvider;
        public CategoryBs()
        {
            categoryProvider = new CategoryProvider();

        }

        public List<Category> GetAllCategory()
        {
            return categoryProvider.GetAllCategory();
        }

        public Category GetCategoryByID(int id)
        {
            return categoryProvider.GetCategoryByID(id);
        }
        public List<Category> GetCategoryByProduct(int productID)
        {
            return categoryProvider.GetCategoryByProduct(productID);
        }

        public void CreateCategory(Category category)
        {
            categoryProvider.CreateCategory(category);
        }

        public void DeleteCategory(int id)
        {
            categoryProvider.DeleteCategory(id);
        }

        public void UpdateCategory(Category category)
        {
            categoryProvider.UpdateCategory(category);
        }
    }
}
