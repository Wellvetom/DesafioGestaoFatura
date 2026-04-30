using DesafioGestaoFatura.Application.DTO;
using DesafioGestaoFatura.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioGestaoFatura.Application.Interfaces
{
    public interface IFaturaRepository
    {

        Task AddAsync(Fatura fatura);
        Task<Fatura?> GetByIdAsync(Guid id);
        Task<List<Fatura>> GetAllAsync();
        Task<List<Fatura>> GetByFiltro(BuscaFaturaDto dto);
        Task SaveChangesItemFatura(ItemFatura item);
        Task SaveChangesAsync();
    }
}
