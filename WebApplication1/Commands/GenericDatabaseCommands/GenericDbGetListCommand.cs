using System.Collections.Generic;
using WebApplication1.Commands.GenericDatabaseCommands.DbReceiver;
using WebApplication1.ViewModel.EventViewModelHelpers;

namespace WebApplication1.Commands.GenericDatabaseCommands
{
    public class GenericDbGetListCommand<TObject> :  AbstractGenericDbCommand<TObject>
    {
       
        public GenericDbGetListCommand(IDbCommandReceiver<TObject> receiver):base(receiver)
        {
           
        }

        public override void Execute()
        {
            Receiver.Operation(Command.GetList, Entity);
        }

        public override List<TObject> GetList(TObject anObject)
        {
            SetEntity(anObject);
            Execute();
            return Receiver.LastResult();
        }


    }
}