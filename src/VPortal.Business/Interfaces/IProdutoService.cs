using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPortal.Business.Models;

namespace VPortal.Business.Interfaces
{
    public interface IProdutoService : IDisposable
    {
        Task Adicionar (Produto produto);
        Task Atualizar (Produto produto);
        Task Remover (Guid id);
    }
}
