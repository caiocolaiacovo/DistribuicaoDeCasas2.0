using ExpectedObjects;
using Nosbor.FluentBuilder.Lib;
using Xunit;

namespace Selecao.Dominio.Teste
{
    public class CriterioDeRendaDe1501Ate2000ReaisTeste
    {
        private CriterioDeRendaDe1501Ate2000Reais _criterio;

        public CriterioDeRendaDe1501Ate2000ReaisTeste()
        {
            _criterio = new CriterioDeRendaDe1501Ate2000Reais();
        }

        [Fact]
        public void Deve_criar_um_criterio_com_nome_e_pontuacao()
        {
            var criterioEsperado = new {
                Nome = "Renda de 1501 até 2000 reais",
                Pontos = 1
            };

            var criterio = new CriterioDeRendaDe1501Ate2000Reais();

            criterioEsperado.ToExpectedObject().ShouldMatch(criterio);
        }

        [Theory]
        [InlineData(750, 750)]
        [InlineData(1500, 0)]
        [InlineData(0, 1500)]
        [InlineData(1000, 1001)]
        [InlineData(1001, 1000)]
        [InlineData(0, 2001)]
        public void Nao_deve_atender_o_criterio_caso_a_familia_possua_renda_fora_da_faixa_de_1501_a_2000_reais(
            decimal rendaPretendente, decimal rendaConjuge)
        {
            var familia = FluentBuilder<Familia>.New().Build();
            var pretendente = FluentBuilder<Pessoa>.New().With(p => p.Renda, rendaPretendente).Build();
            var conjuge = FluentBuilder<Pessoa>.New().With(p => p.Renda, rendaConjuge).Build();
            familia.AdicionarPessoa(pretendente);
            familia.AdicionarPessoa(conjuge);

            var criterioAtendido = _criterio.Satisfaz(familia);

            Assert.False(criterioAtendido);
        }

        [Theory]
        [InlineData(1501, 0)]
        [InlineData(1500, 500)]
        [InlineData(0, 2000)]
        public void Deve_atender_o_criterio_caso_a_familia_possua_renda_na_faixa_de_1501_a_2000_reais(
            decimal rendaPretendente, decimal rendaConjuge)
        {
            var familia = FluentBuilder<Familia>.New().Build();
            var pretendente = FluentBuilder<Pessoa>.New().With(p => p.Renda, rendaPretendente).Build();
            var conjuge = FluentBuilder<Pessoa>.New().With(p => p.Renda, rendaConjuge).Build();
            familia.AdicionarPessoa(pretendente);
            familia.AdicionarPessoa(conjuge);

            var criterioAtendido = _criterio.Satisfaz(familia);

            Assert.True(criterioAtendido);
        }
    }
}