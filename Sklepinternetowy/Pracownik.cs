using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Sklepinternetowy;

namespace Sklepinternetowy
{
        [DataContract]
        public class Pracownik : Osoba, IComparable<Pracownik>, IEquatable<Pracownik>
    {
            [DataMember]
            private decimal stawkaGodzinowa;
            [DataMember]
            private int liczbaGodzin;
            [DataMember]
            private DateTime dataZatrudnienia;

            public decimal StawkaGodzinowa
            {
                get => stawkaGodzinowa;
                set => stawkaGodzinowa = value < 0 ? 0 : value;
            }

            public int LiczbaGodzin
            {
                get => liczbaGodzin;
                set => liczbaGodzin = value < 0 ? 0 : value;
            }

            public DateTime DataZatrudnienia
            {
                get => dataZatrudnienia;
                set => dataZatrudnienia = value;
            }


            public Pracownik() : base()
            {
                stawkaGodzinowa = 0;
                liczbaGodzin = 0;
                dataZatrudnienia = DateTime.Now;
            }

            public Pracownik(string imie, string nazwisko, string dataUrodzenia, string pesel, EnumPlec plec,
                             decimal stawka, int godziny, DateTime dataZatrudnienia)
                : base(imie, nazwisko, dataUrodzenia, pesel, plec)
            {
                StawkaGodzinowa = stawka;
                LiczbaGodzin = godziny;
                DataZatrudnienia = dataZatrudnienia;
            }


            public virtual decimal ObliczWyplate()
            {
                return StawkaGodzinowa * LiczbaGodzin;
            }


            public int StazPracy()
            {
                return ((DateTime.Now - DataZatrudnienia).Days / 365) > 0 ? (DateTime.Now - DataZatrudnienia).Days / 365 : 0;
            }

            public override string ToString()
            {
                return $"[PRACOWNIK] {base.ToString()} (Staż: {StazPracy()} lat) - Wypłata: {ObliczWyplate():C2}";
            }

        public int CompareTo(Pracownik? other)
        {
            if (other == null) return 1;
            int wynik = this.Nazwisko.CompareTo(other.Nazwisko);
            if (wynik == 0)
            {
                return this.Imie.CompareTo(other.Imie);
            }
            return wynik;
        }

        public bool Equals(Pracownik? other)
        {
            if (other == null) return false;
            return this.Pesel == other.Pesel;
        }
    }

}
