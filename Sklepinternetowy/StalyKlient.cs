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
    public class StalyKlient : Klient
    {
        [DataMember]
        private int punktyLojalnosciowe;

        public int PunktyLojalnosciowe
        {
            get => punktyLojalnosciowe;
            set => punktyLojalnosciowe = value < 0 ? 0 : value;
        }

        public StalyKlient() : base() { punktyLojalnosciowe = 0; }

        public StalyKlient(string imie, string nazwisko, string pesel, double wydanaKwota, int punkty)
            : base(imie, nazwisko, pesel, wydanaKwota)
        {
            PunktyLojalnosciowe = punkty;
        }

        public StalyKlient(string imie, string nazwisko, string dataUrodzenia, string pesel, EnumPlec plec, double wydanaKwota, int punkty)
            : base(imie, nazwisko, dataUrodzenia, pesel, plec, wydanaKwota)
        {
            PunktyLojalnosciowe = punkty;
        }


        public override void Kup(Produkt p)
        {

            decimal cenaFinalna = p.ObliczCenePoRabacie(this);


            WydanaKwota += (double)cenaFinalna;

            int nowePunkty = (int)(cenaFinalna / 10);
            PunktyLojalnosciowe += nowePunkty;

            Console.WriteLine($"[STAŁY KLIENT] {Imie} kupił {p.Nazwa} za {cenaFinalna:C2} (Rabat!). Otrzymuje {nowePunkty} pkt.");
        }

        public override string ToString()
        {
            return $"{base.ToString()} - Punkty: {PunktyLojalnosciowe}";
        }
    }
}
