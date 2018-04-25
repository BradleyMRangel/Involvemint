using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Commands.CompositeEventCommands;
using WebApplication1.Commands.CompositeEventCommands.EventReceiver;
using WebApplication1.Model;
using WebApplication1.ViewModel.EventViewModelHelpers;

namespace WebApplication1.Commands.CompositeEventCommands
{
    public class EventAutoCompleteCommand:AbstractEventsCommand
    {
          
            public EventAutoCompleteCommand(IEventCommandReceiver receiver1)
            {
                Receiver = receiver1;
               TheCommand = Command.Autocomplete;
            }
          
        }
}