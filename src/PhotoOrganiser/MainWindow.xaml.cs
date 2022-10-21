using Microsoft.WindowsAPICodePack.Dialogs;
using PhotoOrganiser.Models;
using PhotoOrganiser.Services;
using System.Windows;

namespace PhotoOrganiser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowsMvvm _model;

        public MainWindow()
        {
            InitializeComponent();
            _model = new MainWindowsMvvm();
            _model.ChoiceFolder = new RelayCommand((a) =>
            {
                var newFolder = GetFolder();
                if (string.IsNullOrWhiteSpace(newFolder) is false)
                {
                    _model.Folder = GetFolder();
                }
            });

            _model.FormOK = new RelayCommand((a) =>
            {
                FileMover mover = new();
                mover.MoveFiles(_model.Folder, _model.SelectedItem, false);
            });

            _model.Folder = @"C:\_ByPS\Fotky";
            _model.SelectedItem = _model.Items[0];

            DataContext = _model;
        }

        public MainWindowsMvvm Model => _model;

        private string GetFolder()
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = _model.Folder;
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                return dialog.FileName;
            }

            return null;
        }
    }
}
