using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sklepinternetowy;
using System;

namespace Testy
{
    [TestClass]
    public class TestyPodstawowe
    {
        [TestMethod]
        public void Konstruktor_ListaPracownikow_NieJestNull()
        {
            ZespolPracowniczy zespol = new ZespolPracowniczy();
            Assert.IsNotNull(zespol.Pracownicy);
        }

        [TestMethod]
        public void DodawaniePracownika_ZwiekszaIloscOJeden()
        {
            ZespolPracowniczy zespol = new ZespolPracowniczy();
            int przed = zespol.Pracownicy.Count;

            Pracownik nowy = new Pracownik("Beata", "Nowak", "1991-01-01",
                "91010133333", EnumPlec.K, 20m, 160, DateTime.Now);

            zespol.DodajPracownika(nowy);

            Assert.AreEqual(przed + 1, zespol.Pracownicy.Count);
        }

        [TestMethod]
        public void CompareTo_RozneImionaPrzyTymSamymNazwisku()
        {
            Pracownik p1 = new Pracownik("Jan", "Nowak", "1990-01-01",
                "90010111111", EnumPlec.M, 20m, 160, DateTime.Now);

            Pracownik p2 = new Pracownik("Adam", "Nowak", "1990-01-01",
                "90010122222", EnumPlec.M, 20m, 160, DateTime.Now);

            int wynik = p1.CompareTo(p2);

            Assert.AreEqual(1, wynik);
        }

        [TestMethod]
        public void NiepoprawnyPesel_RzucaWyjatek()
        {
            Assert.ThrowsException<WrongPeselException>(() =>
            {
                new Pracownik("Beata", "Nowak", "1991-01-01",
                    "abc", EnumPlec.K, 20m, 160, DateTime.Now);
            });
        }

        [TestMethod]
        public void Kierownik_JestPoprawniePrzypisany()
        {
            ZespolPracowniczy zespol = new ZespolPracowniczy();
            Kierownik szef = new Kierownik("Adam", "Szef", "1980-01-01",
                "80010112345", EnumPlec.M, 50m, 160, DateTime.Now, 500m);

            zespol.Kierownik = szef;

            Assert.AreEqual(szef, zespol.Kierownik);
        }
    }
}