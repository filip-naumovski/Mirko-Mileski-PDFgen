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
using System.Data.SqlClient;
using System.Data;
using FirstFloor.ModernUI.Presentation;
using System.Windows.Controls.Primitives;
using FirstFloor.ModernUI.Windows.Controls;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Font;

namespace mirmil.Pages
{
    public partial class Home : UserControl
    {
        
        public Home()
        {
            InitializeComponent();

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString =
                @"Data Source=localhost\sqlserver;" +
                "Initial Catalog=Mirko Mileski;" +
                "User id=sa;" +
                "Password=admin1234;";
            AppearanceManager.Current.ThemeSource = AppearanceManager.DarkThemeSource;
            conn.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = conn;
            comm.CommandText = ("SELECT * FROM Uchenici");
            SqlDataAdapter adapter = new SqlDataAdapter(comm);
            DataTable datatable = new DataTable();
            adapter.Fill(datatable);
            datatable.Columns.Add("Datum na raganje", typeof(System.String));
            foreach (DataRow row in datatable.Rows)
            {
                DateTime date = DateTime.Parse(row["Datum na ragjanje"].ToString());
                row["Datum na raganje"] = date.ToString("dd/MM/yyyy");
            }
            datatable.Columns.Remove("Datum na ragjanje");
            var myStyle = new System.Windows.Style(typeof(DataGridCell))
            {
                Setters = {
                     new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center)
                }
            };
            Uchenici.CellStyle = myStyle;
            Uchenici.DataContext = datatable.DefaultView;
        }

        

        private void button_Click(object sender, RoutedEventArgs e)
        {
            OnAddClick window = new OnAddClick();
            window.InitializeComponent();
            window.Show();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString =
                @"Data Source=localhost\sqlserver;" +
                "Initial Catalog=Mirko Mileski;" +
                "User id=sa;" +
                "Password=admin1234;";
            AppearanceManager.Current.ThemeSource = AppearanceManager.DarkThemeSource;
            conn.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = conn;
            comm.CommandText = ("SELECT * FROM Uchenici");
            SqlDataAdapter adapter = new SqlDataAdapter(comm);
            DataTable datatable = new DataTable();
            adapter.Fill(datatable);
            datatable.Columns.Add("Datum na raganje", typeof(System.String));
            foreach (DataRow row in datatable.Rows)
            {
                DateTime date = DateTime.Parse(row["Datum na ragjanje"].ToString());
                row["Datum na raganje"] = date.ToString("dd/MM/yyyy");
            }
            datatable.Columns.Remove("Datum na ragjanje");
            var myStyle = new System.Windows.Style(typeof(DataGridCell))
            {
                Setters = {
                     new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center)
                }
            };
            DataView myView = new DataView();
            myView = datatable.DefaultView;
            Uchenici.CellStyle = myStyle;
            Uchenici.ItemsSource = myView;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (Uchenici.SelectedItem == null)
            {
                MessageBox.Show("Немате одбрано ученик/ученичка.");
                return;
            }
            if(classText.Text == "Клас" || classText.Text == "")
            {
                MessageBox.Show("Немате внесено клас.");
                return;
            }
            if (profession.Text == "Струка")
            {
                MessageBox.Show("Немате одбрано струка.");
                return;
            }
            if (schoolYear.Text == "Учебна година")
            {
                MessageBox.Show("Немате одбрано учебна година.");
                return;
            }
            DataRowView selectedRowView = (DataRowView) Uchenici.SelectedItem;
            DataRow selectedRow = selectedRowView.Row;
            string docIme = selectedRow["Ime"].ToString() + selectedRow["Prezime"].ToString();
            string dest = "../../pdf/" + docIme + ".pdf";
            dest.Trim();
            string destFinal = string.Concat(dest.Where(c => !char.IsWhiteSpace(c)));
            PdfDocument pdf = new PdfDocument(new PdfReader("../../pdf/testform.pdf"), new PdfWriter(destFinal));
            PdfAcroForm form = PdfAcroForm.GetAcroForm(pdf, true);
            IDictionary<string, PdfFormField> fields = new Dictionary<string, PdfFormField>();
            fields = form.GetFormFields();
            string FONT = "../../fonts/OpenSans-Regular.ttf";
            PdfFont font1 = PdfFontFactory.CreateFont(FONT, "Cp1251", true);
            fields["tb1"].SetValue(selectedRow["Ime"].ToString(), font1, 12);
            fields["tb2"].SetValue(selectedRow["Prezime"].ToString(), font1, 12);
            fields["tb3"].SetValue(selectedRow["Tatkovo ime"].ToString(), font1, 12);
            fields["tb4"].SetValue(selectedRow["Datum na raganje"].ToString(), font1, 12);
            fields["tb5"].SetValue(schoolYear.Text, font1, 14);
            fields["tb6"].SetValue(profession.Text, font1, 10);
            fields["tb7"].SetValue(classText.Text, font1, 14);
            form.FlattenFields();
            pdf.Close();
            MessageBox.Show("Успешно е експортиран фајлот.");
        }
        string searchText;
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            searchText = searchBox.Text;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString =
                @"Data Source=localhost\sqlserver;" +
                "Initial Catalog=Mirko Mileski;" +
                "User id=sa;" +
                "Password=admin1234;";
            AppearanceManager.Current.ThemeSource = AppearanceManager.DarkThemeSource;
            conn.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = conn;
            comm.CommandText = ("SELECT * FROM Uchenici");
            SqlDataAdapter adapter = new SqlDataAdapter(comm);
            DataTable datatable = new DataTable();
            adapter.Fill(datatable);
            datatable.Columns.Add("Datum na raganje", typeof(System.String));
            foreach (DataRow row in datatable.Rows)
            {
                DateTime date = DateTime.Parse(row["Datum na ragjanje"].ToString());
                row["Datum na raganje"] = date.ToString("dd/MM/yyyy");
            }
            datatable.Columns.Remove("Datum na ragjanje");
            var myStyle = new System.Windows.Style(typeof(DataGridCell))
            {
                Setters = {
                     new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center)
                }
            };
            string[] searchParam = searchText.Split(' ');
            int wordCount = 0;
            foreach(string item in searchParam)
            {
                wordCount++;
            }
            DataView myView = new DataView();
            myView = datatable.DefaultView;
            if (wordCount != 1 && wordCount != 2)
            {
                MessageBox.Show("Greshna search sintaksa.");
                Uchenici.CellStyle = myStyle;
                Uchenici.ItemsSource = myView;
                return;
            }
            else if(wordCount == 1)
            {
                myView.RowFilter = "Ime LIKE '%" + searchParam[0] + "%'";
                Uchenici.CellStyle = myStyle;
                Uchenici.ItemsSource = myView;
                return;

            }
            else if(wordCount == 2)
            {
                myView.RowFilter = "Ime LIKE '%" + searchParam[0] + "%'" + "AND Prezime LIKE '%" + searchParam[1] + "%'";
                Uchenici.CellStyle = myStyle;
                Uchenici.ItemsSource = myView;
                return;
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
        private void SearchBox_Enter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SearchButton_Click(this, new RoutedEventArgs());
            }
        }

        private void ClassTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void ClassTextBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            bool firstclick = true;
            if (firstclick == true)
            {
                classText.Text = "";
            }
            firstclick = false;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
