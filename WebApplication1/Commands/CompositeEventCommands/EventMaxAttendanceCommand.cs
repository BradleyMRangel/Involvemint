using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Commands.CompositeEventCommands.EventReceiver;
using WebApplication1.ViewModel.EventViewModelHelpers;

namespace WebApplication1.Commands.CompositeEventCommands
{
    public class EventMaxAttendanceCommand:AbstractEventsCommand  //last minute change to show popular events by entering in least amount of attendance
    {
        public EventMaxAttendanceCommand(IEventCommandReceiver receiver1)
        {
            Receiver = receiver1;
            TheCommand = Command.MaxAttendance;
        }
    }
}