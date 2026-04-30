using DesafioGestaoFatura.Domain.Enums;
using DesafioGestaoFatura.Domain.Exception;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioGestaoFatura.Domain.Entities
{
    public class Fatura
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Numero { get; set; }
        public string NomeCliente { get; set; }
        public DateTime DataEmissao { get; set; } = DateTime.UtcNow;
        public StatusFatura Status { get; set; } = StatusFatura.Aberta;
        public decimal ValorTotal { get; set; }
        public List<ItemFatura> Itens { get; set; } = new();
        public Fatura(string nomeCliente)
        {
            if (string.IsNullOrWhiteSpace(nomeCliente))
                throw new DomainException("Nome do cliente é obrigatório");

            NomeCliente = nomeCliente;
            Numero = Guid.NewGuid().ToString().Substring(0, 8);
        }

        public void AdicionarItem(ItemFatura item)
        {
            if (Status == StatusFatura.Fechada)
                throw new DomainException("Fatura fechada não pode ser alterada");

            item.FaturaId = this.Id; 

            Itens.Add(item);
            RecalcularTotal();
        }

        public void Fechar()
        {
            if (Status == StatusFatura.Fechada)
                throw new DomainException("Fatura já está fechada");

            Status = StatusFatura.Fechada;
        }

        private void RecalcularTotal()
        {
            ValorTotal = Itens.Sum(i => i.ValorTotal);
        }
    }
}
