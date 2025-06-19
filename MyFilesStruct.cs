using System.Collections.ObjectModel;

namespace WpfExplorer
{
    public enum TypesFile
    {
        File,
        Directory,
        Drive
    }
    public class MyFilesStruct : BaseViewModel
    {
        private string _fileName;
        public string fileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
                OnPropertyChanged(nameof(fileName));
            }
        }

        private bool _isSelected;
        public bool isSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(isSelected));
            }
        }
        public string fullName;
        public TypesFile typeFile;
        public object tag;

        public ObservableCollection<MyFilesStruct> myFilesStructs { get; set; }

    }


}
