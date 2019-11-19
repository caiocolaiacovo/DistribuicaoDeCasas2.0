using System;
using System.Linq;

namespace Selecao.Dominio
{
    public class CriterioDeIdadeDoPretendenteComMenosDe30Anos : ICriterio
    {
        public string Nome => "Pretendente com menos de 30 anos";

        public int Pontos => 1;

        public bool Satisfaz(Familia familia)
        {
            var pretendente = familia.Pessoas.Where(p => p.Tipo == TipoPessoa.Pretendente).First();
            var ultimaDataValida = DateTime.Today.AddYears(-30);

            return pretendente.DataDeNascimento > ultimaDataValida;
        }
    }
}