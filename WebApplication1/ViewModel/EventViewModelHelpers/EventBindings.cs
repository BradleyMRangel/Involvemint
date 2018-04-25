using Ninject.Modules;
using WebApplication1.Model;
using WebApplication1.Properties;
using WebApplication1.Repository;

namespace WebApplication1.ViewModel.EventViewModelHelpers
{
    // this class uses the Setting repository to select the repository to use for storage
    // It extends NinjectModule to bind an IRepository to a concrete Repository vis dependency injection


    // ******Important for Azure DocumentDb update setting to use your
    // make sure you set your *********************endpointUrl******************** 
    // and ****************************************authorization key******************
    // and ****************************************databaseId************************
    // and*****************************************collectionId********************** 
    public class EventBindings : NinjectModule
    {
        public override void Load()
        {
            switch (Settings.Default.repository)
            {
                case "Sql":  // see above configuration settings expected 
                    Bind<IRepository<string, Event>>().To<SqlEventRepository>();// 
                    break;
                case "hashtagmcb":  // see above configuration settings expected 
                    Bind<IRepository<string, Event>>().To<AzureNoSqlRepository<string, Event>>();// Azure NoSqlDatabase
                    break;
                case "NDatabase":
                    Bind<IRepository<string, Event>>().To<NDatabaseNoSqlRepository<string, Event>>(); // NDatabase local file system NoSqlDatabase
                    break;
                default:
                    Bind<IRepository<string, Event>>().To<DictionaryRepository<string, Event>>();// use this for testing
                    break;
            }
        }
    }
}