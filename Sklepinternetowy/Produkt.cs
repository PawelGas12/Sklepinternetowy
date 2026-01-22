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
        public class Produkt : ICloneable, IOpisowy
    {
            [DataMember]
            private string nazwa = string.Empty;
            [DataMember]
            private decimal cena;

            public string Nazwa
            {
                get => nazwa;
                set => nazwa = value;
            }

        public decimal Cena
        {
            get => cena;
            set
            {
                if (value < 0)
                    throw new BledneDaneException("Cena produktu nie może być ujemna.");
                cena = value;
            }
        }

        public Produkt()
            {
                nazwa = "Towar";
                cena = 0;
            }

            public Produkt(string nazwa, decimal cena)
            {
                Nazwa = nazwa;
                Cena = cena;
            }

            public decimal ObliczCenePoRabacie(Klient k)
        {
                if (k is StalyKlient sk)
                {
                    decimal rabat = 0.0m;

                    if (sk.PunktyLojalnosciowe >= 1000) rabat = 0.20m;
                    else if (sk.PunktyLojalnosciowe >= 500) rabat = 0.10m;
                    else if (sk.PunktyLojalnosciowe >= 100) rabat = 0.05m;

                    return Cena * (1.0m - rabat);
                }
                return Cena;
            }

            public override string ToString()
            {
                return $"{Nazwa} - {Cena:C2}";
            }

            public object Clone()
            {
                return this.MemberwiseClone();
            }

            
            public string PobierzPelnyOpis()
            {
                return $"PRODUKT: {Nazwa}, CENA BRUTTO: {Cena:C2}";
            }

    }
    
}

