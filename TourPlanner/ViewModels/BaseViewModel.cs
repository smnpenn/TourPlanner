using System.ComponentModel;

namespace TourPlanner.UI.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        // Behavior notifications that UI should update
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public BaseViewModel()
        {

        }
    }
}
