using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NDatabase;
using NDatabase.Api;
using WebApplication1.Properties;

namespace WebApplication1.Repository
{
    public class NDatabaseNoSqlRepository<TKey, TObject> : IRepository<TKey, TObject>
    {

        private readonly string dbName;

        public NDatabaseNoSqlRepository()
        {
            dbName = Settings.Default.Ndatabase;
        }
        public void Add(TKey id, TObject aObject)
        {
            using (var odb = OdbFactory.Open(dbName))
            {
               
                odb.Store((IStorable)aObject);
            }


        }

        public void Delete(TKey id)
        {
            using (var odb = OdbFactory.Open(dbName))
            {
                var q = from TObject p in odb.AsQueryable<TObject>()
                        where ((IStorable)p).id.Equals(id)
                        select p;

                odb.Delete((IStorable)q.First());

            }
        }

        public void Update(TKey id, TObject aGuitar)
        {
            using (var odb = OdbFactory.Open(dbName))
            {
                var q = from TObject p in odb.AsQueryable<TObject>()
                        where ((IStorable)p).id.Equals(id)
                        select p;
                var result = q.First();
                odb.Delete((IStorable)result);
                odb.Store((IStorable)aGuitar);

            }
        }

        public Maybe<TObject> Get(TKey id)
        {
            using (var odb = OdbFactory.Open(dbName))
            {
                var q = from TObject p in odb.AsQueryable<TObject>()
                        where ((IStorable)p).id.Equals(id)
                        select p;

                if (!q.Any()) return new Maybe<TObject>();

                return new Maybe<TObject>(q.First());
            }
        }

        public void ClearAll()
        {
            using (var odb = OdbFactory.Open(dbName))
            {
                IList<TObject> objects = odb.Query<TObject>().Execute<TObject>().ToList();
                foreach (var anObject in objects)
                {
                    odb.Delete((IStorable)anObject);
                }
            }
        }

        public IEnumerator<TObject> GetEnumerator()
        {
            using (var odb = OdbFactory.Open(dbName))
            {
                var q2 = odb.Query<TObject>().Execute<TObject>().ToList();
                return q2.GetEnumerator();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Task Clear()
        {
           ClearAll();
            var task = Task<string>.Factory.StartNew(() =>
            {
                string s = ".NET";
                return s;
            });
            return task;
        }
    }
}
