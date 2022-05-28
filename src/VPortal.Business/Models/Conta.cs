using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPortal.Business.Models
{
    public class Conta : Entity
    {
        public string UserId { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public TipoConta TipoConta { get; set; }
        public Endereco Endereco { get; set; }
        public bool Ativo { get; set; }

        /* EF Relations */
        public IEnumerable<Produto> Produtos { get; set; }
    }
}
