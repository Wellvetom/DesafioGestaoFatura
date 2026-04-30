using DesafioGestaoFatura.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioGestaoFatura.Application.DTO
{
    public class BuscaFaturaDto
    {
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public string? NomeCliente { get; set; }
        public StatusFatura? Status { get; set; }
    }
}
