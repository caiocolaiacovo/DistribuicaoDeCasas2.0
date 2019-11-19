using ExpectedObjects;
using Nosbor.FluentBuilder.Lib;
using System;
using Xunit;

namespace Selecao.Dominio.Teste
{
    public class CriterioDeFamiliaCom3OuMaisDependentesMenoresDeIdadeTeste
    {
        private readonly CriterioDeFamiliaCom3OuMaisDependentesMenoresDeIdade _criterio;
        private readonly Pessoa _dependente1Com18Anos;
        private readonly Pessoa _dependente2Com18Anos;

        public CriterioDeFamiliaCom3OuMaisDependentesMenoresDeIdadeTeste()
        {
            _criterio = new CriterioDeFamiliaCom3OuMaisDependentesMenoresDeIdade();

            _dependente1Com18Anos = FluentBuilder<Pessoa>.New()
                .With(p => p.DataDeNascimento, DateTime.Today.AddYears(-18))
                .With(p => p.Tipo, TipoPessoa.Dependente)
                .Build();
            _dependente2Com18Anos = FluentBuilder<Pessoa>.New()
                .With(p => p.DataDeNascimento, DateTime.Today.AddYears(-18))
                .With(p => p.Tipo, TipoPessoa.Dependente)
                .Build();
        }

        [Fact]
        public void Deve_criar_um_criterio_com_nome_e_pontuacao()
        {
            var criterioEsperado = new
            {
                Nome = "Família com 3 ou mais dependentes menores de idade",
                Pontos = 3
            };

            var criterio = new CriterioDeFamiliaCom3OuMaisDependentesMenoresDeIdade();

            criterioEsperado.ToExpectedObject().ShouldMatch(criterio);
        }

        [Fact]
        public void Nao_deve_atender_o_criterio_caso_a_familia_possua_um_dependente_com_18_anos()
        {
            var familia = FluentBuilder<Familia>.New().Build();
            familia.AdicionarPessoa(_dependente1Com18Anos);

            var criterioAtendido = _criterio.Satisfaz(familia);

            Assert.False(criterioAtendido);
        }

        [Fact]
        public void Nao_deve_atender_o_criterio_caso_a_familia_possua_dois_dependentes_com_18_anos()
        {
            var familia = FluentBuilder<Familia>.New().Build();
            familia.AdicionarPessoa(_dependente1Com18Anos);
            familia.AdicionarPessoa(_dependente2Com18Anos);

            var criterioAtendido = _criterio.Satisfaz(familia);

            Assert.False(criterioAtendido);
        }

        [Fact]
        public void Nao_deve_atender_o_criterio_caso_a_familia_possua_tres_dependentes_com_18_anos()
        {
        }
    }
}
