using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace PhotoOrganiser.Models
{
    public class MainWindowsMvvm : INotifyPropertyChanged
    {
        private string _selectedItem;

        private string _folder;

        private ObservableCollection<string> _items = new ObservableCollection<string>()
        {
            "*.*",
            "*.jpg",
            "*.png",
            "*.mov",
            "*.mp4",
        };

        public event PropertyChangedEventHandler PropertyChanged;

        public string Folder
        {
            get { return _folder; }
            set
            {
                _folder = value;
                OnPropertyChanged(nameof(Folder));
            }
        }

        public ICommand FormOK { get; set; }

        public ICommand FormCancel { get; set; }

        public ICommand ChoiceFolder { get; set; }

        public ObservableCollection<string> Items
        {
            get { return _items; }
        }

        public string SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public string NewItem
        {
            get
            {
                return string.Empty;
            }
            set
            {
                if (SelectedItem != null)
                {
                    return;
                }
                if (!string.IsNullOrEmpty(value))
                {
                    _items.Add(value);
                    SelectedItem = value;
                }
            }
        }

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
