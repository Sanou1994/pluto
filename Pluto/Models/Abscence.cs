
namespace Pluto.Models
{
    public class Abscence
    {
        public long id { get; set; }
        public long dateDebut { get; set; }
        public long dateFin { get; set; }
        public bool status { get; set; }
        public long dateAbscenceCreate { get; set; }

        public ClasseList classe { get; set; }

        public string raison { get; set; }
        public User user { get; set; }
    }
}