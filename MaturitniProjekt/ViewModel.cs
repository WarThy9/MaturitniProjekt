using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Windows.Ink;
using System.Windows;
using LibreHardwareMonitor.Hardware.Storage;

namespace MaturitniProjekt
{
    public class ViewModel
    {


        public ISeries[] Series { get; set; }
            = new ISeries[]
            {
                new LineSeries<double>
                {
                    Values = new double[] {10,56,45,89,36,54},
                    Fill = new SolidColorPaint(new SKColor(55, 114, 255)),
                    LineSmoothness = 0,
                    GeometrySize = 0,
                }
            };

        public Axis[] XAxes { get; set; } =
           {
            new Axis
            {
                MinLimit = 0,
                MaxLimit = 60,
                IsVisible = false,
            }
        };

        public Axis[] YAxes { get; set; } =
           {
            new Axis
            {
                MinLimit = 0,
                MaxLimit = 100,
                IsVisible = false,
            }
        };
    }
}
