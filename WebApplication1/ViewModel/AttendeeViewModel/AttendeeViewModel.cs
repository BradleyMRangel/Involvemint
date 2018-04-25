using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Commands.GenericDatabaseCommands;
using WebApplication1.Model;
using WebApplication1.Repository;

namespace WebApplication1.ViewModel.AttendeeViewModel
{
    public class AttendeeViewModel : ViewModelBase
    {

        public AttendeeViewModel()
        {
          //  arole = vm.arole;
            //AttendeeId = id;
           // var searchinEvent = new Event { id = id };
           // AnEvent = DbCommands.GetByIdDbCommand.ById(searchinEvent);
         
        }
        public AttendeeViewModel(string id)
        {
            arole = false;
            AttendeeId = id;
            var searchinEvent = new Event { id = id };
            AnEvent = DbCommands.GetByIdDbCommand.ById(searchinEvent);
            SearchEntity = new NullAttendee();
            EventCommand = "List";
            Entity = new Attendee();
        }
        public AttendeeViewModel(string id, bool admin)
        {
            arole = admin;
            AttendeeId = id;
            var searchinEvent = new Event { id = id };
            AnEvent = DbCommands.GetByIdDbCommand.ById(searchinEvent);
            SearchEntity = new NullAttendee();
            EventCommand = "List";
            Entity = new Attendee();
        }
        public bool arole { get; set; }
        //public HttpPostedFileBase Photo { get; set; }
        
        public List<Attendee> AttendeeList { get; set; }
        public Attendee SearchEntity { get; set; }
        public Attendee Entity { get; set; }
        public string AttendeeId { get; set; }
        public Event AnEvent { get; set; }

        public override void HandleRequest()
        {
            var searchinEvent = new Event { id = AttendeeId };
            AnEvent = DbCommands.GetByIdDbCommand.ById(searchinEvent);
            switch (EventCommand.ToLower())
            {
                case "return":
                    SaveAttendeeAndReturn();
                    break;
            }
            base.HandleRequest();
        }

        public void SaveAttendeeAndReturn()
        {

            DbCommands.UpdateDbCommand.SetEntity(AnEvent);
            DbCommands.UpdateDbCommand.Execute();
            AttendeeList = AnEvent.Attendees;
            ListMode();
        }
        protected override void Add()
        {
            IsValid = true;
            // Initialize and set new Entity Object to add to the database
            Entity = new Attendee();
            base.Add();
        }
        protected override void Edit()
        {
            Entity = new Attendee{ id = EventArgument };
            Entity = AnEvent.Attendees.Find(l => l.id == EventArgument);
            base.Edit();    // find a event to edit
        }

        protected override void Save()
        {
            if (Mode == "Add")
            {
                AnEvent.Attendees.Add(Entity);
            }
            else
            {
                var anEntity = AnEvent.Attendees.Find(l => l.id == EventArgument);
                AnEvent.Attendees.Remove(anEntity);
                AnEvent.Attendees.Add(Entity); // update event with attendees 
            }
            DbCommands.UpdateDbCommand.SetEntity(AnEvent); // save updated Attendees in event
            ListMode();

            var searchinEvent = new Event { id = AttendeeId };
            AnEvent = DbCommands.GetByIdDbCommand.ById(searchinEvent);
            AttendeeList = AnEvent.Attendees;
            Get();
            base.Save();
        }

        protected override void Delete()
        {
            Entity = AnEvent.Attendees.Find(l => l.id == EventArgument);
            var adapter = new BlogStorageAdapter();

            adapter.Delete(Entity.AttendeeResource);


            AnEvent.Attendees.Remove(Entity);
            DbCommands.UpdateDbCommand.SetEntity(AnEvent); // save updated product
            Get();
            base.Delete();
        }

        protected override void ResetSearch()
        {
            SearchEntity = new NullAttendee();
            base.ResetSearch();
        }

        protected override void Get()
        {
            if (AnEvent != null)
            {
                var attendees = AnEvent.Attendees;

                if (!string.IsNullOrEmpty(SearchEntity?.lastName))
                {
                    attendees =
                        attendees.FindAll(
                            p =>
                                p.lastName.ToLower()
                                    .StartsWith(SearchEntity.lastName, StringComparison.CurrentCultureIgnoreCase));
                }

                AttendeeList = (from aAttendee in attendees
                    orderby aAttendee.lastName ascending
                    select (aAttendee)).ToList();
            }
            base.Get();
        }
    }
}