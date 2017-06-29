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
       
        //minden funnkciónak külön class, pls compress vagy ezek együtt egy, de ne a displayben!
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            display.Home();
        }

        private void CompressBtn_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)LibraryView.SelectedItem;
            if (item is null)
            {
                string Path=display.GetPathFromBrowser();
                display.CompressFile(Path);
            }
            else
            {
                display.CompressFile(item);
            }
        }

        private void DecompressBtn_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)LibraryView.SelectedItem;
            if (item is null)
            {
                string Path = display.GetPathFromBrowser();
                display.DecompressFile(Path);
            }
            else
            {
                display.DecompressFile(item);
            }
        }

        private void CryptBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ModifyBtn_Click(object sender, RoutedEventArgs e)
        {
            OldNameTxt.Visibility = Visibility.Visible;
            NewNameTxt.Visibility = Visibility.Visible;
            RenameBtn.Visibility = Visibility.Visible;
            string Path = display.GetPathFromBrowser();
            display.DisplaySelectedDirectory(Path);
        }
        private void RenameBtn_Click(object sender, RoutedEventArgs e)
        {
            string Path = display.GetPathFromBrowser();
            string OldName = Path + "\\" + OldNameTxt.Text;
            string NewName = Path + "\\" + NewNameTxt.Text;
            if (OldName is null|| NewName is null)
            {
                MessageBox.Show("The files are neede.");
            }
            display.RenameFile(OldName, NewName);
            OldNameTxt.Visibility = Visibility.Hidden;
            NewNameTxt.Visibility = Visibility.Hidden;
            RenameBtn.Visibility = Visibility.Hidden;
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

        private void InfoBtn_Click(object sender, RoutedEventArgs e)
        {
            string Path = display.GetPathFromBrowser();
            display.DisplaySelectedDirectory(Path);
        }
    }
}
