using PaymentFlow.Domain.Interfaces.Repositories;
using PaymentFlow.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentFlow.Domain.Service
{
    public class ServiceBase<TEntity> :  IServiceBase<TEntity> where TEntity : class
    {
        private readonly IRepositoryBase<TEntity> _repository;

        public ServiceBase(IRepositoryBase<TEntity> repository)
            => _repository = repository;

        public async Task Add(TEntity entity)
            => await _repository.Add(entity);

        public async Task Delete(string id)
            => await _repository.Delete(id);

        public async Task<IEnumerable<TEntity>> GetAll()
            => await _repository.GetAll();

        public async Task<TEntity> GetById(string id)
            => await _repository.GetById(id);

    }
}
