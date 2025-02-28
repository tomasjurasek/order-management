
namespace Order.Integrator.Entities
{
    public class Zakazka
    {
        public long ZakazkaId { get; set; }

        public string Type { get; set; }

        public string Popisek { get; set; }
    }

    public class Polozky
    {
        public long PolozkaId { get; set; }
        public long ZakazkaId { get; set; }
        public string Popis { get; set; }
        public int Mnozstvi { get; set; }
        public decimal Cena { get; set; }
    }
}
