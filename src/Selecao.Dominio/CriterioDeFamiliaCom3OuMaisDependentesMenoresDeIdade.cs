using System;
using System.Linq;

namespace Selecao.Dominio
{
    public class CriterioDeFamiliaCom3OuMaisDependentesMenoresDeIdade : ICriterio
    {
        public string Nome => "Família com 3 ou mais dependentes menores de idade";
        public int Pontos => 3;

        public bool Satisfaz(Familia familia)
        {
            var ultimaDataValida = DateTime.Today.AddYears(-18);
            var totalDeDependentesMenoresDeIdade = familia.Pessoas
                .Count(p => p.Tipo == TipoPessoa.Dependente && p.DataDeNascimento >= ultimaDataValida);

            return totalDeDependentesMenoresDeIdade >= 3;
        }
    }
}