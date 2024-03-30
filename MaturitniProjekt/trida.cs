using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;

namespace MaturitniProjekt
{
    public static class trida
    {
        public static Dictionary<string, List<int>> zatezJader = new Dictionary<string, List<int>>();
        public static List<int> dataX = new List<int>();
        public static int prvotniNacteni { get; set; } = 0;
        public static long skore { get; set; }
        public static bool globalniBeh { get; set; } = true;
    }
}
