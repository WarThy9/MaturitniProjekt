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

namespace MaturitniProjekt
{
    public class ViewModel
    {
        public ISeries[] Series { get; set; }
            = new ISeries[]
            {
                new LineSeries<double>
                {
                    Values = new double[] { 2, 1, 3, 5, 3, 2, 1, 3, 5, 3, },
                    Fill = new SolidColorPaint(new SKColor(55, 114, 255)),
                    LineSmoothness = 0,
                    GeometrySize = 0,
                    Stroke = new SolidColorPaint(new SKColor(55, 114, 255)) { StrokeThickness = 2 },
                }
            };

        public Axis[] XAxes { get; set; } =
           {
            new Axis
            {
               
                MinLimit = 0,
                MaxLimit = 60,
            
            }
        };

        public Axis[] YAxes { get; set; } =
           {
            new Axis
            {
                
                MinLimit = 0,
                MaxLimit = 100,

            }
        };
    }
}
