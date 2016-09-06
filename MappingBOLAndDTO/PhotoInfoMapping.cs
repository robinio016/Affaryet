using BLL;
using BOL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingBOLAndDTO
{
    public class PhotoInfoMapping
    {
        private Map map;
        private AdminAreaBs adminAreaBs;

        public PhotoInfoMapping()
        {
        }
        public PhotoInfoDTO PhotoInfoToPhotoInfoDTO(PhotoInfo photo)
        {
            PhotoInfoDTO photoDTO = new PhotoInfoDTO();
            photoDTO.PhotoName = photo.PhotoName;
            photoDTO.ProductID = photo.ProductID;
            photoDTO.UploadedOn = photo.UploadedOn;
            photoDTO.PhotoInfoID = photo.PhotoInfoID;
            photoDTO.Description = photo.Description;

            photoDTO.Path = photo.Path;

            return photoDTO;
        }

        public PhotoInfo PhotoInfoDTOToPhotoInfo(PhotoInfoDTO photoDTO)
        {
            PhotoInfo photo = new PhotoInfo();
            photo.PhotoName = photoDTO.PhotoName;
            photo.ProductID = photoDTO.ProductID;
            photo.UploadedOn = photoDTO.UploadedOn;
            photo.PhotoInfoID = photoDTO.PhotoInfoID;
            photo.Description = photoDTO.Description;

            photoDTO.Path = photo.Path;

            return photo;
        }
    }
}        

