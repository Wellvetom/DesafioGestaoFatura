using DesafioGestaoFatura.Application.DTO;
using DesafioGestaoFatura.Application.Interfaces;
using DesafioGestaoFatura.Domain.Entities;
using DesafioGestaoFatura.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioGestaoFatura.Infrastructure.Repositories
{
    public class FaturaRepository : IFaturaRepository
    {
        private readonly AppDbContext _context;

        public FaturaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Fatura fatura)
        {
            await _context.Faturas.AddAsync(fatura);
            await _context.SaveChangesAsync();
        }

        public async Task<Fatura?> GetByIdAsync(Guid id)
        {
            return await _context.Faturas
                .Include(f => f.Itens)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<List<Fatura>> GetAllAsync()
        {
            return await _context.Faturas.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesItemFatura(ItemFatura item)
        {
            _context.Itens.Add(item);
            await _context.SaveChangesAsync();

        }

        public async Task<List<Fatura>> GetByFiltro(BuscaFaturaDto dto)
        {
            var query = _context.Faturas
            .Include(f => f.Itens)
            .AsQueryable();

            if (!string.IsNullOrWhiteSpace(dto.NomeCliente))
            {
                query = query.Where(x => x.NomeCliente.Contains(dto.NomeCliente));
            }

            if (dto.Status.HasValue)
            {
                query = query.Where(x => x.Status == dto.Status.Value);
            }

            if (dto.DataInicio.HasValue)
            {
                query = query.Where(x => x.DataEmissao.Date >= dto.DataInicio.Value.Date);
            }

            if (dto.DataFim.HasValue)
            {
                query = query.Where(x => x.DataEmissao.Date <= dto.DataFim.Value.Date);
            }

            return await query.ToListAsync();
        }
    }
}
