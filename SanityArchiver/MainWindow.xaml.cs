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
            display = new DirectoryDisplay(LibraryView, FileBrowser, SearchTxt);
            display.DisplayDirectoryTreeView();
        }
       

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            display.Home();
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //find a file from the texbox
            //show a default structure on open
        }

        private void CompressBtn_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)LibraryView.SelectedItem;
            if (item is null)
            {
                MessageBox.Show("First Select an item from the Currenct Directories list.");
            }
            else
            {
                display.ComprassFile(item);
            }
        }

        private void CryptBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ModifyBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Item_DoubleClick(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)LibraryView.SelectedItem;
            display.LoadBrowser(item);
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            string NewPath = SearchTxt.Text;
            display.LoadBrowser(NewPath);
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            display.Back();
        }

        private void ForwardBtn_Click(object sender, RoutedEventArgs e)
        {
            display.Forward();
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            display.Home();
        }
    }
}
