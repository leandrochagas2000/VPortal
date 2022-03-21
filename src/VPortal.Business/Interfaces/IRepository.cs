using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VPortal.Business.Models;

namespace VPortal.Business.Interfaces
{

    // Repositorio genérico (metodo para qualquer entidade)
    // utiliza IDisposable para liberar memoria
    // where para TEntity ser usada somente se for filha de "Entity"
    public interface IRepository <TEntity> : IDisposable where TEntity : Entity
    {
        Task<List<TEntity>> ObterTodos();
        Task<TEntity> ObterPorId(Guid id);
        Task Adicionar(TEntity entity);
        Task Atualizar(TEntity entity);
        Task Remover(Guid id);
        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();
    }
}
