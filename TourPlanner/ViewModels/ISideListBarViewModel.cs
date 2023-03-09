using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.UI.ViewModels
{
    interface ISideListBarViewModel
    {
        public void AddItem(object obj);
        public void RemoveItem(object obj);
        public void EditItem(object obj);
    }
}
