using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPortal.Business.Models;

namespace VPortal.Business.Interfaces
{
    public interface IContaRepository : IRepository<Conta>
    {
        Task<Conta> ObterContaEndereco(Guid id);
        Task<Conta> ObterContaProdutosEndereco(Guid id);
    }
}
