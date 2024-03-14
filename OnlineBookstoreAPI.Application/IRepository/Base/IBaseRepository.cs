using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstoreAPI.Application.IRepository.Base
{
    public interface IBaseRepository<T>
    {
        Task<IEnumerable<T>?> GetAll();
        Task<T?> FindById(int Id);
        Task<T?> Insert(T entity);
        Task<T?> Update(int id, T entity);
        Task<bool> Delete(int id);
    }
}
