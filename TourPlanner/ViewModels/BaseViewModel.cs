using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.UI.Service;

namespace TourPlanner.UI.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        // Behavior notifications that UI should update
        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly DialogService _dialogService;

        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public BaseViewModel()
        {
            _dialogService = new DialogService();
        }
    }
}
