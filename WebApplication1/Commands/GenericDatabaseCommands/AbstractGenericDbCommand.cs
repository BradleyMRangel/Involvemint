using System;
using System.Collections.Generic;
using WebApplication1.Commands.GenericDatabaseCommands.DbReceiver;

namespace WebApplication1.Commands.GenericDatabaseCommands
{
    public class AbstractGenericDbCommand<TObject>: IDbCommand<TObject>
    {
        protected IDbCommandReceiver<TObject> Receiver;
        protected TObject Entity;

        public AbstractGenericDbCommand(IDbCommandReceiver<TObject> receiver)
        {
            Receiver = receiver;
        }

        public virtual void Execute()
        {
            throw new NotImplementedException();
        }

        public virtual List<TObject> GetList(TObject anobject)
        {
            throw new NotImplementedException();
        }

        public virtual void SetEntity(TObject anObject)
        {
            Entity = anObject;
            Execute();
           
        }

        public virtual TObject ById(TObject anObject)
        {
            throw new NotImplementedException();
        }
    }
}