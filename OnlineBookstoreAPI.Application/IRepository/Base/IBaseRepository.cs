using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstoreAPI.Application.IRepository.Base
{
    public interface IBaseRepository<T1, T2>
    {
        Task<IQueryable<T1>?> GetAll();
        Task<T1?> FindById(T2 Id);
        Task<T1?> Insert(T1 entity);
        Task<T1?> Update(T2 id, T1 entity);
        Task<bool> Delete(T2 id);
    }
}
