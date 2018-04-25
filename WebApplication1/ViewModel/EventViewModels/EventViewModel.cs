using System;
using System.Collections.Generic;
using WebApplication1.Commands;
using WebApplication1.Commands.CompositeEventCommands.EventReceiver;
using WebApplication1.Commands.GenericDatabaseCommands;
using WebApplication1.Model;
using WebApplication1.Properties;
using WebApplication1.Repository;
using WebApplication1.ViewModel.EventViewModelHelpers;

namespace WebApplication1.ViewModel.EventViewModels
{
    public class EventViewModel : ViewModelBase
    {


        // added *********************************** new property to set role
        public bool arole { get; set; }

        //*********************************************
        public List<Event> Events { get; set; }
        public Event SearchEntity { get; set; }
        public Event Entity { get; set; }

        public bool sortbyType = false;
        private static bool _initialized;
        public EventViewModel()
        {
            arole = false;
            SearchEntity = new NullEvent();
            EventCommand = "List";
            Entity = new Event();

            if (Settings.Default.initdatabase.Equals("true") && _initialized == false)
            {
                //   database is initialized 
                Commands.CompositeEventCommands.EventCommand.InitializeEventDbCommand.Execute(null, null);
                _initialized = true;
            }
        }

        public EventViewModel(bool admin)
        {
            arole = true;   //************** a true values represents the admin role
            SearchEntity = new NullEvent();
            EventCommand = "List";
            Entity = new Event();

            if (Settings.Default.initdatabase.Equals("true") && _initialized == false)
            {
                //   database is initialized 
                Commands.CompositeEventCommands.EventCommand.InitializeEventDbCommand.Execute(null, null);
                _initialized = true;
            }
        }
        public override void HandleRequest()
        {
            switch (EventCommand.ToLower())
            {
                case "addyourown":
                    break;
            }
            base.HandleRequest();
        }
        protected override void Add()
        {
            IsValid = true;
            // Initialize and set new Entity Object to add to the database
            Entity = new Event();
            base.Add();
        }
        protected override void Edit()
        {
            Entity = new Event {id = EventArgument};
            Entity = DbCommands.GetByIdDbCommand.ById(Entity);// find a product to edit

            base.Edit();
        }

        protected override void View()
        {
            Entity = new Event { id = EventArgument };
            Entity = DbCommands.GetByIdDbCommand.ById(Entity);// find a product to edit

            base.View();
        }

        protected override void Save()
        {
            if (IsValid) // added
            {
                if (Mode == "Add")
                {
                    DbCommands.AddDbCommand.SetEntity(Entity); // add to database
                }
                else
                {
                    Entity.id = EventArgument; // set product id
                    DbCommands.UpdateDbCommand.SetEntity(Entity); // update databasE      
                }
                     Get();
            }
            else
            {
                if(Mode == "Add" || Mode == "Edit")
                {
                    AddMode();
                }
            }

           
            base.Save();
        }
        protected override void Delete()
        {
            // set product to delete Product 
            Entity = new Event {id = EventArgument};
          
            DbCommands.DeleteDbCommand.SetEntity(Entity);
            Get();

            base.Delete();
        }
        protected override void ResetSearch()
        {
            SearchEntity = new NullEvent();
            base.ResetSearch();
        }
        protected override void Get()
        {
            //Commands.CompositeEventCommands.EventCommand.SortEventsbyNameCommand.Execute(string.Empty, SearchEntity);
            //Events = Commands.CompositeEventCommands.EventCommand.SortEventsbyNameCommand.GetSortedList();

            Commands.CompositeEventCommands.EventCommand.SortEventsbyTypeCommand.Execute(string.Empty, SearchEntity);
            Events = Commands.CompositeEventCommands.EventCommand.SortEventsbyTypeCommand.GetSortedList();
            base.Get();
        }



    }
}