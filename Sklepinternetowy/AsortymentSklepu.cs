using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Sklepinternetowy;
using System.Security.Cryptography.X509Certificates;

namespace Sklepinternetowy
{
    [DataContract]
    public class AsortymentSklepu
    {
        [DataMember]
        private Dictionary<string, Produkt> produkty;

        public AsortymentSklepu()
        {
            produkty = new Dictionary<string, Produkt>();
        }

        public void DodajProdukt(string kod, Produkt produkt)
        {
            if (string.IsNullOrWhiteSpace(kod))
                throw new BledneDaneException("Kod produktu nie może być pusty.");

            if (produkt == null)
                throw new ArgumentNullException(nameof(produkt));

            if (produkty.ContainsKey(kod))
            {
                throw new ProduktJuzIstniejeException(kod);
            }

            produkty.Add(kod, produkt);
        }

        public bool UsunProdukt(string kod)
        {
            if (produkty.ContainsKey(kod))
            {
                produkty.Remove(kod);
                return true;
            }
            return false;
        }

        public Produkt? PobierzProdukt(string kod)
        {
            if (produkty.TryGetValue(kod, out var p))
            {
                return p;
            }
            return null;
        }

        public Produkt? WyszukajPoNazwie(string nazwa)
        {
            return produkty.Values.FirstOrDefault(p => p.Nazwa.Equals(nazwa, StringComparison.OrdinalIgnoreCase));
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("ASORTYMENT");

            if (produkty.Count == 0)
            {
                sb.AppendLine("(Pusto)");
            }
            else
            {
                foreach (var item in produkty)
                {
                    sb.AppendLine($"[Kod: {item.Key}] {item.Value}");
                }
            }

            return sb.ToString();
        }
        public List<Produkt> PobierzListe()
        {
            return produkty.Values.ToList();
        }
        public void UsunProduktPrzezObiekt(Produkt p)
        {
            var wpis = produkty.FirstOrDefault(x => x.Value == p);

            if (wpis.Key != null)
            {
                produkty.Remove(wpis.Key);
            }
        }
    }
   
}
