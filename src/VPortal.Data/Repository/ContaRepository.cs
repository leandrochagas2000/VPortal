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
    public class ContaRepository : Repository<Conta>, IContaRepository
    {
        public ContaRepository(MeuDbContext context) : base(context)
        {
        }

        public async Task<Conta> ObterContaEndereco(Guid id)
        {
            return await Db.Contas.AsNoTracking()
                .Include(c => c.Endereco)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Conta> ObterContaProdutosEndereco(Guid id)
        {
            return await Db.Contas.AsNoTracking()
                .Include(c => c.Produtos)
                .Include(c => c.Endereco)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
