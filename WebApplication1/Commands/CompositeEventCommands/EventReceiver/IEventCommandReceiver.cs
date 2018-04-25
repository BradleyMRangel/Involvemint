using System.Collections.Generic;
using WebApplication1.Model;
using WebApplication1.ViewModel.EventViewModelHelpers;

namespace WebApplication1.Commands.CompositeEventCommands.EventReceiver
{
    public interface IEventCommandReceiver
    {
        void Operation(Command command, string id, Event queryEvent);
        List<Event> LastResult();
    }
}
