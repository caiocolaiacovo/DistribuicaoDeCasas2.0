using System;
using ExpectedObjects;
using Nosbor.FluentBuilder.Lib;
using Xunit;

namespace Selecao.Dominio.Teste
{
    public class CriterioDeIdadeDoPretendenteDe30A44AnosTeste
    {
        private readonly CriterioDeIdadeDoPretendenteDe30A44Anos _criterio;
        private readonly Familia _familia;

        public CriterioDeIdadeDoPretendenteDe30A44AnosTeste()
        {
            _criterio = new CriterioDeIdadeDoPretendenteDe30A44Anos();

            _familia = FluentBuilder<Familia>.New().Build();
        }

        [Fact]
        public void Deve_criar_um_criterio_com_nome_e_pontuacao()
        {
            var criterioEsperado = new
            {
                Nome = "Pretendente com idade entre 30 e 44 anos",
                Pontos = 2
            };

            var criterio = new CriterioDeIdadeDoPretendenteDe30A44Anos();

            criterioEsperado.ToExpectedObject().ShouldMatch(criterio);
        }

        [Fact]
        public void Deve_atender_o_criterio_caso_o_pretendente_possua_30_anos()
        {
            var dataDe30AnosAtras = new DateTime(DateTime.Today.Year - 30,
                DateTime.Today.Month, DateTime.Today.Day);
            var pretendente = FluentBuilder<Pessoa>.New()
                .With(p => p.DataDeNascimento, dataDe30AnosAtras)
                .With(p => p.Tipo, TipoPessoa.Pretendente)
                .Build();
            _familia.AdicionarPessoa(pretendente);

            var criterioAtendido = _criterio.Satisfaz(_familia);

            Assert.True(criterioAtendido);
        }

        [Fact]
        public void Deve_atender_o_criterio_caso_o_pretendente_possua_mais_de_30_anos()
        {
            var dataDe30AnosEUmDiaAtras = new DateTime(DateTime.Today.Year - 30,
                DateTime.Today.Month, DateTime.Today.Day - 1);
            var pretendente = FluentBuilder<Pessoa>.New()
                .With(p => p.DataDeNascimento, dataDe30AnosEUmDiaAtras)
                .With(p => p.Tipo, TipoPessoa.Pretendente)
                .Build();
            _familia.AdicionarPessoa(pretendente);

            var criterioAtendido = _criterio.Satisfaz(_familia);

            Assert.True(criterioAtendido);
        }

        [Fact]
        public void Deve_atender_o_criterio_caso_o_pretendente_possua_44_anos()
        {
            var dataDe30AnosEUmDiaAtras = new DateTime(DateTime.Today.Year - 45,
                DateTime.Today.Month, DateTime.Today.Day + 1);
            var pretendente = FluentBuilder<Pessoa>.New()
                .With(p => p.DataDeNascimento, dataDe30AnosEUmDiaAtras)
                .With(p => p.Tipo, TipoPessoa.Pretendente)
                .Build();
            _familia.AdicionarPessoa(pretendente);

            var criterioAtendido = _criterio.Satisfaz(_familia);

            Assert.True(criterioAtendido);
        }

        [Fact]
        public void Nao_deve_atender_o_criterio_caso_o_pretendente_possua_menos_de_30_anos()
        {
            var dataDeUmDiaAntesDoAniversarioDe30Anos = new DateTime(DateTime.Today.Year - 30,
                DateTime.Today.Month, DateTime.Today.Day + 1);
            var pretendente = FluentBuilder<Pessoa>.New()
                .With(p => p.DataDeNascimento, dataDeUmDiaAntesDoAniversarioDe30Anos)
                .With(p => p.Tipo, TipoPessoa.Pretendente)
                .Build();
            _familia.AdicionarPessoa(pretendente);

            var criterioAtendido = _criterio.Satisfaz(_familia);

            Assert.False(criterioAtendido);
        }

        [Fact]
        public void Nao_deve_atender_o_criterio_caso_o_pretendente_possua_mais_de_44_anos()
        {
            var dataUmDiaDepoisDoAniversarioDe30Anos = new DateTime(DateTime.Today.Year - 45,
                DateTime.Today.Month, DateTime.Today.Day);
            var pretendente = FluentBuilder<Pessoa>.New()
                .With(p => p.DataDeNascimento, dataUmDiaDepoisDoAniversarioDe30Anos)
                .With(p => p.Tipo, TipoPessoa.Pretendente)
                .Build();
            _familia.AdicionarPessoa(pretendente);

            var criterioAtendido = _criterio.Satisfaz(_familia);

            Assert.False(criterioAtendido);
        }
    }
}