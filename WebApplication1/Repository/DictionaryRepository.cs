using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication1.Repository
{
    public class DictionaryRepository<TKey,TObject>: IRepository<TKey, TObject>
    {  
        
        private readonly Dictionary<TKey, TObject> _dictionary;

        public DictionaryRepository()
        {
            
            _dictionary = new Dictionary<TKey, TObject>();
        }

        public void Add(TKey id, TObject anObject)
        {
       
               
            _dictionary.Add(id, anObject);

        }

        public void Update(TKey id, TObject anObject)
        {
            TObject found;
            if (_dictionary.TryGetValue(id, out found))
            {
                // yay,it exists!
                _dictionary[id] = anObject;
            }
        }

        public Maybe<TObject> Get(TKey id)
        {
            TObject found;
            if (_dictionary.TryGetValue(id, out found))
            {  // yay, it exists!
                return new Maybe<TObject>(found);
            }

            return new Maybe<TObject>();
        }


        public IEnumerator<TObject> GetEnumerator()
        {
           return  _dictionary.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        public void Delete(TKey id)
        {
            _dictionary.Remove(id);
        }


        public void ClearAll()
        {
            _dictionary.Clear();
        }

        public Task Clear()
        {
          _dictionary.Clear();

            //dummy task
            var task = Task<string>.Factory.StartNew(() =>
            {
                string s = ".NET";
                return s;
            });
            return task;
        }
    }
}
