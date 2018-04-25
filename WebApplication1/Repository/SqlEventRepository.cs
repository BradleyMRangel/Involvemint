using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Model;

namespace WebApplication1.Repository
{
    public class EventContext : DbContext
    {
        public DbSet<MarketingMaterial> Material { get; set; }
        public DbSet<Event> Events { get; set; }
    }

    public class SqlEventRepository : IRepository<string, Event>
    {
        public void Add(string id, Event anObject)
        {
            using (var context = new EventContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Events.Add(anObject);
                context.SaveChanges();
            }
        }

        public Task Clear()
        {
            using (var context = new EventContext())
            {
                var linqQuery = context.Events.Include(n => n.Materials);
                foreach (var item in linqQuery)
                {
                    Delete(item.id);
                }
            }
            var task = Task<string>.Factory.StartNew(() =>
            {
                var s = ".NET";
                return s;
            });
            return task;
        }


        public void ClearAll()
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            using (var context = new EventContext())
            {
                var result = context.Events.AsNoTracking().Include(n => n.Materials)
                    .FirstOrDefault(n => n.id == id);
                if (result != null)
                {
                    context.Entry(result).State = EntityState.Deleted;
                    context.SaveChanges();
                }
            }
        }


        public Maybe<Event> Get(string id)
        {
            using (var context = new EventContext())
            {
                var result = context.Events.AsNoTracking().Include(n => n.Materials)
                    .FirstOrDefault(n => n.id == id);

                if (result == null) return new Maybe<Event>();

                return new Maybe<Event>(result);
            }
        }


        public void Update(string id, Event anObject)
        {
            using (var context = new EventContext())
            {
                var newList = new List<MarketingMaterial>();
                var ninja = context.Events.Include(n => n.Materials)
                    .FirstOrDefault(n => n.id == id);

                if (anObject.Materials.Count < ninja.Materials.Count)
                {
                    // deleted marketing material

                    var differenceQuery =
                        ninja.Materials.Except(anObject.Materials);
                    context.Material.Remove(differenceQuery.FirstOrDefault());
                    context.SaveChanges();
                }
                else
                {
                    // added marketing material
                    var differenceQuery =
                                          ninja.Materials.Except(anObject.Materials);

                    var marketingMaterials = differenceQuery as MarketingMaterial[] ?? differenceQuery.ToArray();
                    if (marketingMaterials.Count() != 0)
                    {
                        context.Material.Remove(marketingMaterials.FirstOrDefault());
                    }
                    var differenceQuery1 =
                                        anObject.Materials.Except(ninja.Materials);

                    foreach (var material in differenceQuery1)
                    {
                        ninja.Materials.Add(MarketingMaterial.Copy(material));
                    }
                 //   ninja.Materials = newList;
                    ninja.EventLocation = anObject.EventLocation;
                    ninja.EventType = anObject.EventType;
                    ninja.EventDate = anObject.EventDate;
                    ninja.EventName = anObject.EventName;
                    ninja.EventAttendance = anObject.EventAttendance;
                    context.Entry(ninja).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
        }


        public IEnumerator<Event> GetEnumerator()
        {
            using (var context = new EventContext())
            {
                context.Database.Log = message => Debug.WriteLine(message);
                var linqQuery = context.Events.Include(n => n.Materials);
                linqQuery = linqQuery.OrderBy(n => n.EventName);
                return linqQuery.ToList().GetEnumerator();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}