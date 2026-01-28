    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;

    namespace Sklepinternetowy
    {
        [DataContract]
        public class Sklep
        {
            [DataMember]
            private string nazwa;

            [DataMember]
            private AsortymentSklepu asortyment;

            [DataMember]
            private ZespolPracowniczy personel;

            public string Nazwa
            {
                get => nazwa;
                set => nazwa = value;
            }

            public AsortymentSklepu Asortyment
            {
                get => asortyment;
                set => asortyment = value;
            }

            public ZespolPracowniczy Personel
            {
                get => personel;
                set => personel = value;
            }

            public Sklep()
            {
                nazwa = "Nowy Sklep";
                asortyment = new AsortymentSklepu();
                personel = new ZespolPracowniczy();
            }

            public Sklep(string nazwa)
            {
                this.nazwa = nazwa;
                this.asortyment = new AsortymentSklepu();
                this.personel = new ZespolPracowniczy();
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("========================================");
                sb.AppendLine($"         SKLEP: {Nazwa.ToUpper()}");
                sb.AppendLine("========================================");
                sb.AppendLine();

                sb.AppendLine(Personel.ToString());
                sb.AppendLine();
                sb.AppendLine(Asortyment.ToString());

                sb.AppendLine("========================================");
                return sb.ToString();
            }

            public static Sklep? ReadDCXML(string name)
            {
                if (!File.Exists(name)) return null;
                DataContractSerializer serializer = new DataContractSerializer(typeof(Sklep));
                using (XmlReader reader = XmlReader.Create(name))
                {
                    return (Sklep)serializer.ReadObject(reader);
                }
            }
        

            public void SaveToDCXML(string fname)
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(Sklep));
                using (XmlWriter wrtier = XmlWriter.Create(fname))
                {
                    serializer.WriteObject(wrtier, this);
                }
            }
        }
    }
