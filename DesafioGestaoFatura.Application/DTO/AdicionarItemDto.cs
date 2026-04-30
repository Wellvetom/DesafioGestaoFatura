using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioGestaoFatura.Application.DTO
{
    public class AdicionarItemDto
    {
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public string? Justificativa { get; set; }
    }
}
