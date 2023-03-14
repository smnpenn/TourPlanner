using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.UI.ViewModels
{
    interface ISideListBarViewModel
    {
        public void AddItem();
        public void DeleteItem();
        public void EditItem();
    }
}
