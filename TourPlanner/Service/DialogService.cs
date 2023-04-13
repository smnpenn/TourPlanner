using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.UI.ViewModels;

namespace TourPlanner.UI.Service
{
    public class DialogService
    {

        public void ShowDialog<T>(object dataContext, Action <T> onDialogClose = null) where T: Window, new()
        {
            var view = new T();
            view.DataContext = dataContext;
            view.Owner = Application.Current.MainWindow;
            view.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            if(onDialogClose != null)
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
    }
}
