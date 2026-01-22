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
    public class ZespolPracowniczy
    {
        [DataMember]
        private Kierownik? kierownik;
        [DataMember]
        private List<Pracownik> pracownicy;

        public Kierownik Kierownik
        {
            get => kierownik;
            set => kierownik = value;
        }

        public List<Pracownik> Pracownicy
        {
            get => pracownicy;
            set => pracownicy = value;
        }

        public ZespolPracowniczy()
        {
            kierownik = null;
            pracownicy = new List<Pracownik>();
        }

        public void DodajPracownika(Pracownik p)
        {
            if (p == null) throw new ArgumentNullException(nameof(p));

            if (p is Kierownik)
            {
                throw new BledneDaneException("Kierownik nie może być dodany do listy zwykłych pracowników. Użyj właściwości 'Kierownik'.");
            }

            if (pracownicy.Any(x => x.Pesel == p.Pesel))
            {
                throw new PracownikJuzIstniejeException(p.Pesel);
            }

            pracownicy.Add(p);
        }

        public bool UsunPracownika(string pesel)
        {
            var doUsuniecia = pracownicy.FirstOrDefault(p => p.Pesel == pesel);
            if (doUsuniecia != null)
            {
                pracownicy.Remove(doUsuniecia);
                return true;
            }
            return false;
        }

        public decimal ObliczKosztyWyplat()
        {
            decimal sumaPracownikow = pracownicy.Sum(p => p.ObliczWyplate());
            
            decimal wyplataKierownika = 0;
            if (kierownik != null)
            {
                wyplataKierownika = kierownik.ObliczWyplate();
            }

            return sumaPracownikow + wyplataKierownika;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("ZESPÓŁ PRACOWNICZY");
            
            if (kierownik != null)
            {
                sb.AppendLine("KIEROWNIK");
                sb.AppendLine(kierownik.ToString());
            }
            else
            {
                sb.AppendLine("KIEROWNIK: BRAK");
            }

            sb.AppendLine("PRACOWNICY");
            if (pracownicy.Count == 0)
            {
                sb.AppendLine("(Brak pracowników)");
            }
            else
            {
                var posortowani = pracownicy.OrderBy(p => p.Nazwisko).ToList();
                foreach (var p in posortowani)
                {
                    sb.AppendLine(p.ToString());
                }
            }
            
            sb.AppendLine("--------------------------------");
            sb.AppendLine($"ŁĄCZNY KOSZT (Kierownik + Zespół): {ObliczKosztyWyplat():C2}");

            return sb.ToString();
        }
    }
}
