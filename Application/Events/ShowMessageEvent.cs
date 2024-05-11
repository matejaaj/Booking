using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Events
{
    public class ShowMessageEvent
    {
        public string Message { get; }
        public string Title { get; }
        public ShowMessageEvent(string message, string tile)
        {
            Message = message;
            Title = tile;
        }
    }
}
