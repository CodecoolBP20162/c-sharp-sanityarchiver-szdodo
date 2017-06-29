using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SanityArchiver
{
    public class FileHandler
    {
        static string DefaultPath = "C:\\Users\\Dodo";

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
            catch (FileNotFoundException)
            {
                System.Windows.MessageBox.Show("result.zip not found");
            }

        }

        public string GetPathFromBrowser(WebBrowser FileBrowser)
        {
            return FileBrowser.Source.PathAndQuery;
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
