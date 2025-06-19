using System.ComponentModel;
using System.Diagnostics;

namespace WpfExplorer
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        // public const int MaxGrafikPoints = 5000;        

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            VerifyPropertyName(propertyName);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [Conditional("DEBUG")]
        private void VerifyPropertyName(string propertyName)
        {
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
                Debug.WriteLine("Так не должно быть! BaseViewModel " + GetType().Name + " does not contain property: " + propertyName);
            //throw new System.ArgumentNullException(GetType().Name + " does not contain property: " + propertyName);
        }
    }


}
