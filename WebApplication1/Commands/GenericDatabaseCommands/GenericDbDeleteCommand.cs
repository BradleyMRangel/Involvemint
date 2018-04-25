using WebApplication1.Commands.GenericDatabaseCommands.DbReceiver;
using WebApplication1.ViewModel.EventViewModelHelpers;

namespace WebApplication1.Commands.GenericDatabaseCommands
{
    public class GenericDbDeleteCommand<TObject> :  AbstractGenericDbCommand<TObject>
    {
        

        public GenericDbDeleteCommand(IDbCommandReceiver<TObject> receiver):base(receiver)
        {
            
        }

        public override void  Execute()
        {
            Receiver.Operation(Command.Delete, Entity);
        }

        
    }
}