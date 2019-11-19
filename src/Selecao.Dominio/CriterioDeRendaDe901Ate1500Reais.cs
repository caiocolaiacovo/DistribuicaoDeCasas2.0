using System.Linq;

namespace Selecao.Dominio
{
    public class CriterioDeRendaDe901Ate1500Reais : ICriterio
    {
        public string Nome => "Renda de 901 até 1500 reais";
        public int Pontos => 3;

        public bool Satisfaz(Familia familia)
        {
            var rendaTotal = familia.Pessoas.Sum(p => p.Renda);

            return rendaTotal >= 901 && rendaTotal <= 1500;
        }
    }
}