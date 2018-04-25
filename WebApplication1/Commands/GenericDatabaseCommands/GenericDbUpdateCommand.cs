using WebApplication1.Commands.GenericDatabaseCommands.DbReceiver;
using WebApplication1.ViewModel.EventViewModelHelpers;

namespace WebApplication1.Commands.GenericDatabaseCommands
{
    public class GenericDbUpdateCommand<TObject> : AbstractGenericDbCommand<TObject>
    {
       
        public GenericDbUpdateCommand(IDbCommandReceiver<TObject> receiver):base(receiver)
        {
            
        }

        public override void Execute()
        {
            Receiver.Operation(Command.Update, Entity);
        }

        
    }
}