using BLL;
using BOL;
using DTO;
using MappingBOLAndDTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AdminService
{
    // il faut implementer userDTO on creating user and udating!!!!!!!!!!

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class AdminService : IAdminService
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
        /// <summary>
        /// //////////////////////////////
        /// </summary>
        private AdminAreaBs adminAreaBs;
        private BaseMap baseMap;
        
        public AdminService()
        {
            adminAreaBs = new AdminAreaBs();
            baseMap = new BaseMap();
        }

        public List<UserDTO> GetAllUser()
        {
            var users = adminAreaBs.userBs.GetAllUser();
            List<UserDTO> usersDTO = new List<UserDTO>();
            UserDTO userDTO = new UserDTO();           
            foreach (var user in users)
            {
                userDTO = baseMap.userMapping.UserToUserDTO(user);
                usersDTO.Add(userDTO);
            }

            return usersDTO.ToList();
            
        }

        public UserDTO GetUserByID(int id)
        {
            User a = new User();
            UserDTO b = new UserDTO();
             a= adminAreaBs.userBs.GetUserByID(id);
            b = baseMap.userMapping.UserToUserDTO(a);

            return b;
        }

        public void CreateUser(UserDTO userDTO)
        {
            User user = baseMap.userMapping.UserDTOToUser(userDTO);
            user.Role = "U";
            adminAreaBs.userBs.CreateUser(user);
        }

        public void DeleteUser(int id)
        {
            adminAreaBs.userBs.DeleteUser(id);
        }

        public void UpdateUser(UserDTO userDto)
        {
            var user = adminAreaBs.userBs.GetUserByID(userDto.UserID);
            user.UserID = userDto.UserID;
            user.UserName = userDto.UserName;
            user.Password = userDto.Password;
            user.DateOfBirth = userDto.DateOfBirth;
            user.Email = userDto.Email;
            user.Address = userDto.Address;
            user.sex = userDto.sex;
            user.PhoneNumber = userDto.PhoneNumber;
            user.UserLastName = userDto.UserLastName;
            user.IsApproved = userDto.IsApproved;
            adminAreaBs.userBs.UpdateUser(user);
        }

        ////////Service for annonce

        public List<AnnonceDTO> GetAllAnnonce()
        {
            var annonces = adminAreaBs.annonceBs.GetAllAnnonce();
            List<AnnonceDTO> annoncesDTO = new List<AnnonceDTO>();
            AnnonceDTO annonceDTO = new AnnonceDTO();
            foreach(var annonce in annonces)
            {
                annonceDTO = baseMap.annonceMapping.AnnonceToAnnonceDTO(annonce);

                annoncesDTO.Add(annonceDTO);
            }

            return annoncesDTO;

        }

        public AnnonceDTO GetAnnonceByID(int id)
        {
            AnnonceDTO annonceDTO = new AnnonceDTO();
            Annonce annonce = new Annonce();
            annonce = adminAreaBs.annonceBs.GetAnnonceByID(id);
            annonceDTO = baseMap.annonceMapping.AnnonceToAnnonceDTO(annonce);

            return annonceDTO;

        }

        public List<AnnonceDTO> GetAnnonceByUser(int idUser)
        {
            var annonces = adminAreaBs.annonceBs.GetAnnonceByUser(idUser);
            List<AnnonceDTO> annoncesDTO = new List<AnnonceDTO>();
            AnnonceDTO annonceDTO = new AnnonceDTO();
            foreach (var annonce in annonces)
            {
                annonceDTO = baseMap.annonceMapping.AnnonceToAnnonceDTO(annonce);

                annoncesDTO.Add(annonceDTO);
            }
            return annoncesDTO;
        }

        public List<AnnonceDTO> GetAnnonceByRegion(int idRegion)
        {
            var annonces = adminAreaBs.annonceBs.GetAnnonceByRegion(idRegion);
            List<AnnonceDTO> annoncesDTO = new List<AnnonceDTO>();
            AnnonceDTO annonceDTO = new AnnonceDTO();
            foreach (var annonce in annonces)
            {
                annonceDTO = baseMap.annonceMapping.AnnonceToAnnonceDTO(annonce);

                annoncesDTO.Add(annonceDTO);
            }
            return annoncesDTO;
        }

/// <summary>
/// //////////////////////////////////////////////////////////
/// ///////////////////////////////////////////////////////////:
/// je test l'insertions d'une annonces
/// ///////////////////////////////////////////////////////////
/// </summary>
/// <param name="newAnnonce"></param>
        public void CreateAnnonce(AnnonceDTO newAnnonce1)
        {
            Annonce newAnnonce = new Annonce();
            newAnnonce.AnnonceTitle = newAnnonce1.AnnonceTitle;
            newAnnonce.CreatedDate = newAnnonce1.CreatedDate;
            newAnnonce.Description = newAnnonce1.Description;
            newAnnonce.Regions = new List<Region>();
            newAnnonce.Products = new List<Product>();
            newAnnonce.User = new User();
            int k = 0;
            k= newAnnonce1.UserDTOID != null ? newAnnonce1.UserDTOID.Value : 8;
            newAnnonce.User = adminAreaBs.userBs.GetUserByID(k);
            foreach(var prod in newAnnonce1.Products)
            {
                var p = baseMap.productMapping.ProductDTOToProduct(prod);
                newAnnonce.Products.Add(p);
            }
            
            //var reg = adminAreaBs.regionBs.GetRegionByID(newAnnonce1.RegionsDTOID[1]);
            //var cat = adminAreaBs.categoryBs.GetCategoryByID(2);  
            foreach (var regID in newAnnonce1.RegionsDTOID)
            {

                var reg = adminAreaBs.regionBs.GetRegionByID(regID);
                newAnnonce.Regions.Add(reg);
            }

           
            adminAreaBs.annonceBs.CreateAnnonce(newAnnonce);
        }

/// <summary>
/// ////////////////////////////////////////////////////////
/// /////////////////////////////////////////////////////////
/// </summary>
/// <param name="id"></param>

        public void DeleteAnnonce(int id)
        {
            adminAreaBs.annonceBs.DeleteAnnonce(id);
        }

        public void UpdateAnnonce(AnnonceDTO annonceDTO)
        {
            var annonce = baseMap.annonceMapping.AnnonceDTOToAnnonce(annonceDTO); 
            adminAreaBs.annonceBs.UpdateAnnonce(annonce);
        }

        //////////////////services for products
        public List<ProductDTO> GetAllProduct()
        {
            var products = adminAreaBs.productBs.GetAllProduct();
            List<ProductDTO> productsDTO = new List<ProductDTO>();
            ProductDTO productDTO = new ProductDTO();
            foreach(var product in products)
            {
                productDTO = baseMap.productMapping.ProductToProductDTO(product);
                productsDTO.Add(productDTO);
            }
            return productsDTO;
        }

        public ProductDTO GetProductByID(int id)
        {
            var product = adminAreaBs.productBs.GetProductByID(id);

            ProductDTO productDTO = new ProductDTO();
            productDTO=baseMap.productMapping.ProductToProductDTO(product);

            return productDTO;
        }

        public List<ProductDTO> GetProductByCategory(int idCategory)
        {
            var products = adminAreaBs.productBs.GetProductByCategory(idCategory);
            List<ProductDTO> productsDTO = new List<ProductDTO>();
            ProductDTO productDTO = new ProductDTO();
            foreach (var product in products)
            {
                productDTO = baseMap.productMapping.ProductToProductDTO(product);
                productsDTO.Add(productDTO);
            }
            return productsDTO;
        }

        public List<ProductDTO> GetProductByRegion(int idRegion)
        {
            var products = adminAreaBs.productBs.GetProductByRegion(idRegion);
            List<ProductDTO> productsDTO = new List<ProductDTO>();
            ProductDTO productDTO = new ProductDTO();
            foreach (var product in products)
            {
                productDTO = baseMap.productMapping.ProductToProductDTO(product);
                productsDTO.Add(productDTO);
            }
            return productsDTO;
        }


        public List<ProductDTO> GetProductByCategoryAndRegion(int idCategory, int idRegion)
        {
            var products = adminAreaBs.productBs.GetProductByCategoryAndRegion(idCategory,idRegion);
            List<ProductDTO> productsDTO = new List<ProductDTO>();
            ProductDTO productDTO = new ProductDTO();
            foreach (var product in products)
            {
                productDTO = baseMap.productMapping.ProductToProductDTO(product);
                productsDTO.Add(productDTO);
            }
            return productsDTO;
        }

        public List<ProductDTO> GetProductByAnnonce(int annonceID)
        {
            var products = adminAreaBs.productBs.GetProductByAnnonce(annonceID);
            List<ProductDTO> productsDTO = new List<ProductDTO>();
            ProductDTO productDTO = new ProductDTO();
            foreach (var product in products)
            {
                productDTO = baseMap.productMapping.ProductToProductDTO(product);
                productsDTO.Add(productDTO);
            }
            return productsDTO;
        }
        public List<ProductDTO> GetProductByPrice(double MinPrice, double MaxPrice)
        {
            var products = adminAreaBs.productBs.GetProductByPrice(MinPrice, MaxPrice);
            List<ProductDTO> productsDTO = new List<ProductDTO>();
            ProductDTO productDTO = new ProductDTO();
            foreach (var product in products)
            {
                productDTO = baseMap.productMapping.ProductToProductDTO(product);
                productsDTO.Add(productDTO);
            }
            return productsDTO;
        }

        public List<ProductDTO> GetProductByPriceAndRegion(double MinPrice, double MaxPrice, int idRegion)
        {
            var products = adminAreaBs.productBs.GetProductByPriceAndRegion(MinPrice, MaxPrice,idRegion);
            List<ProductDTO> productsDTO = new List<ProductDTO>();
            ProductDTO productDTO = new ProductDTO();
            foreach (var product in products)
            {
                productDTO = baseMap.productMapping.ProductToProductDTO(product);
                productsDTO.Add(productDTO);
            }
            return productsDTO;
        }

        public List<ProductDTO> GetProductByPriceAndCategory(double MinPrice, double MaxPrice, int idCategory)
        {
            var products = adminAreaBs.productBs.GetProductByPriceAndCategory(MinPrice, MaxPrice,idCategory);
            List<ProductDTO> productsDTO = new List<ProductDTO>();
            ProductDTO productDTO = new ProductDTO();
            foreach (var product in products)
            {
                productDTO = baseMap.productMapping.ProductToProductDTO(product);
                productsDTO.Add(productDTO);
            }
            return productsDTO;
        }

        public List<ProductDTO> GetProductByPriceAndRegionAndCategory(double MinPrice, double MaxPrice, int idRegion, int idCategory)
        {
            var products = adminAreaBs.productBs.GetProductByPriceAndRegionAndCategory(MinPrice, MaxPrice, idRegion, idCategory);
            List<ProductDTO> productsDTO = new List<ProductDTO>();
            ProductDTO productDTO = new ProductDTO();
            foreach (var product in products)
            {
                productDTO = baseMap.productMapping.ProductToProductDTO(product);
                productsDTO.Add(productDTO);
            }
            return productsDTO;
        }
/// <summary>
/// //////////////////////////////////////////////////////////////////////
/// /////////////////////////////////////////////////////////////////////:::
/// ///////////////////////////////////////////////////////////////////////:
/// //////////////////////////////////////////////////
/// </summary>
/// <param name="newProduct"></param>
        public void CreateProduct(ProductDTO newProduct)
        {
            Product _product = new Product();
            _product.Categories = new List<Category>();
            _product=baseMap.productMapping.ProductDTOToProduct(newProduct);
            
            adminAreaBs.productBs.CreateProduct(_product);
        }

        public void DeleteProduct(int id)
        {
            adminAreaBs.productBs.DeleteProduct(id);
        }

        public void UpdateProduct(ProductDTO productDto)
        {
            var prod = adminAreaBs.productBs.GetProductByID(productDto.ProductID);
            prod.ProductName = productDto.ProductName;
            prod.ProductPrice = productDto.ProductPrice;
            prod.IsApproved = productDto.IsApproved;
            
            
             adminAreaBs.productBs.UpdateProduct(prod);
        }

        /////////////services for Category
        public List<CategoryDTO> GetAllCategory()
        {
            var categories = adminAreaBs.categoryBs.GetAllCategory();
            List<CategoryDTO> categoriesDTO = new List<CategoryDTO>();
            CategoryDTO categoryDTO = new CategoryDTO(); 
            foreach(var category in categories)
            {
                categoryDTO=baseMap.categoryMapping.CategoryToCategoryDTO(category);
                categoriesDTO.Add(categoryDTO);
            }
            return categoriesDTO;
        }

        public CategoryDTO GetCategoryByID(int id)
        {
            var category=adminAreaBs.categoryBs.GetCategoryByID(id);
            CategoryDTO categoryDTO = new CategoryDTO();
            categoryDTO = baseMap.categoryMapping.CategoryToCategoryDTO(category);

            return categoryDTO;
        }


        public void CreateCategory(CategoryDTO categoryDto)
        {
            Category category = new Category();
            category = baseMap.categoryMapping.categoryDTOTocategory(categoryDto);
                adminAreaBs.categoryBs.CreateCategory(category);
        }

        public void DeleteCategory(int id)
        {
            adminAreaBs.categoryBs.DeleteCategory(id);
        }

        public void UpdateCategory(CategoryDTO categoryDto)
        {
            var cat = adminAreaBs.categoryBs.GetCategoryByID(categoryDto.CategoryID);
            cat.CategoryName = categoryDto.CategoryName;
               adminAreaBs.categoryBs.UpdateCategory(cat);
        }

        ////services for regions

        public List<RegionDTO> GetAllRegion()
        {
            var regions=adminAreaBs.regionBs.GetAllRegion();
            List<RegionDTO> regionsDTO = new List<RegionDTO>();
            RegionDTO regionDTO = new RegionDTO();
            foreach(var region in regions)
            {
                regionDTO = baseMap.regionMapping.RegionToRegionDTO(region);
                regionsDTO.Add(regionDTO);
            }

            return regionsDTO;
        }

        public RegionDTO GetRegionByID(int id)
        {
            var region = adminAreaBs.regionBs.GetRegionByID(id);
            RegionDTO regionDTO = new RegionDTO();

            regionDTO = baseMap.regionMapping.RegionToRegionDTO(region);


            return regionDTO;
        }

        public void CreateRegion(RegionDTO regionDto)
        {
            var region=baseMap.regionMapping.regionDTOToRegion(regionDto);
            adminAreaBs.regionBs.CreateRegion(region);
        }

        public void DeleteRegion(int id)
        {
            adminAreaBs.regionBs.DeleteRegion(id);
        }

        public void UpdateRegion(RegionDTO regionDto)
        {
            var region = adminAreaBs.regionBs.GetRegionByID(regionDto.RegionID);
            region.RegionName = regionDto.RegionName;           
               adminAreaBs.regionBs.UpdateRegion(region);
        }
        public List<CategoryDTO> GetCategoriesByProduct(int id)
        {
            var cats= adminAreaBs.categoryBs.GetCategoryByProduct(id);
            List<CategoryDTO> catsDTO = new List<CategoryDTO>();
            CategoryDTO catDTO = new CategoryDTO();
            foreach(var cat in cats)
            {
                catDTO = baseMap.categoryMapping.CategoryToCategoryDTO(cat);
                catsDTO.Add(catDTO);
            }
            return catsDTO;
        }
        public List<RegionDTO> GetRegionByAnnonce(int id)
        {
            
            var regs = adminAreaBs.regionBs.GetRegionByAnnonce(id);
            List<RegionDTO> regsDTO = new List<RegionDTO>();
            RegionDTO regDTO = new RegionDTO();
            foreach (var reg in regs)
            {
                regDTO = baseMap.regionMapping.RegionToRegionDTO(reg);
                regsDTO.Add(regDTO);
            }
            return regsDTO;
        }

        /// <summary>
        /// ///////////////////////
        /// photo Info services
        /// </summary>
        /// <returns></returns>
        public List<PhotoInfoDTO> GetAllPhotoInfo()
        {
            List<PhotoInfoDTO> photosDTO = new List<PhotoInfoDTO>();
            foreach(var photo in adminAreaBs.photoInfoBs.GetAllPhoto())
            {
                var photoDTO = baseMap.photoInfoMapping.PhotoInfoToPhotoInfoDTO(photo);
                photosDTO.Add(photoDTO);
            }
            return photosDTO;
        }

        public PhotoInfoDTO GetPhotoInfoById(int IdPhoto)
        {
            var photo = adminAreaBs.photoInfoBs.GetPhotoByID(IdPhoto);
            var photoDTO = baseMap.photoInfoMapping.PhotoInfoToPhotoInfoDTO(photo);
              
            return photoDTO;
        }

        public void CreatePhotoInfo(PhotoInfoDTO photoDTO)
        {
            
             var photo = baseMap.photoInfoMapping.PhotoInfoDTOToPhotoInfo(photoDTO);
            adminAreaBs.photoInfoBs.CreatePhoto(photo);
            
        }

        public void DeletePhotoInfo(int id)
        {

            adminAreaBs.photoInfoBs.DeletePhoto(id);

        }

        public void UpdatePhotoInfo(PhotoInfoDTO photoDTO)
        {
            var photo = baseMap.photoInfoMapping.PhotoInfoDTOToPhotoInfo(photoDTO);
            adminAreaBs.photoInfoBs.UpdatePhoto(photo);
        }



        ///////////////////
        //get image
        ///////////////////
        [WebGet(UriTemplate = "GetImage/{user}/{annonce}/{fileName}")]
        public Stream GetImage(string user,string annonce,string fileName)
        {
            //FileStream fs = File.OpenRead(@"C:\Users\dridi\Desktop\Gorboj\ImageUpload\ImageUpload\FileUpload\WIN_20150312_205851.jpg");
            FileStream fs = File.OpenRead(@"C:\Users\dridi\Desktop\projet PCD\projet\solution41\VientVendre\ImageUpload\FileUpload\" + user+"\\"+annonce+"\\"+fileName);
            WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpeg";
            return fs;
        }





        /////////////////logique pour site
        public void ApproveOrRejectAll(List<int> Ids, string status)
        {
            adminAreaBs.ApproveOrReject(Ids, status);
        }


        public List<CommentUserProdDTO> GetAllComment()
        {
            var commList= adminAreaBs.commentUserProdBs.GetAllComment();
            List<CommentUserProdDTO> commsDTO = new List<CommentUserProdDTO>();
            foreach(var comm in commList)
            {
                CommentUserProdDTO commDTO = new CommentUserProdDTO();
                commDTO.Comment = comm.Comment;
                commDTO.UserID = comm.UserID;
                commDTO.ProductID = comm.ProductID;
                commDTO.PostDate = comm.PostDate;
                commsDTO.Add(commDTO);
            }
            return commsDTO;
        }


        public void createComment(CommentUserProdDTO commDTO)
        {
            CommentUserProd comm = new CommentUserProd();
            comm.Comment = commDTO.Comment;
            comm.PostDate = comm.PostDate;
            comm.User = new User();
            int k = commDTO.UserID == null ? 1 : commDTO.UserID.Value;
            comm.User = adminAreaBs.userBs.GetUserByID(k);
            k= commDTO.ProductID == null ? 1 : commDTO.ProductID.Value;
            comm.Product = adminAreaBs.productBs.GetProductByID(k);

            adminAreaBs.commentUserProdBs.CreateComment(comm);
        }
    }

}
