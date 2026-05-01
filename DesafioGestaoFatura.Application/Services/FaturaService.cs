using DesafioGestaoFatura.Application.DTO;
using DesafioGestaoFatura.Application.DTO.Response;
using DesafioGestaoFatura.Application.Interfaces;
using DesafioGestaoFatura.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioGestaoFatura.Application.Services
{
    public class FaturaService
    {
        private readonly IFaturaRepository _repo;

        public FaturaService(IFaturaRepository repo)
        {
            _repo = repo;
        }

        public async Task<Guid> CriarFatura(CriarFaturaDto dto)
        {
            var fatura = new Fatura(dto.NomeCliente);

            await _repo.AddAsync(fatura);

            return fatura.Id;
        }

        public async Task<List<FaturaDto>> BuscarFaturas(BuscaFaturaDto dto)
        {
            var faturas = await _repo.GetByFiltro(dto);
            return faturas.Select(f => new FaturaDto
            {
                Id = f.Id,
                NomeCliente = f.NomeCliente,
                ValorTotal = f.ValorTotal,
                Status = f.Status.ToString(),
                Itens = f.Itens.Select(i => new ItemFaturaDto
                {
                    Descricao = i.Descricao,
                    Quantidade = i.Quantidade,
                    ValorUnitario = i.ValorUnitario,
                    Justificativa = i.Justificativa
                }).ToList()
            }).ToList();
        }

        public async Task AdicionarItem(Guid id, AdicionarItemDto dto)
        {
            var fatura = await _repo.GetByIdAsync(id);

            if (fatura == null)
                throw new Exception("Fatura não encontrada");

            var item = new ItemFatura(dto.Descricao, dto.Quantidade, dto.ValorUnitario, dto.Justificativa);

            fatura.AdicionarItem(item);
            await _repo.SaveChangesItemFatura(item);
        }

        public async Task FecharFatura(Guid id)
        {
            var fatura = await _repo.GetByIdAsync(id);

            if (fatura == null)
                throw new Exception("Fatura não encontrada");

            fatura.Fechar();

            await _repo.SaveChangesAsync();
        }
    }
}
