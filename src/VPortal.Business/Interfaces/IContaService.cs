using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPortal.Business.Models;

namespace VPortal.Business.Interfaces
{
    public interface IContaService : IDisposable
    {
        Task Adicionar(Conta conta);
        Task Atualizar (Conta conta);
        Task Remover(Guid id);
        Task AtualizarEndereco(Endereco endereco);
    }
}
