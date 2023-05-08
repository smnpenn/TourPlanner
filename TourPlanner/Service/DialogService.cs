using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using TourPlanner.UI.ViewModels;

namespace TourPlanner.UI.Service
{
    public class DialogService
    {

        private static DialogService instance = null;

        public static DialogService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DialogService();
                }
                return instance;
            }
        }

        public void ShowDialog<T>(object dataContext, Action<T> onDialogClose = null) where T : Window, new()
        {
            var view = new T();
            view.DataContext = dataContext;
            view.Owner = Application.Current.MainWindow;
            view.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            if (onDialogClose != null)
            {
                view.Closed += (sender, args) => onDialogClose(view as T);
            }

            view.ShowDialog();
        }
        public void CloseDialog(BaseViewModel viewModel)
        {
            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.DataContext == viewModel);
            window?.Close();
        }

        public string? ShowSaveFileDialog(string defaultName, string ext)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = defaultName; // Default file name
            dlg.DefaultExt = $".{ext}"; // Default file extension
            dlg.Filter = $"{ext} documents |*.{ext}"; // Filter files by extension

            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                return dlg.FileName;
            }
            else
            {
                return null;
            }
        }

        public Stream? ShowOpenFileDialog(string ext)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = $"{ext} documents |*.{ext}";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                return dlg.OpenFile();
            }
            else
            {
                return null;
            }
        }

        public void OpenFileExplorer(string filename)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo("explorer");
                startInfo.Arguments = filename;
                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Could not open given file", "Error");
            }

        }
    }
}
