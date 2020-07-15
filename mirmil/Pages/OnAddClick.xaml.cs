using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace mirmil.Pages
{
    public partial class OnAddClick : ModernWindow
    {
        public OnAddClick()
        {
            InitializeComponent();
            Style = (Style)App.Current.Resources["BlankWindow"];
         }
        bool imeWrite = false;
        bool prezimeWrite = false;
        bool tatkovoWrite = false;
        bool datumWrite = false;

        

        private void Ime_OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (!imeWrite) Ime.Text = "";
            imeWrite = true;
        }
        private void Prezime_OnGotFocus(object sender, RoutedEventArgs e)
        {
            if(!prezimeWrite) Prezime.Text = "";
            prezimeWrite = true;
        }
        private void Tatkovo_OnGotFocus(object sender, RoutedEventArgs e)
        {
            if(!tatkovoWrite) Tatkovoime.Text = "";
            tatkovoWrite = true;
        }
        private void Datum_OnGotFocus(object sender, RoutedEventArgs e)
        {
            if(!datumWrite) Datum.Text = "";
            datumWrite = true;
        }
        private void Ime_Enter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Prezime.SelectAll();
            }
            
        }
        private void Prezime_Enter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Tatkovoime.SelectAll();
            }
        }
        private void Tatkovo_Enter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Datum.SelectAll();
            }
        }
        private void Datum_Enter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                button_Click(this, new RoutedEventArgs());
            }

        }

        private void Ime_OnLostFocus(object sender, RoutedEventArgs e)
        {
            //if (Ime.Text == "") Ime.Visibility = Visibility.Visible;
        }
        private void Prezime_OnLostFocus(object sender, RoutedEventArgs e)
        {
            //if (Prezime.Text == "") Prezime.Visibility = Visibility.Visible;
        }
        private void Tatkovo_OnLostFocus(object sender, RoutedEventArgs e)
        {
            //if (Tatkovoime.Text == "") Tatkovoime.Visibility = Visibility.Visible;
        }
        private void Datum_OnLostFocus(object sender, RoutedEventArgs e)
        {
            //if (Datum.Text == "") Datum.Visibility = Visibility.Visible;
        }

        //Funkcionalnost

        string newIme="";
        string newPrezime="";
        string newTatkovo="";
        string newDatum="";

        private void Ime_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void Prezime_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void Tatkovoime_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void Datum_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            newIme = Ime.Text;
            newPrezime = Prezime.Text;
            newTatkovo = Tatkovoime.Text;
            newDatum = Datum.Text;
            if (newIme == "" || newPrezime == "" || newTatkovo == "" || newDatum == "")
            {
                this.Hide();
                return;
            }
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString =
                @"Data Source=localhost\sqlserver;" +
                "Initial Catalog=Mirko Mileski;" +
                "User id=sa;" +
                "Password=admin1234;";
            string sqlQuery = "INSERT INTO dbo.Uchenici VALUES(@newIme,@newPrezime,@newTatkovo,@newDatum);";
            SqlCommand comm = new SqlCommand(sqlQuery, conn);
            conn.Open();
            comm.Parameters.AddWithValue("@newIme", newIme);
            comm.Parameters.AddWithValue("@newPrezime", newPrezime);
            comm.Parameters.AddWithValue("@newTatkovo", newTatkovo);
            comm.Parameters.AddWithValue("@newDatum", newDatum);
            comm.ExecuteNonQuery();
            this.Hide();
        }
    }
}

