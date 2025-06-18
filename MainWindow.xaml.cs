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
        public ObservableCollection<MyFilesStruct> myFilesStructs { get; set; } = new ObservableCollection<MyFilesStruct>();
        public MainWindow()
        {
            InitializeComponent();

            trView.Items.Clear();
            try
            {
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    TreeViewItem item = new TreeViewItem();
                    item.Tag = drive;
                    item.Header = drive.ToString();
                    item.Items.Add("*");
                    trView.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("tabletsettings 1 : " + ex.Message + " --- " + ex.StackTrace);
            }
        }

        private void trView_Expanded(object sender, RoutedEventArgs e)
        {
            try
            {
                TreeViewItem item = (TreeViewItem)e.OriginalSource;

                if (item == null) return;

                item.Items.Clear();
                DirectoryInfo dir;

                if (item.Tag is DriveInfo)
                {
                    DriveInfo drive = (DriveInfo)item.Tag;
                    dir = drive.RootDirectory;
                }
                else
                    dir = (DirectoryInfo)item.Tag;

                myFilesStructs.Clear();

                FileInfo[] fileInfos = dir.GetFiles("*.*");
                   


                foreach (FileInfo file in fileInfos)
                {
                    MyFilesStruct myFilesStruct = new MyFilesStruct();
                   
                    {
                        myFilesStruct.fileName = file.Name;
                        myFilesStruct.isSelected = false;
                    }
                    myFilesStructs.Add(myFilesStruct);
                }

               // cvMainVMM.selectedDirectory = dir.FullName;
               // GetAllPadFiles(dir.FullName);

                foreach (DirectoryInfo subDir in dir.GetDirectories())
                {
                    TreeViewItem newItem = new TreeViewItem();
                    newItem.Tag = subDir;
                    newItem.Header = subDir.ToString();
                    newItem.Items.Add("*");
                    item.Items.Add(newItem);
                }
                foreach (FileInfo subDir in dir.GetFiles())
                {
                    TreeViewItem newItem = new TreeViewItem();                    
                    newItem.Tag = subDir;
                    newItem.Header = subDir;
                    //newItem.Items.Add("*");
                    item.Items.Add(newItem);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in TabletSettings Trview_expanded : " + ex.Message + " --- " + ex.StackTrace);
                int uu = 1;
            }

        }

        private void trView_Selected(object sender, RoutedEventArgs e)
        {
            try
            {
                trItem = (TreeViewItem)e.OriginalSource;

                if (trItem == null) return;

                DirectoryInfo dir;
                if (trItem.Tag is DriveInfo)
                {
                    DriveInfo drive = (DriveInfo)trItem.Tag;
                    dir = drive.RootDirectory;
                }
                else
                    dir = (DirectoryInfo)trItem.Tag;

                myFilesStructs.Clear();
                FileInfo[] fileInfos = dir.GetFiles("*.*");

            //    if ((cvMainVMM.selectedGeoTablet != null) && (cvMainVMM.selectedGeoTablet.fileName != null) && (cvMainVMM.selectedGeoTablet.fileName.ToLower().IndexOf(".pad") == -1))
            //        cvMainVMM.selectedGeoTablet.fileName += ".pad";

                foreach (FileInfo file in fileInfos)
                {
                    MyFilesStruct myFilesStruct = new MyFilesStruct();

                  /*  if ((cvMainVMM.selectedGeoTablet != null) && (cvMainVMM.selectedGeoTablet.fileName != null) && (file.Name.ToUpper() == cvMainVMM.selectedGeoTablet.fileName.ToUpper()))
                    {
                        if (dir.FullName.ToUpper() == GlobalData.Instance.geoProfile.fullTabletPath.ToUpper())
                        {
                            myFilesStruct.fileName = file.Name + " *";
                            myFilesStruct.isSelected = true;
                        }
                        else
                        {
                            myFilesStruct.fileName = file.Name;
                            myFilesStruct.isSelected = false;
                        }
                    }
                    else*/
                    {
                        myFilesStruct.fileName = file.Name;
                        myFilesStruct.isSelected = false;
                    }
                    myFilesStructs.Add(myFilesStruct);
                }
                //cvMainVMM.selectedDirectory = dir.FullName;
                //cvMainVMM.currDirectory = dir.FullName;

               // GetAllPadFiles(dir.FullName);
            }

            catch (Exception ex)
            {
                Debug.WriteLine("Error in trView_Selected " + ex.Message + " :: " + ex.StackTrace);
            }

        }
    }
}
