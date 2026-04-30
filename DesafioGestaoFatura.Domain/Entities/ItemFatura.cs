using DesafioGestaoFatura.Domain.Exception;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioGestaoFatura.Domain.Entities
{
    public class ItemFatura
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal => Quantidade * ValorUnitario;
        public string? Justificativa { get; set; }
        public Guid FaturaId { get; set; }
        public Fatura Fatura { get; set; }
 
        public ItemFatura(string descricao, int quantidade, decimal valorUnitario, string? justificativa)
        {
            if (string.IsNullOrWhiteSpace(descricao) || descricao.Length < 3)
                throw new DomainException("Descrição inválida");

            if (quantidade <= 0)
                throw new DomainException("Quantidade inválida");

            if (valorUnitario <= 0)
                throw new DomainException("Valor inválido");

            if ((quantidade * valorUnitario) > 1000 && string.IsNullOrWhiteSpace(justificativa))
                throw new DomainException("A justificativa obrigatória para itens cujo o valor total ultrapasse R$ 1000,00");

            Descricao = descricao;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            Justificativa = justificativa;
        }
    }
}
