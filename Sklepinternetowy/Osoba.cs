using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Sklepinternetowy;

namespace Sklepinternetowy
{
    public enum EnumPlec { K, M, X }


    [DataContract]
    public abstract class Osoba : IEquatable<Osoba>
    {
        [DataMember]
        private string imie = string.Empty;
        [DataMember]
        private string nazwisko = string.Empty;
        [DataMember]
        private DateTime dataUrodzenia;
        [DataMember]
        private string pesel = string.Empty;
        [DataMember]
        private EnumPlec plec;

        public string Imie { get => imie; set => imie = value; }
        public string Nazwisko { get => nazwisko; set => nazwisko = value; }
        public DateTime DataUrodzenia { get => dataUrodzenia; set => dataUrodzenia = value; }
        public EnumPlec Plec { get => plec; set => plec = value; }

        public string Pesel
        {
            get => pesel;
            init
            {
                Regex r = new Regex(@"^\d{11}$");
                if (!r.IsMatch(value))
                {
                    throw new WrongPeselException("Pesel jest nieprawidłowy");
                }
                pesel = value;
                RozszyfrujPesel(value);
            }
        }

        public Osoba()
        {
            Imie = string.Empty;
            Nazwisko = string.Empty;
            DataUrodzenia = DateTime.Now;
            pesel = new string('0', 11);
            plec = EnumPlec.X;
        }

        public Osoba(string imie, string nazwisko, string pesel) : this()
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Pesel = pesel;
        }

        public Osoba(string imie, string nazwisko, string dataUrodzenia, string pesel, EnumPlec plec) : this(imie,nazwisko, pesel)
        {

            if (!string.IsNullOrEmpty(dataUrodzenia) && DateTime.TryParseExact(dataUrodzenia, ["yyyy-MM-dd", "dd-MM-yyyy"], null, DateTimeStyles.None, out DateTime d))
            {
                DataUrodzenia = d;
            }

            if (plec != EnumPlec.X)
            {
                this.plec = plec;
            }
        }

        private void RozszyfrujPesel(string p)
        {
            int cyfraPlci = int.Parse(p.Substring(9, 1));
            this.plec = (cyfraPlci % 2 == 0) ? EnumPlec.K : EnumPlec.M;

            int rok = int.Parse(p.Substring(0, 2));
            int miesiac = int.Parse(p.Substring(2, 2));
            int dzien = int.Parse(p.Substring(4, 2));

            if (miesiac >= 81 && miesiac <= 92) { rok += 1800; miesiac -= 80; }
            else if (miesiac >= 1 && miesiac <= 12) { rok += 1900; }
            else if (miesiac >= 21 && miesiac <= 32) { rok += 2000; miesiac -= 20; }
            else if (miesiac >= 41 && miesiac <= 52) { rok += 2100; miesiac -= 40; }
            else if (miesiac >= 61 && miesiac <= 72) { rok += 2200; miesiac -= 60; }

            try
            {
                this.dataUrodzenia = new DateTime(rok, miesiac, dzien);
            }
            catch
            {
                throw new WrongPeselException("Pesel jest nieprawidłowy");
            }
        }

        public int Wiek()
        {
            var today = DateTime.Today;
            var age = today.Year - DataUrodzenia.Year;
            if (DataUrodzenia.Date > today.AddYears(-age)) age--;
            return age;
        }

        public override string ToString()
        {
            return $"{Imie} {Nazwisko} [{Wiek()}] ({Plec}), ur. {DataUrodzenia:yyyy-MM-dd} ({Pesel})";
        }

        public bool Equals(Osoba? other)
        {
            if (other is null) return false;
            return Pesel.Equals(other.Pesel);
        }
    }
}
