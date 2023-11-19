﻿using Hardware.Info;
using System;
using System.Collections.Generic;
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

namespace MaturitniProjekt
{
    /// <summary>
    /// Interakční logika pro HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Page
    {
        static readonly IHardwareInfo hardwareInfo = new HardwareInfo();
        public HomeWindow()
        {
            InitializeComponent();

            hardwareInfo.RefreshCPUList();
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

            if (TBTNtest.IsChecked == false)
            {
                await Task.Delay(5000);
                BTNstart.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8A94A6"));
                IMGstart.Source = new BitmapImage(new Uri("obrazky/start.png", UriKind.RelativeOrAbsolute));
                LBLstart.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8A94A6"));
                BTNstart.IsEnabled = true;
                TBTNtest.IsEnabled = true;
            }
            else if (TBTNtest.IsChecked == true)
            {
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
    }
}
