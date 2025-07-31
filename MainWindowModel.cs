using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfExplorer
{
    public class MainWindowModel:BaseViewModel
    {
        private string _title;
        private string _description;
        public string Title
            { get { return _title; } set { _title = value; OnPropertyChanged(nameof(Title)); } }
        public string Description
            { get { return _description; } set {_description = value; OnPropertyChanged(nameof(Description)); } }

        public MyFilesStruct leftFileStruct { get; set; }
        public MyFilesStruct rightFileStruct { get; set; }

        public Tree<MyFilesStruct> leftTree;
        public Tree<MyFilesStruct> rightTree;

    }
}
