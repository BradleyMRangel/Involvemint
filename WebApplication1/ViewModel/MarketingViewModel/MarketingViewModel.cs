using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Security;
using WebApplication1.Commands.GenericDatabaseCommands;
using WebApplication1.Model;
using WebApplication1.Repository;

namespace WebApplication1.ViewModel.MarketingViewModel
{
    public class MarketingViewModel : ViewModelBase
    {

        public MarketingViewModel()
        {
        }

        public MarketingViewModel(string id)
        {
            arole = false;
            EventId = id;
            var searchEvent = new Event { id = id };
            TheEvent = DbCommands.GetByIdDbCommand.ById(searchEvent);
            SearchEntity = new NullMarketingmaterial();
            EventCommand = "List";
            Entity = new MarketingMaterial();
        }

        public MarketingViewModel(string id, bool admin)

        {
            arole = admin;
            EventId = id;
            var searchEvent = new Event {id = id};
            TheEvent = DbCommands.GetByIdDbCommand.ById(searchEvent);
            SearchEntity = new NullMarketingmaterial();
            EventCommand = "List";
            Entity = new MarketingMaterial();
        }
        public bool arole { get; set; }

        public HttpPostedFileBase Photo { get; set; }
        public string EventId { get; set; }
        public Event TheEvent { get; set; }
        public List<MarketingMaterial> MarketingList { get; set; }
        public MarketingMaterial SearchEntity { get; set; }
        public MarketingMaterial Entity { get; set; }

        public override void HandleRequest()
        {
            var searchEvent = new Event {id = EventId};
            TheEvent = DbCommands.GetByIdDbCommand.ById(searchEvent);
            switch (EventCommand.ToLower())
            {
                case "return":
                    SaveProductAndReturn();
                    break;
            }
            base.HandleRequest();
        }

        public void SaveProductAndReturn()
        {

            DbCommands.UpdateDbCommand.SetEntity(TheEvent);
            DbCommands.UpdateDbCommand.Execute();
            MarketingList = TheEvent.Materials;
            ListMode();
        }

        protected override void Add()
        {
            IsValid = true;
            Entity = new MarketingMaterial();
            base.Add();
        }

        protected override void Edit()
        {
            Entity = new MarketingMaterial {id = EventArgument};
            Entity = TheEvent.Materials.Find(l => l.id == EventArgument);
            base.Edit();
        }

        protected override void Save()
        {
            if (Mode == "Add")
            {
               TheEvent.Materials.Add(Entity); 
              
               
                
            }
            else
            {
              var  holdEntity = TheEvent.Materials.Find(l => l.id == EventArgument);
                TheEvent.Materials.Remove(holdEntity);
                TheEvent.Materials.Add(Entity); // update product     
            }
            DbCommands.UpdateDbCommand.SetEntity(TheEvent); // save updated product
            
            ListMode();
          
            var searchEvent = new Event { id = EventId };
            TheEvent = DbCommands.GetByIdDbCommand.ById(searchEvent);
            MarketingList = TheEvent.Materials;
            Get();
            base.Save();
        }

        protected  override void Delete()
        {
            // set product to delete Product 
            Entity = TheEvent.Materials.Find(l => l.id == EventArgument);

            var adapter = new BlogStorageAdapter();

             adapter.Delete(Entity.Resource);
        
           
            TheEvent.Materials.Remove(Entity);
            DbCommands.UpdateDbCommand.SetEntity(TheEvent); // save updated product
            Get();
            base.Delete();
        }

        protected override void ResetSearch()
        {
            SearchEntity = new NullMarketingmaterial();
            base.ResetSearch();
        }

        protected override void Get()
        {
            if (TheEvent != null)
            {
                var materials = TheEvent.Materials;

                if (!string.IsNullOrEmpty(SearchEntity?.Description))
                {
                    materials =
                        materials.FindAll(
                            p =>
                                p.Description.ToLower()
                                    .StartsWith(SearchEntity.Description, StringComparison.CurrentCultureIgnoreCase));
                }

                MarketingList = (from mkt in materials
                    orderby mkt.Description ascending
                    select (mkt)).ToList();
            }
            base.Get();
        }
    }
}