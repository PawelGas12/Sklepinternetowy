using Sklepinternetowy; 

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {

            Sklep sklep = new Sklep("Super Market C#");


            sklep.Asortyment.DodajProdukt("P001", new Produkt("Laptop Gamingowy", 4500.00m));
            sklep.Asortyment.DodajProdukt("P002", new Produkt("Myszka bezprzewodowa", 150.00m));
            sklep.Asortyment.DodajProdukt("P003", new Produkt("Kabel HDMI", 49.99m));


            Kierownik szef = new Kierownik("Adam", "Szefowski", "1980-05-20", "80052012345", EnumPlec.M,
                                           100.0m, 160, DateTime.Parse("2015-01-01"), 5000.0m);
            sklep.Personel.Kierownik = szef;

            Pracownik kasjer = new Pracownik("Ewa", "Pracowita", "1995-10-12", "95101209876", EnumPlec.K,
                                             25.0m, 160, DateTime.Parse("2022-03-01"));
            sklep.Personel.DodajPracownika(kasjer);

            Console.WriteLine("\n--- ZAKUPY ---");

            Klient k1 = new Klient("Jan", "Kowalski", "90010112345", 0);
            Produkt laptop = sklep.Asortyment.PobierzProdukt("P001");
            if (laptop != null) k1.Kup(laptop); 


            StalyKlient k2 = new StalyKlient("Anna", "Lojalna", "85050512345", 0, 1200);
            if (laptop != null) k2.Kup(laptop); 

            Console.WriteLine("\n--- STAN SKLEPU ---");
            Console.WriteLine(sklep.ToString());

            string plik = "sklep_dane.xml";
            sklep.SaveToDCXML(plik);
            Console.WriteLine($"Zapisano dane do {plik}");

            Sklep? wczytanySklep = Sklep.ReadDCXML(plik);
            Console.WriteLine("\n--- WCZYTANO ZE SKLEPU ---");
            Console.WriteLine(wczytanySklep.ToString());

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Wystąpił błąd: {ex.Message}");
        }
    }
}