using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPortal.Business.Interfaces;
using VPortal.Business.Models;

namespace VPortal.Business.Services
{
    public abstract class BaseService
    {
        private readonly INotificador _notificador;

        protected BaseService(INotificador notificador)
        {
            _notificador = notificador;
        }



        protected void Notificar (ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificar(error.ErrorMessage);
            }
        }

        protected void Notificar(string mensagem)
        {
            // Propagar esse erro até a camada de apresentação
            _notificador.Handle(new Notificacoes.Notificacao(mensagem));
        }

        protected bool ExecutarValidacao<TV, TE> (TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validacao.Validate (entidade);

            if (validator.IsValid) return true;

            Notificar(validator);

            return false;
        }
    }
}
