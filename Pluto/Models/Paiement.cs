

namespace Pluto.Models
{
    public class Paiement
    {
        public long id { get; set; }
        public User user { get; set; }
        public long datePaiement { get; set; }
        public  string order_number { get; set; }
        public double montant { get; set; }
        public string typeTransaction { get; set; }
        public string status { get; set; }

        public DepartementList departement { get; set; }
       
    }
}

