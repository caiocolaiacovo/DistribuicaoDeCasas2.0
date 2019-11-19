using ExpectedObjects;
using Nosbor.FluentBuilder.Lib;
using System;
using Xunit;

namespace Selecao.Dominio.Teste
{
    public class CriterioDeFamiliaCom1Ou2DependentesMenoresDeIdadeTeste
    {
        private readonly CriterioDeFamiliaCom1Ou2DependentesMenoresDeIdade _criterio;
        private readonly Pessoa _dependente1Com18Anos;
        private readonly Pessoa _dependente2Com18Anos;

        public CriterioDeFamiliaCom1Ou2DependentesMenoresDeIdadeTeste()
        {
            _criterio = new CriterioDeFamiliaCom1Ou2DependentesMenoresDeIdade();

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
                Nome = "Família com 1 ou 2 dependentes menores de idade",
                Pontos = 2
            };

            var criterio = new CriterioDeFamiliaCom1Ou2DependentesMenoresDeIdade();

            criterioEsperado.ToExpectedObject().ShouldMatch(criterio);
        }

        [Fact]
        public void Deve_atender_o_criterio_caso_a_familia_possua_um_dependente_com_18_anos()
        {
            var familia = FluentBuilder<Familia>.New().Build();
            familia.AdicionarPessoa(_dependente1Com18Anos);

            var criterioAtendido = _criterio.Satisfaz(familia);

            Assert.True(criterioAtendido);
        }

        [Fact]
        public void Deve_atender_o_criterio_caso_a_familia_possua_dois_dependentes_com_18_anos()
        {
            var familia = FluentBuilder<Familia>.New().Build();
            familia.AdicionarPessoa(_dependente1Com18Anos);
            familia.AdicionarPessoa(_dependente2Com18Anos);

            var criterioAtendido = _criterio.Satisfaz(familia);

            Assert.True(criterioAtendido);
        }

        [Fact]
        public void Nao_deve_atender_o_criterio_caso_a_familia_possua_mais_de_dois_dependentes_com_18_anos()
        {
            var familia = FluentBuilder<Familia>.New().Build();
            familia.AdicionarPessoa(_dependente1Com18Anos);
            familia.AdicionarPessoa(_dependente2Com18Anos);
            var dependente3Com18Anos = FluentBuilder<Pessoa>.New()
                .With(p => p.DataDeNascimento, DateTime.Today.AddYears(-18))
                .With(p => p.Tipo, TipoPessoa.Dependente)
                .Build();
            familia.AdicionarPessoa(dependente3Com18Anos);

            var criterioAtendido = _criterio.Satisfaz(familia);

            Assert.False(criterioAtendido);
        }

        [Fact]
        public void Nao_deve_atender_o_criterio_caso_a_familia_possua_um_dependente_com_mais_de_18_anos()
        {
            var familia = FluentBuilder<Familia>.New().Build();
            var dependenteComMaisDe18Anos = FluentBuilder<Pessoa>.New()
                .With(p => p.DataDeNascimento, DateTime.Today.AddYears(-18).AddDays(-1))
                .With(p => p.Tipo, TipoPessoa.Dependente)
                .Build();
            familia.AdicionarPessoa(dependenteComMaisDe18Anos);

            var criterioAtendido = _criterio.Satisfaz(familia);

            Assert.False(criterioAtendido);
        }

        [Fact]
        public void Nao_deve_atender_o_criterio_caso_a_familia_possua_uma_pessoa_nao_dependente_com_18_anos()
        {
            var familia = FluentBuilder<Familia>.New().Build();
            var conjuge = FluentBuilder<Pessoa>.New()
                .With(p => p.DataDeNascimento, DateTime.Today.AddYears(-18))
                .With(p => p.Tipo, TipoPessoa.Conjuge)
                .Build();
            familia.AdicionarPessoa(conjuge);

            var criterioAtendido = _criterio.Satisfaz(familia);

            Assert.False(criterioAtendido);
        }
    }
}
