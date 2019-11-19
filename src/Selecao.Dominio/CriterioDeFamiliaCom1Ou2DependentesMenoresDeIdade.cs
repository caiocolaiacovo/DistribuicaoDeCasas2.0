using System;
using System.Linq;

namespace Selecao.Dominio
{
    public class CriterioDeFamiliaCom1Ou2DependentesMenoresDeIdade : ICriterio
    {
        public string Nome => "Família com 1 ou 2 dependentes menores de idade";
        public int Pontos => 2;

        public bool Satisfaz(Familia familia)
        {
            var ultimaDataValida = DateTime.Today.AddYears(-18);
            var totalDeDependentesMenoresDeIdade = familia.Pessoas
                .Count(p => p.Tipo == TipoPessoa.Dependente && p.DataDeNascimento >= ultimaDataValida);

            return totalDeDependentesMenoresDeIdade > 0 && totalDeDependentesMenoresDeIdade <= 2;
        }
    }
}