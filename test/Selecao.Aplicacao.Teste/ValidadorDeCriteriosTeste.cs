using System.Collections.Generic;
using Moq;
using Nosbor.FluentBuilder.Lib;
using Selecao.Dominio;
using Xunit;

namespace Selecao.Aplicacao.Teste
{
  public class ValidadorDeCriteriosTeste
    {
        [Fact]
        public void Nao_deve_validar_criterio_de_uma_familia_nula()
        {
            var mensagemDeErroEsperada = "É obrigatório informar uma família para validar critérios";
            var familia = (Familia)null;
            var validadorDeCriterios = new ValidadorDeCriterios(new List<ICriterio>());

            void Acao() => validadorDeCriterios.Validar(familia);

            var mensagemDeErro = Assert.Throws<ExcecaoDeAplicacao>(Acao).Message;
            Assert.Equal(mensagemDeErroEsperada, mensagemDeErro);
        }

        [Fact]
        public void Deve_percorrer_todos_os_criterios_ao_validar_uma_familia()
        {
            var familia = FluentBuilder<Familia>.New().Build();
            var criterio1 = new Mock<ICriterio>();
            var criterio2 = new Mock<ICriterio>();
            var criterios = new[] {
                criterio1.Object,
                criterio2.Object
            };
            var validadorDeCriterios = new ValidadorDeCriterios(criterios);

            validadorDeCriterios.Validar(familia);

            criterio1.Verify(c => c.Satisfaz(familia));
            criterio2.Verify(c => c.Satisfaz(familia));
        }

        [Fact]
        public void Deve_vincular_o_criterio_a_familia_caso_o_mesmo_seja_atendido()
        {
            var criterioNaoAtendido = new Mock<ICriterio>();
            var criterioAtendido = new Mock<ICriterio>();
            criterioNaoAtendido.Setup(c => c.Satisfaz(It.IsAny<Familia>())).Returns(false);
            criterioAtendido.Setup(c => c.Satisfaz(It.IsAny<Familia>())).Returns(true);
            var criterios = new[] { criterioNaoAtendido.Object, criterioAtendido.Object };
            var validadorDeCriterios = new ValidadorDeCriterios(criterios);
            var familia = new Mock<Familia>();

            validadorDeCriterios.Validar(familia.Object);

            familia.Verify(f => f.AdicionarCriterioAtendido(criterioAtendido.Object));
        }
    }  
}