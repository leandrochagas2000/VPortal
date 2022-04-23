using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPortal.Business.Interfaces;
using VPortal.Business.Models;
using VPortal.Business.Models.Validations;
using VPortal.Business.Models.Validations.Documentos;

namespace VPortal.Business.Services
{
    public class ContaService : BaseService, IContaService
    {

        private readonly IContaRepository _contaRepository;
        private readonly IEnderecoRepository _enderecoRepository;

        public ContaService(IContaRepository contaRepository,
                            IEnderecoRepository enderecoRepository,
                            INotificador notificador) : base(notificador)
        {
            _contaRepository = contaRepository;
            _enderecoRepository = enderecoRepository;
        }


        public async Task Adicionar(Conta conta)
        {
            // Validar o estado da entidade - utilizando Fluent Validation
            if (!ExecutarValidacao(new ContaValidation(), conta)
                || !ExecutarValidacao(new EnderecoValidation(), conta.Endereco)) return;

            // Verifica se ja existe documento na base
            if (_contaRepository.Buscar(c => c.Documento == conta.Documento).Result.Any())
            {
                Notificar("Já existe uma conta com este documento informado.");
                return;
            }

            await _contaRepository.Adicionar(conta);
        }

        public async Task Atualizar(Conta conta)
        {
            if (!ExecutarValidacao(new ContaValidation(), conta)) return;

            if (_contaRepository.Buscar(c => c.Documento == conta.Documento && c.Id != conta.Id).Result.Any())
            {
                Notificar("Já existe uma conta com este documento informado");
                return;
            }

            await _contaRepository.Atualizar(conta);
        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            if (!ExecutarValidacao(new EnderecoValidation(), endereco)) return;

            await _enderecoRepository.Atualizar(endereco);

        }

        public async Task Remover(Guid id)
        {
            if (_contaRepository.ObterContaProdutosEndereco(id).Result.Produtos.Any())
            {
                Notificar("A conta possui produtos cadastrados, necessário deletar produtos para então excluir a conta.");
                return;
            }

            await _contaRepository.Remover(id);
        }

        public void Dispose()
        {
            _contaRepository?.Dispose();
            _enderecoRepository?.Dispose();
        }
    }
}
