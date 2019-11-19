using System;
using ExpectedObjects;
using Nosbor.FluentBuilder.Lib;
using Xunit;

namespace Selecao.Dominio.Teste
{
    public class CriterioDeIdadeDoPretendenteComMenosDe30AnosTeste
    {
        private CriterioDeIdadeDoPretendenteComMenosDe30Anos _criterio;
        private Familia _familia;

        public CriterioDeIdadeDoPretendenteComMenosDe30AnosTeste()
        {
            _criterio = new CriterioDeIdadeDoPretendenteComMenosDe30Anos();

            _familia = FluentBuilder<Familia>.New().Build();
        }

        [Fact]
        public void Deve_criar_um_criterio_com_nome_e_pontuacao()
        {
            var criterioEsperado = new {
                Nome = "Pretendente com menos de 30 anos",
                Pontos = 1
            };

            var criterio = new CriterioDeIdadeDoPretendenteComMenosDe30Anos();

            criterioEsperado.ToExpectedObject().ShouldMatch(criterio);
        }

        [Fact]
        public void Deve_atender_o_criterio_caso_o_pretendente_possua_menos_de_30_anos()
        {
            var dataUmDiaAntesDoAniversarioDe30Anos = new DateTime(DateTime.Today.Year - 30, 
                DateTime.Today.Month, DateTime.Today.Day + 1);
            var pretendente = FluentBuilder<Pessoa>.New()
                .With(p => p.DataDeNascimento, dataUmDiaAntesDoAniversarioDe30Anos)
                .With(p => p.Tipo, TipoPessoa.Pretendente)
                .Build();
            _familia.AdicionarPessoa(pretendente);

            var criterioAtendido = _criterio.Satisfaz(_familia);

            Assert.True(criterioAtendido);
        }

        [Fact]
        public void Nao_deve_atender_o_criterio_caso_o_pretendente_possua_30_anos()
        {
            var dataDe30AnosAtras = new DateTime(DateTime.Today.Year - 30, 
                DateTime.Today.Month, DateTime.Today.Day);
            var pretendente = FluentBuilder<Pessoa>.New()
                .With(p => p.DataDeNascimento, dataDe30AnosAtras)
                .With(p => p.Tipo, TipoPessoa.Pretendente)
                .Build();
            _familia.AdicionarPessoa(pretendente);

            var criterioAtendido = _criterio.Satisfaz(_familia);

            Assert.False(criterioAtendido);
        }

        [Fact]
        public void Nao_deve_atender_o_criterio_caso_o_pretendente_possua_mais_de_30_anos()
        {
            var dataUmDiaDepoisDoAniversarioDe30Anos = new DateTime(DateTime.Today.Year - 30, 
                DateTime.Today.Month, DateTime.Today.Day - 1);
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