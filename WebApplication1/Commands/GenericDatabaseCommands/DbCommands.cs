using WebApplication1.Commands.GenericDatabaseCommands.DbReceiver;
using WebApplication1.Model;

namespace WebApplication1.Commands.GenericDatabaseCommands
{
    

    // command pattern
    public class DbCommands
    {  // generic commands
        public static IDbCommand<Event> AddDbCommand;
        public static IDbCommand<Event> UpdateDbCommand;
        public static IDbCommand<Event> GetByIdDbCommand;
        public static IDbCommand<Event> GetListDbCommand;
        public static IDbCommand<Event> DeleteDbCommand;
        public static IDbCommand<Event> ClearDbCommand;

        // cat specific composite commands
       

        static DbCommands()  // note static constructor
        {   // inject receiver and singleton 
            IDbCommandReceiver<Event> receiver = SingletonDbReceiver<Event>.Receiver;
            // generic database commands
            AddDbCommand =     new GenericDbAddCommand<Event>(receiver);
            GetByIdDbCommand = new GenericDbGetByIdCommand<Event>(receiver);
            GetListDbCommand = new GenericDbGetListCommand<Event>(receiver);
            UpdateDbCommand =  new GenericDbUpdateCommand<Event>(receiver);
            DeleteDbCommand =  new GenericDbDeleteCommand<Event>(receiver);
            ClearDbCommand =  new GenericDbClearCommand<Event>(receiver);

          
        }
    }
}