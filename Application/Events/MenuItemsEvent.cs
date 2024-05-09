using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.Application.Events
{
    public class MenuItemsEvent
    {
        public Visibility Visibility { get; set; }
        public MenuItemsEvent(Visibility visibility)
        {
            this.Visibility = visibility;
        }
    }
}
