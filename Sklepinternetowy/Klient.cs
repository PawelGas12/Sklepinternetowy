using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Sklepinternetowy;

namespace Sklepinternetowy
{
    [DataContract]
    public class Klient : Osoba
    {
        [DataMember]
        private double wydanaKwota;

        public double WydanaKwota
        {
            get => wydanaKwota;
            set => wydanaKwota = value < 0 ? 0 : value;
        }

        public Klient() : base()
        {
            wydanaKwota = 0;
        }

        public Klient(string imie, string nazwisko, string pesel, double wydanaKwota)
            : base(imie, nazwisko, pesel)
        {
            WydanaKwota = wydanaKwota;
        }

        public Klient(string imie, string nazwisko, string dataUrodzenia, string pesel, EnumPlec plec, double wydanaKwota)
            : base(imie, nazwisko, dataUrodzenia, pesel, plec)
        {
            WydanaKwota = wydanaKwota;
        }

        public virtual void Kup(Produkt p)
        {

            decimal cenaFinalna = p.ObliczCenePoRabacie(this);
            WydanaKwota += (double)cenaFinalna;
            Console.WriteLine($"{Imie} kupił {p.Nazwa} za {cenaFinalna:C2}.");
        }

        public override string ToString()
        {
            return $"[KLIENT] {base.ToString()}, Wydana kwota: {WydanaKwota:C2}";
        }
    }
}
