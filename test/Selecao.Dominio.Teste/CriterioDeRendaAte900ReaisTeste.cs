using ExpectedObjects;
using Nosbor.FluentBuilder.Lib;
using Xunit;

namespace Selecao.Dominio.Teste
{
    public class CriterioDeRendaAte900ReaisTeste
    {
        [Fact]
        public void Deve_criar_um_criterio_com_nome_e_pontuacao()
        {
            var criterioEsperado = new
            {
                Nome = "Renda até 900 reais",
                Pontos = 5
            };

            var criterio = new CriterioDeRendaAte900Reais();

            criterioEsperado.ToExpectedObject().ShouldMatch(criterio);
        }

        [Theory]
        [InlineData(0, 900)]
        [InlineData(900, 0)]
        [InlineData(450, 450)]
        public void Deve_atender_o_criterio_caso_a_familia_possua_renda_de_ate_900_reais(
            decimal rendaPretendente, decimal rendaConjuge)
        {
            var criterio = new CriterioDeRendaAte900Reais();
            var familia = FluentBuilder<Familia>.New().Build();
            var pretendente = FluentBuilder<Pessoa>.New().With(p => p.Renda, rendaPretendente).Build();
            var conjuge = FluentBuilder<Pessoa>.New().With(p => p.Renda, rendaConjuge).Build();
            familia.AdicionarPessoa(pretendente);
            familia.AdicionarPessoa(conjuge);

            var criterioAtendido = criterio.Satisfaz(familia);

            Assert.True(criterioAtendido);
        }
    }
}