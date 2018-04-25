using System.Linq;
using WebApplication1.Commands.GenericDatabaseCommands.DbReceiver;
using WebApplication1.ViewModel.EventViewModelHelpers;

namespace WebApplication1.Commands.GenericDatabaseCommands
{
    public class GenericDbGetByIdCommand<TObject> :  AbstractGenericDbCommand<TObject>
    {
       

        public GenericDbGetByIdCommand(IDbCommandReceiver<TObject> receiver):base(receiver)
        {
            
        }

        public override void Execute()
        {  
            Receiver.Operation(Command.GetById, Entity);

        }

    
        public override TObject ById(TObject anObject)
        {   SetEntity(anObject);
            Execute();
            return Receiver.LastResult().Single();
        }

      
    }
}