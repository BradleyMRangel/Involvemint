using WebApplication1.Commands.GenericDatabaseCommands.DbReceiver;
using WebApplication1.ViewModel.EventViewModelHelpers;

namespace WebApplication1.Commands.GenericDatabaseCommands
{
    public class GenericDbClearCommand<TObject> : AbstractGenericDbCommand<TObject>
    { 
        public GenericDbClearCommand(IDbCommandReceiver<TObject> receiver):base(receiver)
        {
            
        }

        public override void Execute()
        {
            Receiver.Operation(Command.Clear, Entity);
        }

       
    }
}