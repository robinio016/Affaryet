using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VientVendreMVC.Areas.User.Models
{
    public class WizardViewModel
    {
        public AnnonceViewModel annonceStep { get; set; }
        public ProductViewModel ProductStep { get; set; }
    }
}