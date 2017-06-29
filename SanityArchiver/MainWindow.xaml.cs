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
        static FileHandler handler;

        public MainWindow()
        {
            InitializeComponent();
            display = new DirectoryDisplay(LibraryView, FileBrowser, SearchTxt);
            display.DisplayDirectoryTreeView();
            handler = new FileHandler();
        }
       
        //minden funnkciónak külön class, pls compress vagy ezek együtt egy, de ne a displayben!
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            display.Home();
        }

        private void CompressBtn_Click(object sender, RoutedEventArgs e)
        {
            HideAllElements();
            TreeViewItem item = (TreeViewItem)LibraryView.SelectedItem;
            if (item is null)
            {
                string Path=handler.GetPathFromBrowser(FileBrowser);
                handler.CompressFile(Path);
            }
            else
            {
                handler.CompressFile(item);
            }
        }

        private void DecompressBtn_Click(object sender, RoutedEventArgs e)
        {
            HideAllElements();
            TreeViewItem item = (TreeViewItem)LibraryView.SelectedItem;
            if (item is null)
            {
                string Path = handler.GetPathFromBrowser(FileBrowser);
                handler.DecompressFile(Path);
            }
            else
            {
                handler.DecompressFile(item);
            }
        }

        private void CryptMenu_Click(object sender, RoutedEventArgs e)
        {
            HideAllElements();
            OldNameTxt.Visibility = Visibility.Visible;
            OldNameTxt.Text = "File Name";
            EncryptBtn.Visibility = Visibility.Visible;
            string Path = handler.GetPathFromBrowser(FileBrowser);
            display.DisplaySelectedDirectory(Path);
        }

        private void EncryptBtn_Click(object sender, RoutedEventArgs e)
        {
            string Path = handler.GetPathFromBrowser(FileBrowser);
            string OldName = Path + "\\" + OldNameTxt.Text;
            if (OldName is null)
            {
                MessageBox.Show("Missing file name.");
            }
            handler.EncryptFile(Path, OldName);
            OldNameTxt.Visibility = Visibility.Hidden;
            EncryptBtn.Visibility = Visibility.Hidden;
        }

        private void DeCryptMenu_Click(object sender, RoutedEventArgs e)
        {
            HideAllElements();
            OldNameTxt.Visibility = Visibility.Visible;
            OldNameTxt.Text = "File Name";
            DecryptBtn.Visibility = Visibility.Visible;
            string Path = handler.GetPathFromBrowser(FileBrowser);
            display.DisplaySelectedDirectory(Path);
        }

        private void DecryptBtn_Click(object sender, RoutedEventArgs e)
        {
            string Path = handler.GetPathFromBrowser(FileBrowser);
            string OldName = Path + "\\" + OldNameTxt.Text;
            if (OldName is null)
            {
                MessageBox.Show("Missing file name.");
            }
            handler.DecryptFile(Path, OldName);
            OldNameTxt.Visibility = Visibility.Hidden;
            DecryptBtn.Visibility = Visibility.Hidden;
        }

        private void ModifyBtn_Click(object sender, RoutedEventArgs e)
        {
            HideAllElements();
            OldNameTxt.Visibility = Visibility.Visible;
            NewNameTxt.Visibility = Visibility.Visible;
            RenameBtn.Visibility = Visibility.Visible;
            string Path = handler.GetPathFromBrowser(FileBrowser);
            display.DisplaySelectedDirectory(Path);
        }

        private void RenameBtn_Click(object sender, RoutedEventArgs e)
        {
            string Path = handler.GetPathFromBrowser(FileBrowser);
            string OldName = Path + "\\" + OldNameTxt.Text;
            string NewName = Path + "\\" + NewNameTxt.Text;
            if (OldName is null|| NewName is null)
            {
                MessageBox.Show("Missing file names.");
            }
            handler.RenameFile(OldName, NewName);
            HideAllElements();
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
            string Path = handler.GetPathFromBrowser(FileBrowser);
            display.DisplaySelectedDirectory(Path);
        }

        private void HideAllElements()
        {
            OldNameTxt.Visibility = Visibility.Hidden;
            NewNameTxt.Visibility = Visibility.Hidden;
            RenameBtn.Visibility = Visibility.Hidden;
            EncryptBtn.Visibility = Visibility.Hidden;
            DecryptBtn.Visibility = Visibility.Hidden;
        }
    }
}
