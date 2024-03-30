using System;
using System.Collections.Generic;
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
    }
}
