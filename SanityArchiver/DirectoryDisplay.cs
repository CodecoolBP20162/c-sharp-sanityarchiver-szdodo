using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.IO.Compression;
using System.Windows.Controls.Primitives;
using System.Windows;

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
            DisplaySelectedDirectory(DefaultPath);
        }

        public void DisplaySelectedDirectory(string Path)
        {
            DirectoryInfo rootDir = new DirectoryInfo(Path);
            if (!rootDir.Exists)
                return;
            LibraryView.Items.Clear();
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
            this.DisplaySelectedDirectory(path);
            Uri uri = new Uri(path);
            FileBrowser.Navigate(uri);
        }

        public void CompressFile(TreeViewItem RootDirectory)
        {
            string StartPath = DefaultPath + "\\" + RootDirectory.Header.ToString();
            CompressFile(StartPath);
        }

        public void DecompressFile(TreeViewItem RootDirectory)
        {
            string StartPath = DefaultPath + "\\" + RootDirectory.Header.ToString();
            DecompressFile(StartPath);
        }

        public void CompressFile(string Path)
        {
            string ZipPath = Path + "/result.zip";
            try
            {
                ZipFile.CreateFromDirectory(Path, ZipPath);
            }
            catch (IOException) { }

        }

        public void DecompressFile(string Path)
        {
            string ExtractPath = Path + "/extract";
            string ZipPath = Path + "/result.zip";
            try
            {
                ZipFile.ExtractToDirectory(ZipPath, ExtractPath);
            }
            catch (FileNotFoundException) {
                System.Windows.MessageBox.Show("result.zip not found");
            }

        }

        public string GetPathFromBrowser()
        {
            return  FileBrowser.Source.PathAndQuery;
        }

        public void RenameFile(string OldName, string NewName)
        {
            try
            {
                System.IO.File.Move(OldName, NewName);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("The file must be in the selected directory.");
            }
        }
    }
}
