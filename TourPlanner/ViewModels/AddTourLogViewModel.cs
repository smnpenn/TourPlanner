﻿using System.Windows;
using System.Windows.Input;
using TourPlanner.BL;
using TourPlanner.Model;

namespace TourPlanner.UI.ViewModels
{
    class AddTourLogViewModel : BaseViewModel
    {

        public string Name { get; set; } = "Log";
        public string Date { get; set; }

        public string Comment { get; set; }

        public double Rating { get; set; }

        public int Time { get; set; }

        public double Difficulty { get; set; }

        public ICommand AddTourLogCommand { get; }
        public ICommand CloseWindowCommand { get; }
        // Window win = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);

        private Tour relatedTour;

        private ITourPlannerManager bl;
        private TourLogsSideListBarViewModel vm;
        public AddTourLogViewModel(ITourPlannerManager bl, TourLogsSideListBarViewModel vm)
        {
            AddTourLogCommand = new RelayCommand(_ => AddNewTourLog());
            // CloseWindowCommand = new RelayCommand(_ => CloseWindow(win)); --> closes windows below somehow
            this.bl = bl;
            this.vm = vm;
            relatedTour = vm.SelectedTour;
        }


        public void AddNewTourLog()
        {

            MessageBox.Show($"Date: {Date}, Comment: {Comment}, Rating: {Rating}, Time: {Time}, Difficulty: {Difficulty}");
            TourLog log = new TourLog(Name, relatedTour, System.DateTime.Now, Comment, Difficulty, Time, Rating);
            bl.AddTourLog(log);
            vm.Items.Add(log);
            // TO-DO: Add TourLog to Tour -> We need to pass the current tour as parameter to the addNewTourLog to add the TourLog to the respective Tour

        }

        // Experimental, dont know how to pass window yet. 
        public void CloseWindow(Window window)
        {
            window.Close();
        }

    }
}
