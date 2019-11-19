using System;
using ExpectedObjects;
using Nosbor.FluentBuilder.Lib;
using Xunit;

namespace Selecao.Dominio.Teste
{
    public class CriterioDeIdadeDoPretendenteCom45AnosOuMaisTeste
    {
        private readonly CriterioDeIdadeDoPretendenteCom45AnosOuMais _criterio;
        private readonly Familia _familia;

        public CriterioDeIdadeDoPretendenteCom45AnosOuMaisTeste()
        {
            _criterio = new CriterioDeIdadeDoPretendenteCom45AnosOuMais();

            _familia = FluentBuilder<Familia>.New().Build();
        }

        [Fact]
        public void Deve_criar_um_criterio_com_nome_e_pontuacao()
        {
            var criterioEsperado = new
            {
                Nome = "Pretendente com 45 anos ou mais",
                Pontos = 3
            };

            var criterio = new CriterioDeIdadeDoPretendenteCom45AnosOuMais();

            criterioEsperado.ToExpectedObject().ShouldMatch(criterio);
        }

        [Fact]
        public void Deve_atender_o_criterio_caso_o_pretendente_possua_45_anos()
        {
            var dataDe45AnosAtras = new DateTime(DateTime.Today.Year - 45,
                DateTime.Today.Month, DateTime.Today.Day);
            var pretendente = FluentBuilder<Pessoa>.New()
                .With(p => p.DataDeNascimento, dataDe45AnosAtras)
                .With(p => p.Tipo, TipoPessoa.Pretendente)
                .Build();
            _familia.AdicionarPessoa(pretendente);

            var criterioAtendido = _criterio.Satisfaz(_familia);

            Assert.True(criterioAtendido);
        }

        [Fact]
        public void Deve_atender_o_criterio_caso_o_pretendente_possua_mais_de_45_anos()
        {
            var dataDeUmDiaAposOAniversarioDe45Anos = new DateTime(DateTime.Today.Year - 45,
                DateTime.Today.Month, DateTime.Today.Day - 1);
            var pretendente = FluentBuilder<Pessoa>.New()
                .With(p => p.DataDeNascimento, dataDeUmDiaAposOAniversarioDe45Anos)
                .With(p => p.Tipo, TipoPessoa.Pretendente)
                .Build();
            _familia.AdicionarPessoa(pretendente);

            var criterioAtendido = _criterio.Satisfaz(_familia);

            Assert.True(criterioAtendido);
        }

        [Fact]
        public void Nao_deve_atender_o_criterio_caso_o_pretendente_possua_menos_de_45_anos()
        {
            var dataDeUmDiaAntesDoAniversarioDe45Anos = new DateTime(DateTime.Today.Year - 45,
                DateTime.Today.Month, DateTime.Today.Day + 1);
            var pretendente = FluentBuilder<Pessoa>.New()
                .With(p => p.DataDeNascimento, dataDeUmDiaAntesDoAniversarioDe45Anos)
                .With(p => p.Tipo, TipoPessoa.Pretendente)
                .Build();
            _familia.AdicionarPessoa(pretendente);

            var criterioAtendido = _criterio.Satisfaz(_familia);

            Assert.False(criterioAtendido);
        }
    }
}