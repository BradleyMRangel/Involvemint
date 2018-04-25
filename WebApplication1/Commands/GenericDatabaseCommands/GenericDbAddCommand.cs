using WebApplication1.Commands.GenericDatabaseCommands.DbReceiver;
using WebApplication1.ViewModel.EventViewModelHelpers;

namespace WebApplication1.Commands.GenericDatabaseCommands
{
    public class GenericDbAddCommand<TObject> : AbstractGenericDbCommand<TObject>
    {
        
        public GenericDbAddCommand(IDbCommandReceiver<TObject> receiver):base(receiver)
        {
           
        }

        public override void Execute()
        {
          Receiver.Operation(Command.Insert, Entity);
        }

        
    }
}