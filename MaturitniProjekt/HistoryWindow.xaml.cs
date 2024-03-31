using Syncfusion.UI.Xaml.Grid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
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

namespace MaturitniProjekt
{
    /// <summary>
    /// Interakční logika pro HistoryWindow.xaml
    /// </summary>
    public partial class HistoryWindow : Page
    {
        private SQLiteConnection connection;

        public HistoryWindow()
        {
            globalniPromenne.globalniBeh = false;
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NBaF5cXmRCekx0THxbf1x0ZFNMYVVbQHVPIiBoS35RckVgWn9feHVcQmhUUkVw");
            InitializeComponent();

            connection = new SQLiteConnection($"Data Source={globalniPromenne.cestaKDatabazi};Version=3;");
            vypisDatZTabulky();
        }

        private void vypisDatZTabulky()
        {
            string vypisovaciDotaz = "SELECT * FROM historie";

            using (SQLiteCommand command = new SQLiteCommand(vypisovaciDotaz, connection))
            {
                connection.Open();
                command.ExecuteNonQuery();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGrid.ItemsSource = dataTable.DefaultView;            
            }
           
            connection.Close();
        }

        private void BTNdelete_MouseEnter(object sender, MouseEventArgs e)
        {
            if (BTNdelete.BorderBrush is SolidColorBrush solidColorBrush && solidColorBrush.Color == (Color)ColorConverter.ConvertFromString("#8A94A6"))
            {
                BTNdelete.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4B5D78"));
                IMGdelete.Source = new BitmapImage(new Uri("obrazky/deleteH.png", UriKind.RelativeOrAbsolute));
                LBLdelete.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4B5D78"));
            }
        }

        private void BTNdelete_MouseLeave(object sender, MouseEventArgs e)
        {
            if (BTNdelete.BorderBrush is SolidColorBrush solidColorBrush && solidColorBrush.Color == (Color)ColorConverter.ConvertFromString("#4B5D78"))
            {
                BTNdelete.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8A94A6"));
                IMGdelete.Source = new BitmapImage(new Uri("obrazky/delete.png", UriKind.RelativeOrAbsolute));
                LBLdelete.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8A94A6"));
            }
        }

        private void BTNdelete_Click(object sender, RoutedEventArgs e)
        {
            BTNdelete.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3772FF"));
            IMGdelete.Source = new BitmapImage(new Uri("obrazky/saveC.png", UriKind.RelativeOrAbsolute));
            LBLdelete.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3772FF"));

            smazaniZTabulky();

            BTNdelete.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8A94A6"));
            IMGdelete.Source = new BitmapImage(new Uri("obrazky/delete.png", UriKind.RelativeOrAbsolute));
            LBLdelete.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8A94A6"));
        }

        private void smazaniZTabulky()
        {
            var selectedItem = dataGrid.SelectedItem;
            
            
            if (selectedItem != null)
            {
                DataRowView row = (DataRowView)selectedItem;
                int id = Convert.ToInt32(row["ID"]);
                string mazaciDotaz = $@"DELETE FROM historie
                                        WHERE ID={id}";

                using (SQLiteCommand command = new SQLiteCommand(mazaciDotaz, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                connection.Close();
                dataGrid.ItemsSource = null;
                vypisDatZTabulky();
                MessageBox.Show($"Smazán záznam s ID {id}");
            }
            else
            {
                MessageBox.Show("Není vybrán žádný řádek.");
            }
        }
    }
}
