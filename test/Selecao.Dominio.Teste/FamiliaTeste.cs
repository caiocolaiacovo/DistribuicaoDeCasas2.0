using ExpectedObjects;
using Moq;
using Nosbor.FluentBuilder.Lib;
using Xunit;

namespace Selecao.Dominio.Teste
{
    public class FamiliaTeste
    {
        [Fact]
        public void Deve_adicionar_um_criterio_a_familia()
        {
            var familia = FluentBuilder<Familia>.New().Build();
            var criterio = new Mock<ICriterio>();
            var criteriosEsperados = new [] { criterio.Object };

            familia.AdicionarCriterioAtendido(criterio.Object);

            criteriosEsperados.ToExpectedObject().ShouldMatch(familia.Criterios);
        }
    }
}