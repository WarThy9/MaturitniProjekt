using ScottPlot.WPF;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.TextFormatting;

namespace MaturitniProjekt
{
    public static class globalniPromenne
    {
        
        public static bool prvotniNacteni { get; set; } = false;
        public static bool globalniBeh { get; set; } = true;
        public static string cestaKDatabazi { get; set; }

    }

}
