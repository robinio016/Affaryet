using BOL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AnnonceBs
    {
        private AnnonceProvider annonceProvider;
        public AnnonceBs()
        {
            annonceProvider = new AnnonceProvider();
        }

        public List<Annonce> GetAllAnnonce()
        {
            return annonceProvider.GetAllAnnonce();

        }

        public Annonce GetAnnonceByID(int id)
        {
            return annonceProvider.GetAnnonceByID(id);
        }

        public List<Annonce> GetAnnonceByUser(int idUser)
        {
            return annonceProvider.GetAnnonceByUser(idUser);
        }

        public List<Annonce> GetAnnonceByRegion(int idRegion)
        {
            return annonceProvider.GetAnnonceByRegion(idRegion);
        }


        public void CreateAnnonce(Annonce newAnnonce)
        {
            annonceProvider.CreateAnnonce(newAnnonce);
        }

        public void DeleteAnnonce(int id)
        {
            annonceProvider.DeleteAnnonce(id);
        }

        public void UpdateAnnonce(Annonce annonce)
        {
            annonceProvider.UpdateAnnonce(annonce);
        }
    }
}
