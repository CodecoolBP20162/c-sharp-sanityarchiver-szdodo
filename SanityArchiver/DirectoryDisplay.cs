using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.IO.Compression;

namespace SanityArchiver
{
    class DirectoryDisplay
    {
        static string DefaultPath = "C:\\Users\\Dodo";
        TreeView LibraryView;
        WebBrowser FileBrowser;
        TextBox SearchText;

        public DirectoryDisplay() { }

        public DirectoryDisplay(TreeView LibraryView, WebBrowser FileBrowser, TextBox SearchText) {
            this.LibraryView = LibraryView;
            this.FileBrowser = FileBrowser;
            this.SearchText = SearchText;
        }
        

        public void DisplayDirectoryTreeView()
        {
            DirectoryInfo rootDir = new DirectoryInfo(DefaultPath);
            if (!rootDir.Exists)
                return;


            foreach (DirectoryInfo dir in rootDir.GetDirectories())
            {
                try
                {
                    var item = new TreeViewItem();
                    item.Header = dir.ToString();
                    LibraryView.Items.Add(item);
                    CheckIfEmpty(item, dir);
                }
                catch (UnauthorizedAccessException) { continue; }
            }
        }

        private void CheckIfEmpty(TreeViewItem Root, DirectoryInfo RootDirectory)
        {
            int NumberOfSubDir = RootDirectory.GetDirectories().Length;
            int NumberOfFiles = RootDirectory.GetFiles().Length;
            var itemSubDir = new TreeViewItem();
            itemSubDir.Header = "Subdirectories: " + NumberOfSubDir;
            var itemFiles = new TreeViewItem();
            itemFiles.Header = "Files: " + NumberOfFiles;
            Root.Items.Add(itemSubDir);
            Root.Items.Add(itemFiles);
        }

        private void AddTreeItems(TreeViewItem Root, DirectoryInfo RootDirectory)
        {
            foreach (DirectoryInfo dir in RootDirectory.GetDirectories())
            {
                try
                {
                    var item = new TreeViewItem();
                    item.Header = dir.ToString();
                    Root.Items.Add(item);
                    AddTreeItems(item, dir);
                }
                catch (UnauthorizedAccessException) { continue; }
            }
            
        }

        public void LoadBrowser(TreeViewItem RootDirectory)
        {
            try
            {
                string path = DefaultPath + "\\" + RootDirectory.Header.ToString();
                Uri uri = new Uri(path);
                FileBrowser.Navigate(uri);
            }
            catch (NullReferenceException) { }
        }

        public void LoadBrowser(string Path)
        {
            try
            {
                Uri uri = new Uri(Path);
                FileBrowser.Navigate(uri);
            }
            catch (UriFormatException) {
                SearchText.Text = "Search";
            }
        }

        public void Back()
        {
            if (FileBrowser.CanGoBack)
                FileBrowser.GoBack();
        }

        public void Forward()
        {
            if (FileBrowser.CanGoForward)
                FileBrowser.GoForward();
        }

        public void Home()
        {
            string path = DefaultPath + "\\";
            Uri uri = new Uri(path);
            FileBrowser.Navigate(uri);
        }

        public void ComprassFile(TreeViewItem RootDirectory)
        {
            string StartPath = DefaultPath + "\\" + RootDirectory.Header.ToString();
            string ZipPath = StartPath+"\\result.zip";
            string ExtractPath = StartPath+"\\extract";
            //FileBrowser.Cursor

            ZipFile.CreateFromDirectory(StartPath, ZipPath);

            ZipFile.ExtractToDirectory(ZipPath, ExtractPath);
        }

    }
}
