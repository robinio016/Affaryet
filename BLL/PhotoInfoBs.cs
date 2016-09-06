using BOL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PhotoInfoBs
    {
        private PhotoInfoProvider photoInfoProvider;
        public PhotoInfoBs()
        {
            photoInfoProvider = new PhotoInfoProvider();

        }

        public List<PhotoInfo> GetAllPhoto()
        {
            return photoInfoProvider.GetAllPhoto();
        }

        public PhotoInfo GetPhotoByID(int id)
        {
            return photoInfoProvider.GetPhotoByID(id);
        }
        
        public List<PhotoInfo> GetPhotoByProduct(int productId)
        {
            return photoInfoProvider.GetPhotoByProduct(productId);
        }

        public void CreatePhoto(PhotoInfo photo)
        {
            photoInfoProvider.CreatePhoto(photo);
        }

        public void DeletePhoto(int id)
        {
            photoInfoProvider.DeletePhoto(id);
        }

        public void UpdatePhoto(PhotoInfo photo)
        {
            photoInfoProvider.UpdatePhoto(photo);
        }
    }
}
