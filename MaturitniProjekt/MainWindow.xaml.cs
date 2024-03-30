using Hardware.Info;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static readonly IHardwareInfo hardwareInfo = new HardwareInfo();
        private SQLiteConnection connection;
        public MainWindow()
        {
            InitializeComponent();

            string appDataCesta = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string cestaKSlozce = System.IO.Path.Combine(appDataCesta, "zatezovyTest");

            if (!Directory.Exists(cestaKSlozce))
            {
                Directory.CreateDirectory(cestaKSlozce);
            }

            globalniPromenne.cestaKDatabazi = System.IO.Path.Combine(cestaKSlozce, "historie.db");
            connection = new SQLiteConnection($"Data Source={globalniPromenne.cestaKDatabazi};Version=3;");
            vytvoreniTabulky();
            
            

            hlavniFrame.Content = new HomeWindow();
            LBLhome.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3772FF"));
            IMGhome.Source = new BitmapImage(new Uri("obrazky/homeC.png", UriKind.RelativeOrAbsolute));
            BRDhome.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3772FF"));
            BRDhome.Visibility = Visibility.Visible;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        #region homeHover
        private void BTNhome_MouseEnter(object sender, MouseEventArgs e)
        {
            if (LBLhome.Foreground is SolidColorBrush solidColorBrush && solidColorBrush.Color == (Color)ColorConverter.ConvertFromString("#8A94A6"))
            {
                LBLhome.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4B5D78"));
                IMGhome.Source = new BitmapImage(new Uri("obrazky/homeH.png", UriKind.RelativeOrAbsolute));
                BRDhome.Visibility = Visibility.Visible;
            }
        }

        private void BTNhome_MouseLeave(object sender, MouseEventArgs e)
        {
            if (LBLhome.Foreground is SolidColorBrush solidColorBrush && solidColorBrush.Color == (Color)ColorConverter.ConvertFromString("#4B5D78"))
            {
                LBLhome.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8A94A6"));
                IMGhome.Source = new BitmapImage(new Uri("obrazky/home.png", UriKind.RelativeOrAbsolute));
                BRDhome.Visibility = Visibility.Hidden;
            }
        }
        #endregion homeHover

        private void BTNhome_Click(object sender, RoutedEventArgs e)
        {
            LBLhome.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3772FF"));
            IMGhome.Source = new BitmapImage(new Uri("obrazky/homeC.png", UriKind.RelativeOrAbsolute));
            BRDhome.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3772FF"));
            BRDhome.Visibility = Visibility.Visible;

            LBLhistory.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8A94A6"));
            IMGhistory.Source = new BitmapImage(new Uri("obrazky/history.png", UriKind.RelativeOrAbsolute));
            BRDhistory.Visibility = Visibility.Hidden;

            LBLshutdown.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8A94A6"));
            IMGshutdown.Source = new BitmapImage(new Uri("obrazky/shutdown.png", UriKind.RelativeOrAbsolute));
            BRDshutdown.Visibility = Visibility.Hidden;

            hlavniFrame.Content = new HomeWindow();
        }

        #region historyHover
        private void BTNhistory_MouseEnter(object sender, MouseEventArgs e)
        {
            if (LBLhistory.Foreground is SolidColorBrush solidColorBrush && solidColorBrush.Color == (Color)ColorConverter.ConvertFromString("#8A94A6"))
            {
                LBLhistory.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4B5D78"));
                IMGhistory.Source = new BitmapImage(new Uri("obrazky/historyH.png", UriKind.RelativeOrAbsolute));
                BRDhistory.Visibility = Visibility.Visible;
            }
        }

        private void BTNhistory_MouseLeave(object sender, MouseEventArgs e)
        {
            if (LBLhistory.Foreground is SolidColorBrush solidColorBrush && solidColorBrush.Color == (Color)ColorConverter.ConvertFromString("#4B5D78"))
            {
                LBLhistory.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8A94A6"));
                IMGhistory.Source = new BitmapImage(new Uri("obrazky/history.png", UriKind.RelativeOrAbsolute));
                BRDhistory.Visibility = Visibility.Hidden;
            }
        }
        #endregion historyHover

        private void BTNhistory_Click(object sender, RoutedEventArgs e)
        {
            LBLhistory.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3772FF"));
            IMGhistory.Source = new BitmapImage(new Uri("obrazky/historyC.png", UriKind.RelativeOrAbsolute));
            BRDhistory.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3772FF"));
            BRDhistory.Visibility = Visibility.Visible;

            LBLhome.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8A94A6"));
            IMGhome.Source = new BitmapImage(new Uri("obrazky/home.png", UriKind.RelativeOrAbsolute));
            BRDhome.Visibility = Visibility.Hidden;

            LBLshutdown.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8A94A6"));
            IMGshutdown.Source = new BitmapImage(new Uri("obrazky/shutdown.png", UriKind.RelativeOrAbsolute));
            BRDshutdown.Visibility = Visibility.Hidden;

            hlavniFrame.Content = new HistoryWindow();
        }

        #region vypnuti
        private void BTNshutdown_MouseEnter(object sender, MouseEventArgs e)
        {
            if (LBLshutdown.Foreground is SolidColorBrush solidColorBrush && solidColorBrush.Color == (Color)ColorConverter.ConvertFromString("#8A94A6"))
            {
                LBLshutdown.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4B5D78"));
                IMGshutdown.Source = new BitmapImage(new Uri("obrazky/shutdownH.png", UriKind.RelativeOrAbsolute));
                BRDshutdown.Visibility = Visibility.Visible;
            }
        }

        private void BTNshutdown_MouseLeave(object sender, MouseEventArgs e)
        {
            if (LBLshutdown.Foreground is SolidColorBrush solidColorBrush && solidColorBrush.Color == (Color)ColorConverter.ConvertFromString("#4B5D78"))
            {
                LBLshutdown.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8A94A6"));
                IMGshutdown.Source = new BitmapImage(new Uri("obrazky/shutdown.png", UriKind.RelativeOrAbsolute));
                BRDshutdown.Visibility = Visibility.Hidden;
            }
        }

        private void BTNshutdown_Click(object sender, RoutedEventArgs e)
        {
            LBLshutdown.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3772FF"));
            IMGshutdown.Source = new BitmapImage(new Uri("obrazky/shutdownC.png", UriKind.RelativeOrAbsolute));
            BRDshutdown.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3772FF"));
            BRDshutdown.Visibility = Visibility.Visible;

            LBLhome.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8A94A6"));
            IMGhome.Source = new BitmapImage(new Uri("obrazky/home.png", UriKind.RelativeOrAbsolute));
            BRDhome.Visibility = Visibility.Hidden;

            LBLhistory.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8A94A6"));
            IMGhistory.Source = new BitmapImage(new Uri("obrazky/history.png", UriKind.RelativeOrAbsolute));
            BRDhistory.Visibility = Visibility.Hidden;

            Application.Current.Shutdown();
        }
        #endregion vypnuti


        private void vytvoreniTabulky()
        {
            string dotazProVytvoreniTabulky = @"CREATE TABLE IF NOT EXISTS historie (
                                                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                                                Datum DATE,
                                                NazevProcesoru CHAR(255),
                                                Skore INT,
                                                AutomatickyTest CHAR(5),
                                                Cas INT)";
            
            using (SQLiteCommand command = new SQLiteCommand(dotazProVytvoreniTabulky, connection))
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            connection.Close();

        }
    }
}
