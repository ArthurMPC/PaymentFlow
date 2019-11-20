using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentFlow.Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {

        Task Add(TEntity entity);

        Task Add(IEnumerable<TEntity> entities);

        Task<TEntity> GetById(string id);

        Task<List<TEntity>> GetAll();

        Task Delete(string id);

    }
}
