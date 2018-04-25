using System.Reflection;
using Ninject;
using WebApplication1.Repository;

namespace WebApplication1.Commands.GenericDatabaseCommands.DbReceiver
{
    // singleton pattern assure there is only one receiver to interact with the database
    public class SingletonDbReceiver<TObject>
    {
        private static DbCommandReceiver<TObject> _instance;

        private SingletonDbReceiver() { }

        public static DbCommandReceiver<TObject> Receiver
        {
            get
            {
                if (_instance == null)

                {  // uses NLog for dependency Injection
                    var kernel = new StandardKernel();
                    kernel.Load(Assembly.GetExecutingAssembly());
                    var repository = kernel.Get<IRepository<string, TObject>>();

                    _instance = new DbCommandReceiver<TObject>(repository);

                }
                return _instance;
            }
        }
    }
}