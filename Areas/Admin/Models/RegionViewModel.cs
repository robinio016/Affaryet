using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VientVendreMVC.Areas.Admin.Models
{
    public class RegionViewModel
    {
        [Required]
        public string RegionNames { get; set; }
    }
}
