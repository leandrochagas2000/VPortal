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
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(MeuDbContext context) : base(context)
        {
        }

        public async Task<Endereco> ObterEnderecoPorConta(Guid contaId)
        {
            return await Db.Enderecos.AsNoTracking()
                .FirstOrDefaultAsync(c => c.ContaId == contaId);
        }
    }
}
