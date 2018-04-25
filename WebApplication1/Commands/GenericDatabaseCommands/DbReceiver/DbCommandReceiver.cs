using System.Collections.Generic;
using System.Linq;
using WebApplication1.Repository;
using WebApplication1.ViewModel.EventViewModelHelpers;

namespace WebApplication1.Commands.GenericDatabaseCommands.DbReceiver
{
   

    public interface IDbCommandReceiver<TObject>
    {
        void Operation(Command command, TObject anObject);
        List<TObject> LastResult();
    }


    public class DbCommandReceiver<TObject> : IDbCommandReceiver<TObject>
    {
        private readonly IRepository<string, TObject> _repository;

        public DbCommandReceiver(IRepository<string, TObject> repository)
        {
            _repository = repository;
        }

        public List<TObject> Results { get; set; }

        public void Operation(Command command, TObject theObject)
        {
            var anObject = (IStorable) theObject;

            switch (command)
            {
                case Command.GetById:
                    Results = _repository.Get(anObject.id).DefaultIfEmpty().ToList();
                    break;
                case Command.GetList:
                    Results = _repository.ToList();
                    break;
                case Command.Insert:
                    if (anObject != null)
                    {
                        _repository.Add(anObject.id, theObject);
                    }
                    break;
                case Command.Update:
                    if (anObject != null)
                    {
                        _repository.Update(anObject.id, theObject);
                    }
                    break;
                case Command.Delete:
                    if (anObject != null) _repository.Delete(anObject.id);
                    break;
                case Command.Clear:
                    _repository.Clear().Wait();
                    break;
            }
        }

        public List<TObject> LastResult()
        {
            return Results;
        }
    }
}