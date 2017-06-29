using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SanityArchiver
{
    public class FileHandler
    {
        static string DefaultPath = "C:\\Users\\Dodo";
        static AesCryptoServiceProvider myAes;

        public FileHandler() {
            myAes = new AesCryptoServiceProvider();
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

        public void EncryptFile(string Path, string FileNamePath)
        {
            string plainText = ReadFile(FileNamePath);
            try
            {
                byte[] Encypted = Encryption.EncryptStringToBytes_Aes(plainText, myAes.Key, myAes.IV);
                ByteArrayToFile(Path+"\\crypted.txt", Encypted);

            }
            catch (ArgumentNullException) { MessageBox.Show("Missing Data"); }

        }

        public void DecryptFile(string Path, string FileNamePath)
        {
            byte[] cipherText = ReadBytes(FileNamePath);
            try
            {
                string Decrypted = Encryption.DecryptStringFromBytes_Aes(cipherText, myAes.Key, myAes.IV);
                File.WriteAllText(Path + "\\decrypted.txt", Decrypted);
            }
            catch (ArgumentNullException) { MessageBox.Show("Missing Data"); }
            catch (CryptographicException) { MessageBox.Show("Decrypting failed"); }

        }

        public string ReadFile(string Path)
        {
            string readText = null;
            try
            {
                readText = File.ReadAllText(Path);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("No file");
            }
            return readText;
        }

        public byte[] ReadBytes(string Path)
        {
            byte[] readText = null;
            try
            {
                readText = File.ReadAllBytes(Path);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("No file");
            }
            return readText;
        }

        public bool ByteArrayToFile(string fileName, byte[] byteArray)
        {
            try
            {
                using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(byteArray, 0, byteArray.Length);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in process: {0}", ex);
                return false;
            }
        }

    }
}
