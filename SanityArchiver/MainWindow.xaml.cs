using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

namespace SanityArchiver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var item = new TreeViewItem();
            item.Header = "kak";
            LibraryView.Items.Add(item);
            var item2 = new TreeViewItem();
            item2.Header = "kk";
            LibraryView.Items.Add(item2);
            var item3 = new TreeViewItem();
            item3.Header = "kaaak";
            item.Items.Add(item3);
            DirectoryDisplay display = new DirectoryDisplay(lb);
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //find a file from the texbox
            //show a default structure on open
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
