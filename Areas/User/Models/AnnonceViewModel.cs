using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VientVendreMVC.Areas.User.Models
{
    public class AnnonceViewModel
    {
        public AnnonceViewModel()
        {
            Regions = new List<string>();
            RegionsID = new List<int>();
        }
        public int AnnonceID { get; set; }
        [Required]
        public string AnnonceTitle { get; set; }
        [Required]
        public string Description { get; set; }

        public List<string> Regions { get; set; }

        public List<int> RegionsID { get; set; }
        public int NombreProduct { get; set; }

        

    }
}
