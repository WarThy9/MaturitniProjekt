﻿using Hardware.Info;
using LibreHardwareMonitor.Hardware;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.WPF;
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
        Computer computer = new Computer
        {
            IsCpuEnabled = true
        };


        public HomeWindow()
        {
            trida.globalniBeh = true;

            InitializeComponent();

            Thread test = new Thread(ziskavaniZateze);
            test.Start();

            if (trida.prvotniNacteni == 0)
            {
                trida.prvotniNacteni = 1;
                hardwareInfo.RefreshCPUList();
            }
            int pocetProcesoru = 0;
            foreach (var cpu in hardwareInfo.CpuList)
            {
                LBLnazev.Content = cpu.Name;
                LBLrychlost.Content = cpu.MaxClockSpeed;
                LBLjadra.Content = cpu.NumberOfCores;
                LBLlprocesory.Content = cpu.NumberOfLogicalProcessors;
                LBLmezi1.Content = (cpu.L1DataCacheSize + cpu.L1InstructionCacheSize) / 1024 + "kB";
                LBLmezi2.Content = cpu.L2CacheSize / 1024 / 1024 + "MB";
                LBLmezi3.Content = cpu.L3CacheSize / 1024 / 1024 + "MB";
                pocetProcesoru = (int)cpu.NumberOfLogicalProcessors;
            }

            vytvoreniGrafu(pocetProcesoru);

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

                Dispatcher.InvokeAsync(update, DispatcherPriority.SystemIdle);
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

                Dispatcher.InvokeAsync(update, DispatcherPriority.SystemIdle);
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
            Dispatcher.InvokeAsync(update, DispatcherPriority.SystemIdle);
        }

        #endregion zatezovyTest
        private async void ziskavaniZateze()
        {
            computer.Open();
            while (true) 
            {
                await Task.Delay(1000);
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
            
            }
        }

        private void kontrolaPoctuZaznamu(string nazevIndexu)
        {
            int pocetIndexu = trida.zatezJader[nazevIndexu].Count;
            if (pocetIndexu == 60)
            {
                trida.zatezJader[nazevIndexu].RemoveAt(0);
            }
        }

        private void vytvoreniGrafu(int pocetJader)
        {
            for (int i = 0; i < pocetJader; i++)
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

                CartesianChart chart1 = new CartesianChart();
                chart1.SetBinding(CartesianChart.SeriesProperty, new Binding("Series" + i));
                chart1.SetBinding(CartesianChart.XAxesProperty, new Binding("XAxes"));
                chart1.SetBinding(CartesianChart.YAxesProperty, new Binding("YAxes"));

                border1.Child = chart1;

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

                CartesianChart chart2 = new CartesianChart();
                chart2.SetBinding(CartesianChart.SeriesProperty, new Binding("Series" + i));
                chart2.SetBinding(CartesianChart.XAxesProperty, new Binding("XAxes"));
                chart2.SetBinding(CartesianChart.YAxesProperty, new Binding("YAxes"));

                border2.Child = chart2;

                wrapPanel.Children.Add(border1);
                wrapPanel.Children.Add(border2);

                grafy.Children.Add(wrapPanel);
            }
            
        }
    }
}
