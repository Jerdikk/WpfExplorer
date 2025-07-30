using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfExplorer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public TreeViewItem trItem;
        public MyFilesStruct selectedFileStruct;
        public MyFilesStruct selectedFileStruct2;
        public MainWindowModel model;
    public MapHash mapFilesHash;
        public MainWindow()
        {
            model = new MainWindowModel();
            mapFilesHash = new MapHash();
            InitializeComponent();
            this.DataContext = model;
            trView.Items.Clear();
            trView2.Items.Clear();
            model.Title = "---";
            model.Description = "+++";
            try
            {
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    TreeViewItem item = new TreeViewItem();
                    //item.Tag = drive;
                    item.Header = drive.ToString();
                    //item.Items.Add("*");
                    trView.Items.Add(item);
                    MyFilesStruct myFilesStruct = new MyFilesStruct();  
                    myFilesStruct.fileName = drive.Name;
                    myFilesStruct.fullName = drive.Name;
                    myFilesStruct.typeFile = TypesFile.Drive;
                    myFilesStruct.tag = drive;
                    item.Tag = myFilesStruct;
                    //  item.Header = myFilesStruct;

                    item = new TreeViewItem();
                    //item.Tag = drive;
                    item.Header = drive.ToString();
                    //item.Items.Add("*");
                    trView2.Items.Add(item);
                    myFilesStruct = new MyFilesStruct();
                    myFilesStruct.fileName = drive.Name;
                    myFilesStruct.fullName = drive.Name;
                    myFilesStruct.typeFile = TypesFile.Drive;
                    myFilesStruct.tag = drive;
                    item.Tag = myFilesStruct;


                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("1 : " + ex.Message + " --- " + ex.StackTrace);
            }
        }

        private void trView_Expanded(object sender, RoutedEventArgs e)
        {
            try
            {
                TreeViewItem item = (TreeViewItem)e.OriginalSource;

                if (item == null) return;

                MyFilesStruct myFilesStruct1 = item.Tag as MyFilesStruct;

                if (myFilesStruct1.tag is FileInfo)
                    return;

                item.Items.Clear();
                DirectoryInfo dir;

                if (myFilesStruct1.tag is DriveInfo)
                {
                    DriveInfo drive = (DriveInfo)myFilesStruct1.tag;
                    dir = drive.RootDirectory;
                }
                else
                    dir = (DirectoryInfo)myFilesStruct1.tag;

              
                foreach (DirectoryInfo subDir in dir.GetDirectories())
                {
                    TreeViewItem newItem = new TreeViewItem();
                   // newItem.Tag = subDir;
                    newItem.Header = subDir.ToString();
                    newItem.Items.Add("*");
                    item.Items.Add(newItem);
                    MyFilesStruct myFilesStruct = new MyFilesStruct();
                    myFilesStruct.typeFile = TypesFile.Directory;
                    myFilesStruct.fullName = subDir.FullName;
                    myFilesStruct.tag = subDir;
                    newItem.Tag = myFilesStruct;
                }
                foreach (FileInfo subDir in dir.GetFiles())
                {
                    TreeViewItem newItem = new TreeViewItem();
                   // newItem.Tag = subDir;
                    newItem.Header = subDir.ToString();
                    //newItem.Items.Add("*");
                    item.Items.Add(newItem);
                    MyFilesStruct myFilesStruct = new MyFilesStruct();
                    myFilesStruct.typeFile = TypesFile.File;
                    myFilesStruct.fullName = subDir.FullName;
                    myFilesStruct.fileName = subDir.Name;
                    myFilesStruct.tag = subDir;
                    newItem.Tag = myFilesStruct;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in Trview_expanded : " + ex.Message + " --- " + ex.StackTrace);
                int uu = 1;
            }

        }

        private void trView_Selected(object sender, RoutedEventArgs e)
        {
            try
            {
                trItem = (TreeViewItem)e.OriginalSource;

                if (trItem == null) return;

                //MyFilesStruct dir;
                selectedFileStruct = (MyFilesStruct)trItem.Tag;
                model.Title = selectedFileStruct.fullName;
                //dir.isSelected = true;

            }

            catch (Exception ex)
            {
                Debug.WriteLine("Error in trView_Selected " + ex.Message + " :: " + ex.StackTrace);
            }

        }

        private void trView2_Expanded(object sender, RoutedEventArgs e)
        {
            try
            {
                TreeViewItem item = (TreeViewItem)e.OriginalSource;

                if (item == null) return;

                MyFilesStruct myFilesStruct1 = item.Tag as MyFilesStruct;

                if (myFilesStruct1.tag is FileInfo)
                    return;

                item.Items.Clear();
                DirectoryInfo dir;

                if (myFilesStruct1.tag is DriveInfo)
                {
                    DriveInfo drive = (DriveInfo)myFilesStruct1.tag;
                    dir = drive.RootDirectory;
                }
                else
                    dir = (DirectoryInfo)myFilesStruct1.tag;


                foreach (DirectoryInfo subDir in dir.GetDirectories())
                {
                    TreeViewItem newItem = new TreeViewItem();
                    // newItem.Tag = subDir;
                    newItem.Header = subDir.ToString();
                    newItem.Items.Add("*");
                    item.Items.Add(newItem);
                    MyFilesStruct myFilesStruct = new MyFilesStruct();
                    myFilesStruct.typeFile = TypesFile.Directory;
                    myFilesStruct.fullName = subDir.FullName;
                    myFilesStruct.tag = subDir;
                    newItem.Tag = myFilesStruct;
                }
                foreach (FileInfo subDir in dir.GetFiles())
                {
                    TreeViewItem newItem = new TreeViewItem();
                    // newItem.Tag = subDir;
                    newItem.Header = subDir.ToString();
                    //newItem.Items.Add("*");
                    item.Items.Add(newItem);
                    MyFilesStruct myFilesStruct = new MyFilesStruct();
                    myFilesStruct.typeFile = TypesFile.File;
                    myFilesStruct.fullName = subDir.FullName;
                    myFilesStruct.fileName = subDir.Name;
                    myFilesStruct.tag = subDir;
                    newItem.Tag = myFilesStruct;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in Trview_expanded : " + ex.Message + " --- " + ex.StackTrace);
                int uu = 1;
            }

        }

        private void trView2_Selected(object sender, RoutedEventArgs e)
        {
            try
            {
                trItem = (TreeViewItem)e.OriginalSource;

                if (trItem == null) return;
                                
                selectedFileStruct2 = (MyFilesStruct)trItem.Tag;
                model.Description = selectedFileStruct2.fullName;
                

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in trView_Selected " + ex.Message + " :: " + ex.StackTrace);
            }
        }

        private void rightButt_Click(object sender, RoutedEventArgs e)
        {
            model.rightFileStruct = selectedFileStruct2;            

            try
            {
                if (selectedFileStruct2.typeFile == TypesFile.File)
                {
                   // byte[] result;
                    if (File.Exists(selectedFileStruct2.fullName))
                    {
                        GenerateMD5Hash(selectedFileStruct2);
                    }
                }
                else
                {
                    if (selectedFileStruct2.typeFile == TypesFile.Directory)
                    {
                        MyFilesStruct test = new MyFilesStruct();
                        test.tag = selectedFileStruct2.tag;
                        test.fullName = selectedFileStruct2.fullName;
                        test.typeFile = selectedFileStruct2.typeFile;
                        test.hashString = selectedFileStruct2.hashString;
                        test.isSelected = selectedFileStruct2.isSelected;
                        test.myFilesStructs = selectedFileStruct2.myFilesStructs;
                        if (test.myFilesStructs == null)
                            test.myFilesStructs = new ObservableCollection<MyFilesStruct> ();

                        DirectoryInfo directoryI = (DirectoryInfo)selectedFileStruct2.tag;
                        if (directoryI != null)
                        {
                            GetDirAndFiles(test, directoryI);

                        }
                        int hh = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                hashStr1.Text = "Failed calc MD5 hash";
            }

        }

        private static void GetDirAndFiles(MyFilesStruct test, DirectoryInfo directoryI)
        {
            foreach (DirectoryInfo subDir in directoryI.GetDirectories())
            {
                MyFilesStruct myFilesStruct = new MyFilesStruct();
                myFilesStruct.typeFile = TypesFile.Directory;
                myFilesStruct.fullName = subDir.FullName;
                myFilesStruct.tag = subDir;
                test.myFilesStructs.Add(myFilesStruct);
            }
            foreach (FileInfo files in directoryI.GetFiles())
            {
                MyFilesStruct myFilesStruct = new MyFilesStruct();
                myFilesStruct.typeFile = TypesFile.File;
                myFilesStruct.fullName = files.FullName;
                myFilesStruct.fileName = files.Name;
                myFilesStruct.tag = files;
                test.myFilesStructs.Add(myFilesStruct);
            }
        }

        private void GenerateMD5Hash(MyFilesStruct myFilesStruct)
        {
            byte[] result;
            using (HashAlgorithm hash = MD5.Create())
            {
                using (Stream stream = File.OpenRead(myFilesStruct.fullName))
                {
                    result = hash.ComputeHash(stream);

                    StringBuilder _stringBuilder = new StringBuilder(result.Length * 2);


                    foreach (byte value in result)
                    {
                        _stringBuilder.Append(value.ToString("x2"));
                    }


                    hashStr2.Text = _stringBuilder.ToString();
                    selectedFileStruct2.hashString = _stringBuilder.ToString();

                    mapFilesHash.Add(myFilesStruct.hashString, myFilesStruct.fullName);

                }
            }            
        }

        private void leftButt_Click(object sender, RoutedEventArgs e)
        {
            model.leftFileStruct = selectedFileStruct;

            try
            {
                if (selectedFileStruct.typeFile == TypesFile.File)
                {
                    byte[] result;
                    if (File.Exists(selectedFileStruct.fullName))
                    {
                        using (HashAlgorithm hash = MD5.Create())
                        {
                            using (Stream stream = File.OpenRead(selectedFileStruct.fullName))
                            {
                                result = hash.ComputeHash(stream);

                                StringBuilder _stringBuilder = new StringBuilder(result.Length * 2);
                                

                                foreach (byte value in result)
                                {
                                    _stringBuilder.Append(value.ToString("x2"));
                                }


                                hashStr1.Text = _stringBuilder.ToString();
                                selectedFileStruct.hashString = _stringBuilder.ToString();

                                mapFilesHash.Add(selectedFileStruct.hashString, selectedFileStruct.fullName);

                            }
                        }
                    }
                }
            }
            catch(Exception ex) 
            {
                Debug.WriteLine(ex.ToString());
                hashStr1.Text = "Failed calc MD5 hash";
            }

        }

        private void sync_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                using (StreamWriter outputFile = new StreamWriter("hashMD5files.txt"))
                {
                    foreach (MapHashValue mapHashValue in mapFilesHash)
                    {
                        string[] t1 = mapHashValue.ToString();
                        int count = t1.Count();
                        for (int i = 0; i < count; i++)
                            outputFile.WriteLine(t1[i]);
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                using (StreamReader reader = new StreamReader("hashMD5files.txt"))
                {
                    if (mapFilesHash == null)
                        mapFilesHash = new MapHash();
                    while(!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] t1 = line.Split('*');
                        mapFilesHash.Add(t1[0], t1[1]);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
    }
}
