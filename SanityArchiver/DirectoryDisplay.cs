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

        public DirectoryDisplay() { }

        public DirectoryDisplay(TreeView LibraryView, Label PathLbl)
        {
            DisplayDirectoryTreeView(LibraryView, PathLbl);
        }

        public void DisplayDirectoryTreeView(TreeView LibraryView, Label PathLbl)
        {
            DirectoryInfo rootDir = new DirectoryInfo(DefaultPath);
            if (!rootDir.Exists)
                return;

            PathLbl.Content = DefaultPath;

            foreach (DirectoryInfo dir in rootDir.GetDirectories())
            {
                try
                {
                    var item = new TreeViewItem();
                    item.Header = dir.ToString();
                    LibraryView.Items.Add(item);
                    //AddTreeItems(item, dir);
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

        public void AddFiles(WebBrowser FileBrowser, TreeViewItem RootDirectory)
        {
            //FileListBox.Items.Add(RootDirectory);
            string path = DefaultPath + "\\" + RootDirectory.Header.ToString();
            Uri uri = new Uri(path);
            FileBrowser.Navigate(uri);
            /*DirectoryInfo Root = new DirectoryInfo(RootDirectory);
            if (Root.Exists)
                return;
            foreach (FileInfo file in Root.GetFiles())
            {
                var item = new TreeViewItem();
                item.Header = file.ToString();
                FileListBox.Items.Add(item);
            }*/
        }

        public void Back(WebBrowser FileBrowser)
        {
            if (FileBrowser.CanGoBack)
            {
                FileBrowser.GoBack();
            }
        }

    }
}
