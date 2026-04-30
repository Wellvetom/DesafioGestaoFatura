using DesafioGestaoFatura.Application.Interfaces;
using DesafioGestaoFatura.Domain.Entities;
using DesafioGestaoFatura.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioGestaoFatura.Infrastructure.Repositories
{
    public class ItemFaturaRepository : IItemFaturaRepository
    {
        private readonly AppDbContext _context;

        public ItemFaturaRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(ItemFatura itemFatura)
        {
            await _context.Itens.AddAsync(itemFatura);
            await _context.SaveChangesAsync();
        }
    }
}
