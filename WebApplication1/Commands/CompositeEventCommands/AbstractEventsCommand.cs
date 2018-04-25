using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Commands.CompositeEventCommands.EventReceiver;
using WebApplication1.Model;
using WebApplication1.ViewModel.EventViewModelHelpers;
using WebApplication1.Commands.CompositeEventCommands;

namespace WebApplication1.Commands.CompositeEventCommands
{
    public class AbstractEventsCommand
    {

        protected IEventCommandReceiver Receiver;

        protected Command TheCommand;

        public void Execute(string parameter, Event aEvent)
        {
            Receiver.Operation(TheCommand, parameter, aEvent);
        }

        public List<Event> GetSortedList()
        {
            return Receiver.LastResult();
        }
    }
}