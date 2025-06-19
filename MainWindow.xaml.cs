using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
    
        public MainWindow()
        {
            model = new MainWindowModel();
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

                //MyFilesStruct dir;
                selectedFileStruct2 = (MyFilesStruct)trItem.Tag;
                model.Description = selectedFileStruct2.fullName;
                //dir.isSelected = true;

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in trView_Selected " + ex.Message + " :: " + ex.StackTrace);
            }
        }

        private void butt2_Click(object sender, RoutedEventArgs e)
        {
            model.file2 = selectedFileStruct2;
        }

        private void butt1_Click(object sender, RoutedEventArgs e)
        {
            model.file1 = selectedFileStruct;
        }

        private void sync_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
