using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPortal.Business.Models.Validations.Documentos;

namespace VPortal.Business.Models.Validations
{
    internal class ContaValidation : AbstractValidator<Conta>
    {
        public ContaValidation()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 100)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            When(c => c.TipoConta == TipoConta.PessoaFisica, () =>
            {
                RuleFor(c => c.Documento.Length).Equal(CpfValidacao.TamanhoCpf)
                    .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
                RuleFor(c => CpfValidacao.Validar(c.Documento)).Equal(true)
                    .WithMessage("O documento fornecido é invalido.");
            });

            When(c => c.TipoConta == TipoConta.PessoaJurica, () =>
            {
                RuleFor(c => c.Documento.Length).Equal(CnpjValidacao.TamanhoCnpj)
                   .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
                RuleFor(c => CnpjValidacao.Validar(c.Documento)).Equal(true)
                    .WithMessage("O documento fornecido é invalido.");
            });
        }

    }
}
