using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Sklepinternetowy;

namespace SklepGUI
{
    public partial class MainWindow : Window
    {
        private Sklep sklep = new Sklep("Mój Sklep");

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                var wczytany = Sklep.ReadDCXML("sklep_dane.xml");
                if (wczytany != null) sklep = wczytany;
            }
            catch { }

            OdswiezWidok();
        }

        private void OdswiezWidok()
        {

            if (sklep.Personel?.Kierownik != null)
                txtKierownik.Text = sklep.Personel.Kierownik.ToString();
            else
                txtKierownik.Text = "BRAK";


            if (sklep.Personel != null)
                lstPracownicy.ItemsSource = new ObservableCollection<Pracownik>(sklep.Personel.Pracownicy);


            if (sklep.Asortyment != null)
                lstProdukty.ItemsSource = new ObservableCollection<Produkt>(sklep.Asortyment.PobierzListe());
        }



        private void btnDodajProdukt_Click(object sender, RoutedEventArgs e)
        {
            ProduktWindow okno = new ProduktWindow();
            if (okno.ShowDialog() == true)
            {
                string kod = "P" + new Random().Next(10000, 99999);
                try
                {
                    sklep.Asortyment.DodajProdukt(kod, okno.NowyProdukt);
                    OdswiezWidok();
                }
                catch (Exception ex) { MessageBox.Show("Błąd: " + ex.Message); }
            }
        }

        private void btnUsunProdukt_Click(object sender, RoutedEventArgs e)
        {
            if (lstProdukty.SelectedItem is Produkt zaznaczony)
            {
                if (MessageBox.Show("Usunąć?", "Pytanie", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    sklep.Asortyment.UsunProduktPrzezObiekt(zaznaczony);
                    OdswiezWidok();
                }
            }
        }

        private void btnDodajPracownika_Click(object sender, RoutedEventArgs e)
        {
            PracownikWindow okno = new PracownikWindow();
            if (okno.ShowDialog() == true)
            {
                sklep.Personel.DodajPracownika(okno.NowyPracownik);
                OdswiezWidok();
            }
        }

        private void btnUsunPracownika_Click(object sender, RoutedEventArgs e)
        {
            if (lstPracownicy.SelectedItem is Pracownik p)
            {
                sklep.Personel.UsunPracownika(p.Pesel);
                OdswiezWidok();
            }
        }

        private void btnZmienKierownika_Click(object sender, RoutedEventArgs e)
        {
            PracownikWindow okno = new PracownikWindow();
            if (okno.ShowDialog() == true)
            {
                Pracownik p = okno.NowyPracownik;
                Kierownik k = new Kierownik(p.Imie, p.Nazwisko, null, p.Pesel, EnumPlec.X, p.StawkaGodzinowa, p.LiczbaGodzin, p.DataZatrudnienia, 500m);
                sklep.Personel.Kierownik = k;
                OdswiezWidok();
            }
        }

        private void btnZapisz_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sklep.SaveToDCXML("sklep_dane.xml");
                MessageBox.Show("Zapisano!");
            }
            catch (Exception ex) { MessageBox.Show("Błąd: " + ex.Message); }
        }

        private void btnSortujProdukty_Click(object sender, RoutedEventArgs e)
        {
            if (sklep.Asortyment != null)
            {
                var lista = sklep.Asortyment.PobierzListe();
                var posortowana = lista.OrderByDescending(p => p.Cena).ToList();
                lstProdukty.ItemsSource = new ObservableCollection<Produkt>(posortowana);
            }
        }


        private void btnKlonujProdukt_Click(object sender, RoutedEventArgs e)
        {
            if (lstProdukty.SelectedItem is Produkt zaznaczony)
            {

                if (zaznaczony is ICloneable)
                {
                    Produkt kopia = (Produkt)zaznaczony.Clone();
                    kopia.Nazwa += " (KOPIA)";
                    string kod = "P" + new Random().Next(100000, 999999);
                    try
                    {
                        sklep.Asortyment.DodajProdukt(kod, kopia);
                        OdswiezWidok();
                    }
                    catch { MessageBox.Show("Błąd klonowania."); }
                }
                else
                {

                    Produkt kopia = new Produkt(zaznaczony.Nazwa + " (KOPIA)", zaznaczony.Cena);
                    string kod = "P" + new Random().Next(10000, 99999);
                    sklep.Asortyment.DodajProdukt(kod, kopia);
                    OdswiezWidok();
                }
            }
        }


        private void btnSortuj_Click(object sender, RoutedEventArgs e)
        {
            if (sklep.Personel != null)
            {

                try
                {
                    sklep.Personel.Pracownicy.Sort();
                }
                catch
                {

                    sklep.Personel.Pracownicy = sklep.Personel.Pracownicy.OrderBy(p => p.Nazwisko).ToList();
                }
                OdswiezWidok();
            }
        }

        private void btnSortujWyplata_Click(object sender, RoutedEventArgs e)
        {
            if (sklep.Personel != null)
            {
                sklep.Personel.Pracownicy.Sort(new WgWyplatyComparer());
                OdswiezWidok();
            }
        }
        private void btnEdytujProdukt_Click(object sender, RoutedEventArgs e)
        {
            if (lstProdukty.SelectedItem is Produkt zaznaczony)
            {

                ProduktWindow okno = new ProduktWindow(zaznaczony);

                if (okno.ShowDialog() == true)
                {

                    zaznaczony.Nazwa = okno.NowyProdukt.Nazwa;
                    zaznaczony.Cena = okno.NowyProdukt.Cena;

                    OdswiezWidok();
                }
            }
            else
            {
                MessageBox.Show("Zaznacz produkt do edycji.");
            }
        }
    }


    public class WgWyplatyComparer : IComparer<Pracownik>
    {
        public int Compare(Pracownik? x, Pracownik? y)
        {
            if (x == null || y == null) return 0;
            decimal w1 = x.ObliczWyplate();
            decimal w2 = y.ObliczWyplate();

            if (w1 < w2) return 1;
            if (w1 > w2) return -1;
            return 0;
        }
    }

}