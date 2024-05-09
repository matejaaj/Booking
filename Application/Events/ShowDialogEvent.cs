using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Events
{
    class ShowDialogEvent
    {
        public string Message { get; }
        public string Title { get; }
        public ShowDialogEvent(string message, string tittle)
        {
            Message = message;
            Title = tittle;
        }
    }
}
