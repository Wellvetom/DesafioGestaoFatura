using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioGestaoFatura.Application.DTO.Validations
{
    public class AdicionarItemValidation : AbstractValidator<AdicionarItemDto>
    {
        public AdicionarItemValidation()
        {
            RuleFor(x => x.Descricao)
                .NotEmpty()
                .MinimumLength(3).WithMessage("Descrição inválida");
            

            RuleFor(x => x.Quantidade)
                .GreaterThan(0).WithMessage("Quantidade inválida");

            RuleFor(x => x.ValorUnitario)
               .GreaterThan(0).WithMessage("Valor inválido");

            When(x => x.Quantidade * x.ValorUnitario > 1000, () =>
                {
                    RuleFor(x => x.Justificativa)
                        .NotEmpty()
                        .WithMessage("A justificativa obrigatória para itens cujo o valor total ultrapasse R$ 1000,00");
                });


        }
    }
}
