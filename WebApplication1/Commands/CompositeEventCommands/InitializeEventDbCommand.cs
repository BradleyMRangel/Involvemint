using WebApplication1.Commands.CompositeEventCommands.EventReceiver;
using WebApplication1.ViewModel.EventViewModelHelpers;

namespace WebApplication1.Commands.CompositeEventCommands
{
    public class InitializeEventDbCommand : AbstractEventsCommand
    {
        public InitializeEventDbCommand(IEventCommandReceiver receiver1)
        {
            this.Receiver = receiver1;
            TheCommand  = Command.InitializeEventDb;
        }

     
      
    }
}
    