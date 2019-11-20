using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentFlow.Domain.Interfaces.Services
{
    public interface IServiceBase<TEntity> where TEntity : class
    {
        Task Add(TEntity entity);
        
        Task<TEntity> GetById(string id);

        Task<IEnumerable<TEntity>> GetAll();

        Task Delete(string id);
    }
}
