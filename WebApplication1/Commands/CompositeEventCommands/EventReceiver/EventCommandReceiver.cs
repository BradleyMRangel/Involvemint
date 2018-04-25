using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using WebApplication1.Commands.GenericDatabaseCommands;
using WebApplication1.Model;
using WebApplication1.ViewModel.EventViewModelHelpers;

namespace WebApplication1.Commands.CompositeEventCommands.EventReceiver
{
    public class EventCommandReceiver : IEventCommandReceiver
    {
        public List<Event> Results { get; set; }

        public List<Event> LastResult()
        {
            return Results;
        }

        public void Operation(Command command, string parameter, Event queryEvent)
        {
            switch (command)
            {
                case Command.InitializeEventDb:
                    DbCommands.ClearDbCommand.Execute();
                    var events = ReadCsvEvents.ReadCsv();

                    foreach (var aEvent in events)
                    {
                        DbCommands.AddDbCommand.SetEntity(aEvent);
                    }
                    break;

                case Command.SortByName:
                    var events1 = DbCommands.GetListDbCommand.GetList(new NullEvent());
                    events1 = FilterEventsByName(events1, queryEvent);
                    var sortedEvents = (from anevent in events1
                        orderby anevent.EventName ascending
                        select (anevent)).ToList();
                    Results = sortedEvents;
                    break;
                case Command.Autocomplete:
                    var events2 = DbCommands.GetListDbCommand.GetList(new NullEvent());
                    var theEvents2 = (from anevent in events2
                        where anevent.EventName.StartsWith(queryEvent.EventName, true, CultureInfo.CurrentCulture)
                        orderby anevent.EventName ascending
                        select (anevent)).ToList();
                    Results = theEvents2;
                    break;
                case Command.SortByType:
                    var events3 = DbCommands.GetListDbCommand.GetList(new NullEvent());
                    events3 = FilterEventsByType(events3, queryEvent);
                    var sortedEventType = (from anevent in events3
                                           orderby anevent.EventType ascending
                                           select (anevent)).ToList();
                    Results = sortedEventType;
                    break;
                case Command.ShowCurrentEvents:
                    var events4 = DbCommands.GetListDbCommand.GetList(new NullEvent());
                    var FilterEventsByCurrentDate = events4.Where(myevent =>
                        myevent.EventDate > DateTime.Today
                        ).OrderByDescending(myevent => myevent.EventName).ToList();
                    Results = FilterEventsByCurrentDate;
                    break;
                case Command.MaxAttendance:
                    var events5 = DbCommands.GetListDbCommand.GetList(new NullEvent());
                    var theEvents5 = (from anevent in events5
                                      where anevent.EventAttendance >= queryEvent.EventAttendance
                                      orderby anevent.EventName ascending
                                      select (anevent)).ToList();
                    Results = theEvents5;
                    break;
            }
        }

        public List<Event> FilterEventsByName(List<Event> events, Event searchEvent)
        {
            var ret = events;
            if (!string.IsNullOrEmpty(searchEvent.EventName))
            {
                ret =
                    events.FindAll(
                        myevent => myevent.EventName.ToLower().StartsWith(searchEvent.EventName, StringComparison.CurrentCultureIgnoreCase));
            }
            return ret;
        }

        public List<Event> FilterEventsByType(List<Event> events, Event searchEvent)
        {
            var ret = events;
            if (!string.IsNullOrEmpty(searchEvent.EventType))
            {
                ret =
                    events.FindAll(
                        myevent => myevent.EventType.ToLower().StartsWith(searchEvent.EventType, StringComparison.CurrentCultureIgnoreCase));
            }
            return ret;
        }
    }
}