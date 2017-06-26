using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SanityArchiver
{
    class DirectoryDisplay
    {
        static string DefaultPath = "C:\\Users\\Dodo";



        public DirectoryDisplay(ListBox lb)
        {
            DirectoryInfo rootDir = new DirectoryInfo(DefaultPath);
            if (!rootDir.Exists)
            {
                lb.Items.Add("nincs ilyen");
            }
            else
            {
                lb.Items.Add("van ilyen");
            }

            foreach (DirectoryInfo fil in rootDir.GetDirectories())
            {
                lb.Items.Add(fil.ToString());
            }

        }
    }
}
