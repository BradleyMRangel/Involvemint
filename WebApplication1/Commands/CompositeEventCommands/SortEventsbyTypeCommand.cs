using System.Collections.Generic;
using WebApplication1.Commands.CompositeEventCommands.EventReceiver;
using WebApplication1.Model;
using WebApplication1.ViewModel.EventViewModelHelpers;

namespace WebApplication1.Commands.CompositeEventCommands
{
    public class SortEventsbyTypeCommand : AbstractEventsCommand
    {
        public SortEventsbyTypeCommand(IEventCommandReceiver receiver1)
        {
            Receiver = receiver1;
            TheCommand = Command.SortByType;
        }
    }
}