using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Interfaces
{
    public interface IReadStoreHandler
    {
        Task<T> GetById<T>(int id);

        void Add<T>(T customer, int id);

        void DeleteById<T>(int id);

        Task<IEnumerable<T>> Get<T>();
    }
}
