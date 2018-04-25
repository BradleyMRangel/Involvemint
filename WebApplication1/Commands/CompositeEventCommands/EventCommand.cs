using WebApplication1.Commands.CompositeEventCommands;
using WebApplication1.Commands.CompositeEventCommands.EventReceiver;

namespace WebApplication1.Commands.CompositeEventCommands
{
    public class EventCommand
    {
        public static SortEventsbyNameCommand SortEventsbyNameCommand;
        public static SortEventsbyTypeCommand SortEventsbyTypeCommand;

        public static InitializeEventDbCommand InitializeEventDbCommand;

        // query commands
        public static EventAutoCompleteCommand AutoCompleteCommand;

        public static EventMaxAttendanceCommand MaxAttendanceCommand;
        public static ShowCurrentEventsCommand ShowCurrrentEventsCommand;

        //public static ProducMaxPriceCommand MaxPriceCommand;


        // product specific commands
        static EventCommand()
        {
            IEventCommandReceiver eventCommandReceiver = new EventCommandReceiver(); // create a receiver
            SortEventsbyNameCommand = new SortEventsbyNameCommand(eventCommandReceiver);
            SortEventsbyTypeCommand = new SortEventsbyTypeCommand(eventCommandReceiver);
            InitializeEventDbCommand = new InitializeEventDbCommand(eventCommandReceiver);
            AutoCompleteCommand = new EventAutoCompleteCommand(eventCommandReceiver);
            MaxAttendanceCommand = new EventMaxAttendanceCommand(eventCommandReceiver);
            ShowCurrrentEventsCommand = new ShowCurrentEventsCommand(eventCommandReceiver);
        }
    }
}