using Order.Integrator.Entities;

namespace Order.Integrator.Storage;

public class LegacyAdapterStorage
{
    public IList<Zakazka> GetZakazky()
    {
        return new List<Zakazka>
        {
            new Zakazka
            {
                ZakazkaId = 1,
                Type = "Type1",
                Popisek = "Popisek1"
            },
            new Zakazka
            {
                ZakazkaId = 2,
                Type = "Type2",
                Popisek = "Popisek2"
            }
        };
    }

    public IList<Polozka> GetPolozky()
    {
        return new List<Polozka>
        {
            new Polozka
            {
                PolozkaId = Guid.NewGuid(),
                ZakazkaId = 1,
                Popis = "Popis1",
                Mnozstvi = 1,
                Cena = 1
            },
            new Polozka
            {
                PolozkaId = Guid.NewGuid(),
                ZakazkaId = 2,
                Popis = "Popis2",
                Mnozstvi = 2,
                Cena = 2
            }
        };
    }
}
