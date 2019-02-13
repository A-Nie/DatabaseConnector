using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Data;

namespace bazaDanych
{
    /// <summary>
    /// 1. Aplikacja nazwiązuje połączenie z baza danych MySQL
    /// 2. Wyświetla informacje jeżeli wystąpi błąd w trakcie połączenia
    /// 3. Dane zostaną wyświetlone w kontrolce DATAGRID
    /// 4. Po pomyślnym wyświetleniu danych połączenie z bazą danych jest zamykane
    ///    w celu oszczędności pamięci serwera
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void logowanieBtn_Click(object sender, RoutedEventArgs e)
        {
            pobierzDane();
        }

        //Metoda nawiązuję połączenie z bazą danych MySQL
        //jeżeli wystąpi błąd logowania pokaże się stosowany komunikat
        public void pobierzDane()
        {
            //pobierz dane logowania z formularza i przypisz
            string mojePolaczenie =
            "Server=localhost;User id=root;persistsecurityinfo=True;database=world;port=3306;password=1234;sslmode=none;";
            //wykonaj polecenie języka SQL
            string sql = "SELECT * FROM world.city";

            MySqlConnection polaczenie = new MySqlConnection(mojePolaczenie);
            //blok try-catch przechwytuje błędy
            try
            {
                //otwórz połączenie z bazą danych
                polaczenie.Open();
                //wykonaj polecenie języka SQL na danych połączeniu
                using (MySqlCommand cmdSel = new MySqlCommand(sql, polaczenie))
                {
                    DataTable dt = new DataTable();
                    //Pobierz dane i zapisz w strukturze DataTable
                    MySqlDataAdapter da = new MySqlDataAdapter(cmdSel);
                    da.Fill(dt);
                    //wpisz dane do kontrolki DATAGRID
                    dataGrid1.ItemsSource = dt.DefaultView;

                }

            }
            //Jeżeli wystąpi wyjątek wyrzuć go i pokaż informacje
            catch (MySql.Data.MySqlClient.MySqlException)
            {
                MessageBox.Show("Błąd logowania do bazy danych MySQL", "Błąd");
            }
            //Zamknij połączenie po wyświetleniu danych
            polaczenie.Close();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            pobierzDane();
        }

    }
}