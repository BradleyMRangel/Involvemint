using System;
using System.Collections.Generic;
using System.IO;
using WebApplication1.Model;
using WebApplication1.Properties;

namespace WebApplication1.Commands.CompositeEventCommands.EventReceiver
{
    public class ReadCsvEvents    {
       public static List<Event> ReadCsv()
        {
            var eventList = new List<Event>();

            var reader = new StreamReader(File.OpenRead(Settings.Default.csvFile));

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line != null)
                {
                    var values = line.Split(',');
                    var anevent = new Event
                    {
                        id = values[0],
                        EventName = values[1],
                        EventDate = Convert.ToDateTime(values[2]),
                        EventType = Convert.ToString(values[3]),
                        EventDescription = Convert.ToString(values[4]),
                        EventAttendance = int.Parse(values[5]),
                        EventLocation = Convert.ToString(values[6])
                    };
                    eventList.Add(anevent);
                }
            }
            reader.Close();
            return eventList;
        }
    }
}