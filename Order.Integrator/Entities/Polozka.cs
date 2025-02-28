
namespace Order.Integrator.Entities;

public class Polozka
{
    public Guid PolozkaId { get; set; }
    public long ZakazkaId { get; set; }
    public string Popis { get; set; }
    public int Mnozstvi { get; set; }
    public decimal Cena { get; set; }
}
