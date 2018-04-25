using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication1.Repository
{
    public interface IRepository<TKey, TObject> : IEnumerable<TObject>
    {
        void Add(TKey id, TObject anObject);

        void Delete(TKey id);
        void Update(TKey id, TObject anObject);
        Maybe<TObject> Get(TKey id);

        Task Clear();
        void ClearAll();
    }
}