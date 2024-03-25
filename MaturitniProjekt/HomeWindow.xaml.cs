using Hardware.Info;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace MaturitniProjekt
{
    /// <summary>
    /// Interakční logika pro HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Page
    {
        static IHardwareInfo hardwareInfo = new HardwareInfo();
        private bool behTestu = true;
        public HomeWindow()
        {
            InitializeComponent();
           
            if (trida.neco == 0)
            {
                trida.neco = 1;
                hardwareInfo.RefreshCPUList();
            }


            foreach (var cpu in hardwareInfo.CpuList)
            {
                LBLnazev.Content = cpu.Name;
                LBLrychlost.Content = cpu.MaxClockSpeed;
                LBLjadra.Content = cpu.NumberOfCores;
                LBLlprocesory.Content = cpu.NumberOfLogicalProcessors;
                LBLmezi1.Content = (cpu.L1DataCacheSize + cpu.L1InstructionCacheSize) / 1024 + "kB";
                LBLmezi2.Content = cpu.L2CacheSize / 1024 / 1024 + "MB";
                LBLmezi3.Content = cpu.L3CacheSize / 1024 / 1024 + "MB";
            }
           
        }

        #region testHover
        private void TBTNtest_MouseEnter(object sender, MouseEventArgs e)
        {
            TBTNtest.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4B5D78"));
            IMGtest.Source = new BitmapImage(new Uri("obrazky/onH.png", UriKind.RelativeOrAbsolute));
        }

        private void TBTNtest_MouseLeave(object sender, MouseEventArgs e)
        {
            if (TBTNtest.IsChecked == false)
            {
                TBTNtest.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3772FF"));
                IMGtest.Source = new BitmapImage(new Uri("obrazky/onC.png", UriKind.RelativeOrAbsolute));
            }
            else if (TBTNtest.IsChecked == true)
            {
                TBTNtest.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8A94A6"));
                IMGtest.Source = new BitmapImage(new Uri("obrazky/on.png", UriKind.RelativeOrAbsolute));
            }
        }

        #endregion testHoveer
        private void TBTNtest_Checked(object sender, RoutedEventArgs e)
        {
            TBTNtest.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8A94A6"));
            IMGtest.Source = new BitmapImage(new Uri("obrazky/on.png", UriKind.RelativeOrAbsolute));
            
        }

        private void TBTNtest_Unchecked(object sender, RoutedEventArgs e)
        {
            TBTNtest.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3772FF"));
            IMGtest.Source = new BitmapImage(new Uri("obrazky/onC.png", UriKind.RelativeOrAbsolute));
        }

        #region startHover
        private void BTNstart_MouseEnter(object sender, MouseEventArgs e)
        {
            if (BTNstart.BorderBrush is SolidColorBrush solidColorBrush && solidColorBrush.Color == (Color)ColorConverter.ConvertFromString("#8A94A6"))
            {
                BTNstart.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4B5D78"));
                IMGstart.Source = new BitmapImage(new Uri("obrazky/startH.png", UriKind.RelativeOrAbsolute));
                LBLstart.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4B5D78"));
            }
        }

        private void BTNstart_MouseLeave(object sender, MouseEventArgs e)
        {
            if (BTNstart.BorderBrush is SolidColorBrush solidColorBrush && solidColorBrush.Color == (Color)ColorConverter.ConvertFromString("#4B5D78"))
            {
                BTNstart.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8A94A6"));
                IMGstart.Source = new BitmapImage(new Uri("obrazky/start.png", UriKind.RelativeOrAbsolute));
                LBLstart.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8A94A6"));
            }
        }
        #endregion startHover

        #region stopHover
        private void BTNstop_MouseEnter(object sender, MouseEventArgs e)
        {
            if (BTNstop.BorderBrush is SolidColorBrush solidColorBrush && solidColorBrush.Color == (Color)ColorConverter.ConvertFromString("#8A94A6"))
            {
                BTNstop.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4B5D78"));
                IMGstop.Source = new BitmapImage(new Uri("obrazky/stopH.png", UriKind.RelativeOrAbsolute));
                LBLstop.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4B5D78"));
            }

        }

        private void BTNstop_MouseLeave(object sender, MouseEventArgs e)
        {
            if (BTNstop.BorderBrush is SolidColorBrush solidColorBrush && solidColorBrush.Color == (Color)ColorConverter.ConvertFromString("#4B5D78"))
            {
                BTNstop.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8A94A6"));
                IMGstop.Source = new BitmapImage(new Uri("obrazky/stop.png", UriKind.RelativeOrAbsolute));
                LBLstop.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8A94A6"));
            }
        }
        #endregion stopHover

        private  void BTNstart_Click(object sender, RoutedEventArgs e)
        {
            BTNstart.IsEnabled = false;
            TBTNtest.IsEnabled = false;
            BTNstart.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3772FF"));
            IMGstart.Source = new BitmapImage(new Uri("obrazky/startC.png", UriKind.RelativeOrAbsolute));
            LBLstart.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3772FF"));

            LBLskore.Visibility = Visibility.Hidden;
            LBLskore.Content = 0;
            trida.skore = 0;

            if (TBTNtest.IsChecked == false) //auto
            {
                LBLcas.Visibility = Visibility.Hidden;
                BRDpb.Visibility = Visibility.Visible;
                Thread test = new Thread(() => zatezovyTest(true));
                test.Start();
            }
            else if (TBTNtest.IsChecked == true) //noAuto
            {
                behTestu = true;
                LBLcas.Visibility = Visibility.Visible;
                BRDpb.Visibility = Visibility.Hidden;

                Thread test = new Thread(() => zatezovyTest(false));
                test.Start();

                BTNstop.IsEnabled = true;
                BTNstop.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8A94A6"));
                IMGstop.Source = new BitmapImage(new Uri("obrazky/stop.png", UriKind.RelativeOrAbsolute));
                LBLstop.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8A94A6"));
            }
        }

        private async void BTNstop_Click(object sender, RoutedEventArgs e)
        {
            BTNstop.IsEnabled = false;

            BTNstop.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3772FF"));
            IMGstop.Source = new BitmapImage(new Uri("obrazky/stopC.png", UriKind.RelativeOrAbsolute));
            LBLstop.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3772FF"));
            
            BTNstart.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4B5D78"));
            IMGstart.Source = new BitmapImage(new Uri("obrazky/startH.png", UriKind.RelativeOrAbsolute));
            LBLstart.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4B5D78"));

            behTestu = false;
            await Task.Delay(1000);
            
            BTNstop.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4B5D78"));
            IMGstop.Source = new BitmapImage(new Uri("obrazky/stopH.png", UriKind.RelativeOrAbsolute));
            LBLstop.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4B5D78"));

            BTNstart.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8A94A6"));
            IMGstart.Source = new BitmapImage(new Uri("obrazky/start.png", UriKind.RelativeOrAbsolute));
            LBLstart.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8A94A6"));

            BTNstart.IsEnabled = true;
            TBTNtest.IsEnabled = true;
           
        }

        public void zatezovyTest(bool automatickyTest)
        {
            Stopwatch stopky = new Stopwatch();

            if (automatickyTest == true) //auto
            {
                stopky.Start();
                while(stopky.Elapsed.TotalSeconds < 61)
                {
                    trida.skore++;
                    update((int)stopky.Elapsed.TotalSeconds, automatickyTest);
                }
                stopky.Stop();

            }
            else //noAuto
            {
                stopky.Start();
                while (behTestu)
                {
                    trida.skore++;
                    update((int)stopky.Elapsed.TotalSeconds, automatickyTest);
                }
                update((int)stopky.Elapsed.TotalSeconds, automatickyTest);
                stopky.Stop();
            }
        }

        public void update(int cas, bool automatickyTest)
        {
            if (automatickyTest == true) //auto
            {
                Dispatcher.Invoke(() => { PBcas.Value = cas; });
                if (cas == 60)
                {
                    Dispatcher.Invoke(() =>
                    {
                        LBLskore.Visibility = Visibility.Visible;
                        LBLskore.Content = $"Skore: {trida.skore / cas}";

                        BTNstart.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8A94A6"));
                        IMGstart.Source = new BitmapImage(new Uri("obrazky/start.png", UriKind.RelativeOrAbsolute));
                        LBLstart.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8A94A6"));
                        BTNstart.IsEnabled = true;
                        TBTNtest.IsEnabled = true;
                    });
                }
            }
            else //noAuto
            {
                if (behTestu == true)
                {
                    Dispatcher.Invoke(() => { LBLcas.Content = $"Čas: {cas}s"; });
                }
                else
                {
                    Dispatcher.Invoke(() =>
                    {
                        LBLskore.Visibility = Visibility.Visible;
                        LBLskore.Content = $"Skore: {trida.skore / cas}";
                    });
                }
            }



        }

    }
}
