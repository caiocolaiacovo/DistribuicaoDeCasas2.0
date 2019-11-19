using ExpectedObjects;
using Nosbor.FluentBuilder.Lib;
using Xunit;

namespace Selecao.Dominio.Teste
{
    public class CriterioDeRendaDe901Ate1500ReaisTeste
    {
        private readonly CriterioDeRendaDe901Ate1500Reais _criterio;

        public CriterioDeRendaDe901Ate1500ReaisTeste()
        {
            _criterio = new CriterioDeRendaDe901Ate1500Reais();
        }

        [Fact]
        public void Deve_criar_um_criterio_com_nome_e_pontuacao()
        {
            var criterioEsperado = new
            {
                Nome = "Renda de 901 até 1500 reais",
                Pontos = 3
            };

            var criterio = new CriterioDeRendaDe901Ate1500Reais();

            criterioEsperado.ToExpectedObject().ShouldMatch(criterio);
        }

        [Theory]
        [InlineData(450, 450)]
        [InlineData(900, 0)]
        [InlineData(0, 900)]
        [InlineData(751, 750)]
        [InlineData(750, 751)]
        [InlineData(0, 1501)]
        [InlineData(1000, 1000)]
        public void Nao_deve_atender_o_criterio_caso_a_familia_possua_renda_fora_da_faixa_de_900_a_1500_reais(
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
        [InlineData(901, 0)]
        [InlineData(599, 901)]
        [InlineData(0, 1500)]
        public void Deve_atender_o_criterio_caso_a_familia_possua_renda_na_faixa_de_900_a_1500_reais(
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