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
        static DirectoryDisplay display;

        public MainWindow()
        {
            InitializeComponent();
            display = new DirectoryDisplay(LibraryView, PathLabel);
        }

       

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //display = new DirectoryDisplay(LibraryView);
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //find a file from the texbox
            //show a default structure on open
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Item_DoubleClick(object sender, RoutedEventArgs e)
        {
            //display.AddFiles(FileBrowser, LibraryView.SelectedItem.GetType());
            TreeViewItem item = (TreeViewItem)LibraryView.SelectedItem;
            //PathLabel.Content = item.Header.ToString();
            display.AddFiles(FileBrowser, item);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            display.Back(FileBrowser);
        }
    }
}
