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
        private bool behTestu = false;
        private bool behTestuAuto = true;
        Stopwatch stopky = new Stopwatch();

        public HomeWindow()
        {
            trida.globalniBeh = true;

            InitializeComponent();

            if (trida.prvotniNacteni == 0)
            {
                trida.prvotniNacteni = 1;
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

        private async void BTNstart_Click(object sender, RoutedEventArgs e)
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
                behTestuAuto = true;
                LBLcas.Visibility = Visibility.Hidden;
                BRDpb.Visibility = Visibility.Visible;

                BTNstart.Dispatcher.InvokeAsync(update, DispatcherPriority.SystemIdle);
                await zatezovyTest(true);

                LBLskore.Visibility = Visibility.Visible;
                LBLskore.Content = $"Skore: {(int)(trida.skore / 100 / stopky.Elapsed.TotalSeconds)}";

                BTNstart.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8A94A6"));
                IMGstart.Source = new BitmapImage(new Uri("obrazky/start.png", UriKind.RelativeOrAbsolute));
                LBLstart.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8A94A6"));
                BTNstart.IsEnabled = true;
                TBTNtest.IsEnabled = true;
            }
            else if (TBTNtest.IsChecked == true) //noAuto
            {
                behTestuAuto = false;
                behTestu = true;
                LBLcas.Visibility = Visibility.Visible;
                BRDpb.Visibility = Visibility.Hidden;

                BTNstop.IsEnabled = true;
                BTNstop.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8A94A6"));
                IMGstop.Source = new BitmapImage(new Uri("obrazky/stop.png", UriKind.RelativeOrAbsolute));
                LBLstop.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8A94A6"));

                BTNstart.Dispatcher.InvokeAsync(update, DispatcherPriority.SystemIdle);
                await zatezovyTest(false);

                LBLskore.Visibility = Visibility.Visible;
                LBLskore.Content = $"Skore: {(int)(trida.skore / 100 / stopky.Elapsed.TotalSeconds)}";
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

        public async Task zatezovyTest(bool automatickyTest)
        {
            stopky.Reset();
            stopky.Start();
            if (automatickyTest == true)
            {
                while (stopky.Elapsed.TotalSeconds < 61 && trida.globalniBeh == true)
                {
                    Task[] tasky = new Task[Environment.ProcessorCount];
                    for (int i = 0; i < tasky.Length; i++)
                    {
                        int indexJadra = i;
                        tasky[i] = Task.Run(() => zatezovaOperace(indexJadra, automatickyTest));
                    }

                    await Task.WhenAll(tasky);
                }
            }
            else
            {
                while (behTestu == true && trida.globalniBeh == true)
                {
                    Task[] tasky = new Task[Environment.ProcessorCount];
                    for (int i = 0; i < tasky.Length; i++)
                    {
                        int indexJadra = i;
                        tasky[i] = Task.Run(() => zatezovaOperace(indexJadra, automatickyTest));
                    }

                    await Task.WhenAll(tasky);
                }
            }
            
        }
        private void zatezovaOperace(int indexJadra, bool automatickyTest)
        {
            // Simulate intensive computation
            float vysledek = 0;
            for (int i = 0; i < int.MaxValue; i++)
            {
                if (automatickyTest == true )
                {
                    if (stopky.Elapsed.TotalSeconds >= 60 || trida.globalniBeh == false)
                    {
                        stopky.Stop();
                        return;
                    }
                }      
                else
                {
                    if (behTestu == false || trida.globalniBeh == false)
                    {
                        stopky.Stop();
                        return; 
                    }
                }
                
                vysledek += Math.Sqrt(i + indexJadra);
                trida.skore++;
            }
        }

        private async void update()
        {
             if (behTestuAuto == true)
            {
                PBcas.Value = stopky.Elapsed.TotalSeconds;
            }
            else if (behTestu == true)
            {
                LBLcas.Content = $"Čas: {(int)stopky.Elapsed.TotalSeconds}s";
            }
            else
            {
                return;
            }
            BTNstart.Dispatcher.InvokeAsync(update, DispatcherPriority.SystemIdle);
        }
    }
}
