using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPortal.Business.Interfaces;
using VPortal.Business.Models;
using VPortal.Data.Context;

namespace VPortal.Data.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(MeuDbContext context) : base(context) { }

        public async Task<Produto> ObterProdutoConta(Guid id)
        {
            return await Db.Produtos.AsNoTracking()
                .Include(f => f.Conta)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Produto>> ObterProdutosConta()
        {
            return await Db.Produtos.AsNoTracking().Include(f => f.Conta)
                .OrderBy(p => p.Nome).ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterProdutosPorConta(Guid contaId)
        {
            return await Buscar(p => p.ContaId == contaId);
        }
    }
}
