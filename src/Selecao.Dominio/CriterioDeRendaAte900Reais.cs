using System.Linq;

namespace Selecao.Dominio
{
    public class CriterioDeRendaAte900Reais : ICriterio
    {
        public string Nome => "Renda até 900 reais";
        public int Pontos => 5;

        public bool Satisfaz(Familia familia)
        {
            var rendaTotal = familia.Pessoas.Sum(p => p.Renda);

            return rendaTotal <= 900;
        }
    }
}