using System.Windows.Media;
using Hardware.Info;
using LibreHardwareMonitor.Hardware;
using ScottPlot;
using ScottPlot.WPF;
using System;
using System.Collections;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;
using Color = System.Windows.Media.Color;
using System.Windows.Markup;

namespace MaturitniProjekt
{
    /// <summary>
    /// Interakční logika pro HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Page
    {
        WpfPlot[] myPlots = new WpfPlot[Environment.ProcessorCount + 1];
        static IHardwareInfo hardwareInfo = new HardwareInfo();
        private bool behTestu = false;
        private bool behTestuAuto = true;
        Stopwatch stopky = new Stopwatch();
        Computer computer = new Computer
        {
            IsCpuEnabled = true
        };


        public HomeWindow()
        {
            trida.globalniBeh = true;
            Dispatcher.InvokeAsync(ziskavaniZateze, DispatcherPriority.SystemIdle);
            vytvoreniGrafu();

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

        #region saveHover
        private void BTNsave_MouseEnter(object sender, MouseEventArgs e)
        {
            if (BTNsave.BorderBrush is SolidColorBrush solidColorBrush && solidColorBrush.Color == (Color)ColorConverter.ConvertFromString("#8A94A6"))
            {
                BTNsave.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4B5D78"));
                IMGsave.Source = new BitmapImage(new Uri("obrazky/saveH.png", UriKind.RelativeOrAbsolute));
                LBLsave.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4B5D78"));
            }
        }

        private void BTNsave_MouseLeave(object sender, MouseEventArgs e)
        {
            if (BTNsave.BorderBrush is SolidColorBrush solidColorBrush && solidColorBrush.Color == (Color)ColorConverter.ConvertFromString("#4B5D78"))
            {
                BTNsave.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8A94A6"));
                IMGsave.Source = new BitmapImage(new Uri("obrazky/save.png", UriKind.RelativeOrAbsolute));
                LBLsave.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8A94A6"));
            }
        }
        #endregion saveHover

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


        private void BTNsave_Click(object sender, RoutedEventArgs e)
        {
            BTNsave.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3772FF"));
            IMGsave.Source = new BitmapImage(new Uri("obrazky/saveC.png", UriKind.RelativeOrAbsolute));
            LBLsave.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3772FF"));
        }


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
        #region zatezovyTest
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
                if (automatickyTest == true)
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

                vysledek += (float)Math.Sqrt(i + indexJadra);
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
        }

        #endregion zatezovyTest
        private async void ziskavaniZateze()
        {
            computer.Open();

                await Task.Delay(950);
                foreach (var hardwareItem in computer.Hardware)
                {
                    if (hardwareItem.HardwareType == HardwareType.Cpu)
                    {
                        hardwareItem.Update();

                        foreach (var sensor in hardwareItem.Sensors)
                        {
                            if (sensor.SensorType == SensorType.Load)
                            {
                                float? neco = sensor.Value;
                                int formatovanaZatez = (int)sensor.Value;

                                if (sensor.Name.Contains("Total"))
                                {
                                    if (!trida.zatezJader.ContainsKey("total"))
                                    {
                                        trida.zatezJader["total"] = new List<int>();
                                    }
                                    kontrolaPoctuZaznamu("total");
                                    trida.zatezJader["total"].Add(formatovanaZatez);
                                }
                                else if (sensor.Name.Contains("#"))
                                {
                                    string formatovaneJmeno = sensor.Name.Substring(sensor.Name.IndexOf("#") + 1);
                                    if (!trida.zatezJader.ContainsKey(formatovaneJmeno))
                                    {
                                        trida.zatezJader[formatovaneJmeno] = new List<int>();
                                    }
                                    kontrolaPoctuZaznamu(formatovaneJmeno);
                                    trida.zatezJader[formatovaneJmeno].Add(formatovanaZatez);
                                }
                            }
                        }
                    }

                }
            aktualizaceGrafu();
            update();
            Dispatcher.InvokeAsync(ziskavaniZateze, DispatcherPriority.SystemIdle);
            
        }

        private void kontrolaPoctuZaznamu(string nazevIndexu)
        {
            int pocetIndexu = trida.zatezJader[nazevIndexu].Count;
            if (pocetIndexu == 60)
            {
                trida.zatezJader[nazevIndexu].RemoveAt(0);
            }
        }

        private void aktualizaceGrafu()
        {
            trida.dataX.Clear();
            for (int f = 0; f < trida.zatezJader["total"].Count; f++)
            {
                trida.dataX.Add(f);
            }
            pridaniScatteru(12, "total");

            for (int i = 0; i < Environment.ProcessorCount; i++)
            {
                int formatIndexu = i + 1;
                string indexJadra = formatIndexu.ToString();

                pridaniScatteru(i, indexJadra);
            }
        }

        private void pridaniScatteru(int indexGrafu, string indexJadra)
        {
            var scatter = myPlots[indexGrafu].Plot.Add.ScatterLine(trida.dataX, trida.zatezJader[indexJadra]);
            scatter.LineWidth = 3;
            scatter.Color = ScottPlot.Color.FromHex("#3772FF");
            myPlots[indexGrafu].Refresh();
        }


        private void vytvoreniGrafu()
        {
            ScottPlot.Control.InputBindings customInputBindings = new() { };

            myPlots[12] = new WpfPlot
            {
                Height = 150,
                Width = 531,
            };

            myPlots[12].Plot.Axes.SetLimits(0, 60, -5, 100);
            myPlots[12].Plot.Axes.Left.IsVisible = false;
            myPlots[12].Plot.Axes.Bottom.IsVisible = false;
            myPlots[12].Plot.Axes.Right.IsVisible = false;
            myPlots[12].Plot.Axes.Top.IsVisible = false;
            ScottPlot.Control.Interaction interaction0 = new(myPlots[0])
            {
                Inputs = customInputBindings,
                Actions = ScottPlot.Control.PlotActions.NonInteractive(),
            };
            myPlots[12].Interaction = interaction0;

            graf.Child = myPlots[12];


            for (int i = 0; i < Environment.ProcessorCount; i++)
            {
                WrapPanel wrapPanel = new WrapPanel
                {
                    Margin = new Thickness(0, 0, 0, 10)
                };

                
                Border border1 = new Border
                {
                    Height = 100,
                    Width = 260.5,
                    Background = new SolidColorBrush(Color.FromRgb(19, 23, 37)),
                    CornerRadius = new CornerRadius(10),
                    Padding = new Thickness(5)
                };

                myPlots[i] = new WpfPlot
                {
                    Height = 100,
                    Width = 260.5,
                };

                myPlots[i].Plot.Axes.SetLimits(0, 60, -13, 100);
                myPlots[i].Plot.Axes.Left.IsVisible = false;
                myPlots[i].Plot.Axes.Bottom.IsVisible = false;
                myPlots[i].Plot.Axes.Right.IsVisible = false;
                myPlots[i].Plot.Axes.Top.IsVisible = false; 
                ScottPlot.Control.Interaction interaction = new(myPlots[i])
                {
                    Inputs = customInputBindings,
                    Actions = ScottPlot.Control.PlotActions.NonInteractive(),
                };
                myPlots[i].Interaction = interaction;

                border1.Child = myPlots[i];

                Border border2 = new Border
                {
                    Height = 100,
                    Width = 260.5,
                    Background = new SolidColorBrush(Color.FromRgb(19, 23, 37)),
                    CornerRadius = new CornerRadius(10),
                    Margin = new Thickness(10, 0, 0, 0),
                    Padding = new Thickness(5)
                };

                i++;

                myPlots[i] = new WpfPlot
                {
                    Height = 100,
                    Width = 260.5,
                };

                myPlots[i].Plot.Axes.SetLimits(0, 60, -13, 100);
                myPlots[i].Plot.Axes.Left.IsVisible = false;
                myPlots[i].Plot.Axes.Bottom.IsVisible = false;
                myPlots[i].Plot.Axes.Right.IsVisible = false;
                myPlots[i].Plot.Axes.Top.IsVisible = false;
                
                ScottPlot.Control.Interaction interaction1 = new(myPlots[i])
                {
                    Inputs = customInputBindings,
                    Actions = ScottPlot.Control.PlotActions.NonInteractive(),
                };
                myPlots[i].Interaction = interaction1;




                border2.Child = myPlots[i];

                wrapPanel.Children.Add(border1);
                wrapPanel.Children.Add(border2);

                grafy.Children.Add(wrapPanel);
            }
        }

    }
}
