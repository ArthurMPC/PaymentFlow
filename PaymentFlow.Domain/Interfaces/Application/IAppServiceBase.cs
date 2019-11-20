using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentFlow.Domain.Interfaces.Application
{
    public interface IAppServiceBase<TEntity> where TEntity : class
    {
        Task Add(TEntity entity);

        Task<TEntity> GetById(string id);

        Task Delete(string id);

        Task<IEnumerable<TEntity>> GetAll();
    }
}
