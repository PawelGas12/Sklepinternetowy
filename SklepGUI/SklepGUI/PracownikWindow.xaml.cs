using System;
using System.Windows;
using Sklepinternetowy; 

namespace SklepGUI
{
    public partial class PracownikWindow : Window
    {

        public Pracownik NowyPracownik { get; private set; }

        public PracownikWindow()
        {
            InitializeComponent();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string imie = txtImie.Text;
                string nazwisko = txtNazwisko.Text;
                string pesel = txtPesel.Text;

                if (string.IsNullOrWhiteSpace(imie) || string.IsNullOrWhiteSpace(nazwisko) || string.IsNullOrWhiteSpace(pesel))
                {
                    MessageBox.Show("Uzupełnij imię, nazwisko i PESEL!");
                    return;
                }

                if (!decimal.TryParse(txtStawka.Text, out decimal stawka))
                {
                    MessageBox.Show("Podaj poprawną stawkę (liczbę).");
                    return;
                }

                if (!int.TryParse(txtGodziny.Text, out int godziny))
                {
                    MessageBox.Show("Podaj poprawną liczbę godzin (liczbę całkowitą).");
                    return;
                }

                if (godziny < 0)
                {
                    MessageBox.Show("Liczba godzin nie może być ujemna!", "Błąd danych", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (stawka < 0)
                {
                    MessageBox.Show("Stawka godzinowa nie może być ujemna!", "Błąd danych", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                NowyPracownik = new Pracownik(imie, nazwisko, null, pesel, EnumPlec.X, stawka, godziny, DateTime.Now);

                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd: {ex.Message}");
            }
        }
    }
}