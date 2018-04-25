using System.Collections.Generic;

namespace WebApplication1.Commands.GenericDatabaseCommands
{
    public interface IDbCommand<TObject>
    {
        void Execute();
        List<TObject> GetList(TObject anObject);
        TObject ById(TObject anObject);
        void SetEntity(TObject anObject);
    }
   
}