using DesafioGestaoFatura.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioGestaoFatura.Application.Interfaces
{
    public interface IItemFaturaRepository
    {
        Task AddAsync(ItemFatura itemFatura);

    }
}
