using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPortal.Business.Models;

namespace VPortal.Business.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> ObterProdutosPorConta(Guid id);
        Task<IEnumerable<Produto>> ObterProdutosConta();

        Task<Produto> ObterProdutoConta(Guid id);

    }
}
