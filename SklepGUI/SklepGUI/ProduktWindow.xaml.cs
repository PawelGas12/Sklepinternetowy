using System;
using System.Windows;
using Sklepinternetowy;

namespace SklepGUI
{
    public partial class ProduktWindow : Window
    {
        public Produkt NowyProdukt { get; private set; }

        public ProduktWindow()
        {
            InitializeComponent();
        }


        public ProduktWindow(Produkt p) : this()
        {
            if (p != null)
            {
                txtNazwa.Text = p.Nazwa;
                txtCena.Text = p.Cena.ToString();
            }
        }


        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNazwa.Text))
                {
                    MessageBox.Show("Podaj nazwę produktu.");
                    return;
                }

                if (decimal.TryParse(txtCena.Text, out decimal cena))
                {
                    NowyProdukt = new Produkt(txtNazwa.Text, cena);
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Podaj poprawną cenę (liczba).");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd tworzenia produktu: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}