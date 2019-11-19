using System.Linq;

namespace Selecao.Dominio
{
    public class CriterioDeRendaDe1501Ate2000Reais : ICriterio
    {
        public string Nome => "Renda de 1501 até 2000 reais";
        public int Pontos => 1;

        public bool Satisfaz(Familia familia)
        {
            var rendaTotal = familia.Pessoas.Sum(p => p.Renda);

            return rendaTotal >= 1501 && rendaTotal <= 2000;
        }
    }
}