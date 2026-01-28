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
    public class Kierownik : Pracownik
    {
        [DataMember]
        private decimal premia;

        public decimal Premia
        {
            get => premia;
            set => premia = value < 0 ? 0 : value;
        }

        public Kierownik() : base()
        {
            premia = 0;
        }

        public Kierownik(string imie, string nazwisko, string dataUrodzenia, string pesel, EnumPlec plec,
                         decimal stawka, int godziny, DateTime dataZatrudnienia, decimal premia)
            : base(imie, nazwisko, dataUrodzenia, pesel, plec, stawka, godziny, dataZatrudnienia)
        {
            Premia = premia;
        }

        public override decimal ObliczWyplate()
        {
            return base.ObliczWyplate() + Premia;
        }

        public override string ToString()
        {
            return $"[KIEROWNIK] {Imie} {Nazwisko} (Premia: {Premia:C2}) - Razem: {ObliczWyplate():C2}";
        }
    }
}
