using BOL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ProductBs
    {
        ProductProvider productProvider;
        public ProductBs()
        {
            productProvider = new ProductProvider();
        }

        public List<Product> GetAllProduct()
        {
            return productProvider.GetAllProduct();
        }

        public Product GetProductByID(int id)
        {
            return productProvider.GetProductByID(id);
        }

        public List<Product> GetProductByCategory(int idCategory)
        {
            return productProvider.GetProductByCategory(idCategory);
        }

        public List<Product> GetProductByRegion(int idRegion)
        {
            return productProvider.GetProductByRegion(idRegion);
        }


        public List<Product> GetProductByCategoryAndRegion(int idCategory, int idRegion)
        {
            return productProvider.GetProductByCategoryAndRegion(idCategory,idRegion);
        }

        public List<Product> GetProductByAnnonce(int annonceID)
        {
            return productProvider.GetProductByAnnonce(annonceID);
        }
        public List<Product> GetProductByPrice(double MinPrice, double MaxPrice)
        {
            return productProvider.GetProductByPrice(MinPrice, MaxPrice);
        }

        public List<Product> GetProductByPriceAndRegion(double MinPrice, double MaxPrice, int idRegion)
        {
            return productProvider.GetProductByPriceAndRegion(MinPrice, MaxPrice, idRegion);
        }

        public List<Product> GetProductByPriceAndCategory(double MinPrice, double MaxPrice, int idCategory)
        {
            return productProvider.GetProductByPriceAndCategory(MinPrice, MaxPrice, idCategory);
        }

        public List<Product> GetProductByPriceAndRegionAndCategory(double MinPrice, double MaxPrice, int idRegion, int idCategory)
        {
            return productProvider.GetProductByPriceAndRegionAndCategory(MinPrice, MaxPrice, idRegion, idCategory);
        }

        public void CreateProduct(Product newProduct)
        {
            productProvider.CreateProduct(newProduct);
        }

        public void DeleteProduct(int id)
        {
            productProvider.DeleteProduct(id);
        }

        public void UpdateProduct(Product product)
        {
            productProvider.UpdateProduct(product);
        }
    }
}
