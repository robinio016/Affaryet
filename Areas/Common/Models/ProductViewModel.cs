using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VientVendreMVC.Areas.Common.Models
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            Categories = new List<string>();
            comm = new List<CommentUserProdDTO>();
            UserName = new List<string>();
            regionsName = new List<string>();
        }
        public int ProductID { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public double ProductPrice { get; set; }
        [Required]
        public string Description { get; set; }
        public List<string> Categories { get; set; }

        public List<int> CategoriesID { get; set; }
        public string IsApproved { get; set; }
        public Stream image { get; set; }
        public Byte[] im { get; set; }

        public string OwnerUserName { get; set; }
        public int OwnerUserPhone { get; set; }
        public string OwnerUserAddress { get; set; }
        public string OwnerUserEmail { get; set; }
        public List<string> regionsName { get; set; }
        public List<string> UserName { get; set; }

         public List<CommentUserProdDTO> comm { get; set; }
    }
}
