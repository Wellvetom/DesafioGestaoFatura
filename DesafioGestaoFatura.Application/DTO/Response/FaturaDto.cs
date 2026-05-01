using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioGestaoFatura.Application.DTO.Response
{
    public class FaturaDto
    {
        public Guid Id { get; set; }
        public string NomeCliente { get; set; }
        public decimal ValorTotal { get; set; }
        public string Status { get; set; }
        public List<ItemFaturaDto> Itens { get; set; }
    }
}
