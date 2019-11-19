using System;
using System.Linq;

namespace Selecao.Dominio
{
    public class CriterioDeIdadeDoPretendenteCom45AnosOuMais : ICriterio
    {
        public string Nome => "Pretendente com 45 anos ou mais";

        public int Pontos => 3;

        public bool Satisfaz(Familia familia)
        {
            var pretendente = familia.Pessoas.First(p => p.Tipo == TipoPessoa.Pretendente);
            var ultimaDataValida = DateTime.Today.AddYears(-45);

            return pretendente.DataDeNascimento <= ultimaDataValida;
        }
    }
}