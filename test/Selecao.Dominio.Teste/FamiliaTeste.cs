using System.Linq;
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

        [Fact]
        public void Deve_adicionar_uma_pessoa_a_familia()
        {
            var familia = FluentBuilder<Familia>.New().Build();
            var pessoaEsperada = FluentBuilder<Pessoa>.New()
                .With(p => p.Nome, "Maria da Luz")
                .With(p => p.Renda, 1000)
                .With(p => p.Tipo, TipoPessoa.Pretendente)
                .Build();

            familia.AdicionarPessoa(pessoaEsperada);

            var pessoaEncontrada = familia.Pessoas.Single();
            pessoaEsperada.ToExpectedObject().ShouldMatch(pessoaEncontrada);
        }
    }
}