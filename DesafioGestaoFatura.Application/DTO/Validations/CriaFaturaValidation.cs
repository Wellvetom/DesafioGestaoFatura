using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace DesafioGestaoFatura.Application.DTO.Validations
{

    public class CriarFaturaValidator : AbstractValidator<CriarFaturaDto>
    {
        public CriarFaturaValidator()
        {
            RuleFor(x => x.NomeCliente)
                .NotEmpty().WithMessage("Nome do cliente é obrigatório");
        }
    }
}
