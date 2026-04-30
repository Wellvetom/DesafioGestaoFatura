using DesafioGestaoFatura.Application.DTO;
using DesafioGestaoFatura.Application.DTO.Validations;
using DesafioGestaoFatura.Domain.Entities;
using DesafioGestaoFatura.Domain.Enums;
using DesafioGestaoFatura.Domain.Exception;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioGestaoFatura.Teste
{
    public class FaturaTeste
    {

        #region Testes nas regras criadas na domain
        [Fact]
        public void Deve_Criar_Fatura_Valida()
        {
            var fatura = new Fatura("Cliente");

            fatura.Status.Should().Be(StatusFatura.Aberta);
        }
        [Fact]
        public void Nao_Deve_Permitir_Item_Sem_Justificativa()
        {
            Action act = () => new ItemFatura("Item", 1, 2000, null);

            act.Should().Throw<DomainException>();
        }
        [Fact]
        public void Deve_Fechar_Fatura()
        {
            var fatura = new Fatura("Cliente");

            fatura.Fechar();

            fatura.Status.Should().Be(StatusFatura.Fechada);
        }
        [Fact]
        public void Nao_Deve_Fechar_Fatura_Ja_Fechada()
        {
            var fatura = new Fatura("Cliente");
            fatura.Fechar();

            Action act = () => fatura.Fechar();

            act.Should().Throw<DomainException>();
        }
        [Fact]
        public void Nao_Deve_Criar_Item_Com_Quantidade_Invalida()
        {
            Action act = () => new ItemFatura("Produto", 0, 100, null);

            act.Should().Throw<DomainException>();
        }
        [Fact]
        public void Nao_Deve_Criar_Item_Com_Descricao_Invalida()
        {
            Action act = () => new ItemFatura("", 1, 100, null);

            act.Should().Throw<DomainException>();
        }
        [Fact]
        public void Nao_Deve_Criar_Fatura_Sem_Cliente()
        {
            Action act = () => new Fatura("");

            act.Should().Throw<DomainException>();
        }
        #endregion
        [Fact]
        public void Deve_Retornar_Erro_Quando_Descricao_Vazia()
        {
            var validator = new AdicionarItemValidation();

            var dto = new AdicionarItemDto
            {
                Descricao = "",
                Quantidade = 1,
                ValorUnitario = 100
            };

            var result = validator.Validate(dto);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.ErrorMessage == "Descrição inválida");
        }
        #region Testes nos DTO´s
        [Fact]
        public void Deve_Retornar_Erro_Quando_NomeCliente_Estiver_Vazio()
        {
            var validator = new CriarFaturaValidator();

            var dto = new CriarFaturaDto
            {
                NomeCliente = ""
            };

            var result = validator.Validate(dto);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.ErrorMessage == "Nome do cliente é obrigatório");
        }
        [Fact]
        public void Deve_Retornar_Erro_Quando_Justificativa_Estiver_Vazia()
        {
            var validator = new AdicionarItemValidation();

            var dto = new AdicionarItemDto
            {
                Descricao = "teste",
                Quantidade = 10,
                ValorUnitario = 2000,
                Justificativa = ""
            };

            var result = validator.Validate(dto);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.ErrorMessage == "A justificativa obrigatória para itens cujo o valor total ultrapasse R$ 1000,00");
        }
        #endregion
    }
}
