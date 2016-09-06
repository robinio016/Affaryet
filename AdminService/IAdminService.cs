using BOL;
using DTO;
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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IAdminService
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
        [OperationContract]
        List<UserDTO> GetAllUser();

        [OperationContract]
        UserDTO GetUserByID(int id);

        [OperationContract]
        void CreateUser(UserDTO user);

        [OperationContract]
        void DeleteUser(int id);

        [OperationContract]
        void UpdateUser(UserDTO user);

        ///////service for contract
        [OperationContract]
        List<AnnonceDTO> GetAllAnnonce();
        [OperationContract]
        AnnonceDTO GetAnnonceByID(int id);

        [OperationContract]
        List<AnnonceDTO> GetAnnonceByUser(int idUser);
        [OperationContract]
        List<AnnonceDTO> GetAnnonceByRegion(int idRegion);

        [OperationContract]
        void CreateAnnonce(AnnonceDTO newAnnonce);

        [OperationContract]
        void DeleteAnnonce(int id);

        [OperationContract]
        void UpdateAnnonce(AnnonceDTO annonce);

        ///////services for products
        [OperationContract]
        List<ProductDTO> GetAllProduct();

        [OperationContract]
        ProductDTO GetProductByID(int id);

        [OperationContract]
        List<ProductDTO> GetProductByCategory(int idCategory);

        [OperationContract]
        List<ProductDTO> GetProductByRegion(int idRegion);


        [OperationContract]
        List<ProductDTO> GetProductByCategoryAndRegion(int idCategory, int idRegion);

        [OperationContract]
        List<ProductDTO> GetProductByAnnonce(int annonceID);
        [OperationContract]
        List<ProductDTO> GetProductByPrice(double MinPrice, double MaxPrice);
        [OperationContract]
        List<ProductDTO> GetProductByPriceAndRegion(double MinPrice, double MaxPrice, int idRegion);

        [OperationContract]
        List<ProductDTO> GetProductByPriceAndCategory(double MinPrice, double MaxPrice, int idCategory);

        [OperationContract]
        List<ProductDTO> GetProductByPriceAndRegionAndCategory(double MinPrice, double MaxPrice, int idRegion, int idCategory);

        [OperationContract]
        void CreateProduct(ProductDTO newProduct);

        [OperationContract]
        void DeleteProduct(int id);

        [OperationContract]
        void UpdateProduct(ProductDTO product);

        ////////services for category
        [OperationContract]
        List<CategoryDTO> GetAllCategory();

        [OperationContract]
        CategoryDTO GetCategoryByID(int id);


        [OperationContract]
        void CreateCategory(CategoryDTO category);

        [OperationContract]
        void DeleteCategory(int id);

        [OperationContract]
        void UpdateCategory(CategoryDTO category);


        ///////services for regions
        [OperationContract]
        List<RegionDTO> GetAllRegion();


        [OperationContract]
        RegionDTO GetRegionByID(int id);

        [OperationContract]
        void CreateRegion(RegionDTO region);

        [OperationContract]
        void DeleteRegion(int id);

        [OperationContract]
        void UpdateRegion(RegionDTO region);

        [OperationContract]
        List<PhotoInfoDTO> GetAllPhotoInfo();

        [OperationContract]
        PhotoInfoDTO GetPhotoInfoById(int IdPhoto);
        [OperationContract]
        void CreatePhotoInfo(PhotoInfoDTO photoDTO);
        [OperationContract]
        void DeletePhotoInfo(int id);
        [OperationContract]
        void UpdatePhotoInfo(PhotoInfoDTO photoDTO);
        /////////////////////////////
        //my contract for getting an image
        [OperationContract]
        [WebGet(UriTemplate = "GetImage/{user}/{annonce}/{fileName}", RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare)]
        Stream GetImage(string user, string annonce, string fileName);



        /////////////////////logique pour site

        [OperationContract]
        void ApproveOrRejectAll(List<int> Ids, string status);

        [OperationContract]
        List<CommentUserProdDTO> GetAllComment();
        [OperationContract]
        void createComment(CommentUserProdDTO commDTO);

    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "AdminService.ContractType".
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }

    //[DataContract]
    //public class UserDto
    //{


    //    [DataMember]
    //    public int UserID { get; set; }
    //    [DataMember]
    //    public string UserName { get; set; }
    //    [DataMember]
    //    public DateTime DateOfBirth { get; set; }
    //    [DataMember]
    //    public string Address { get; set; }
    //    [DataMember]
    //    public string Email { get; set; }
    //    [DataMember]
    //    public string Password { get; set; }

    //    //relationship (user, Annonce)=(1:m)
    //    [DataMember]
    //    public IList<Annonce> Annonces { get; set; }
    //}
}
