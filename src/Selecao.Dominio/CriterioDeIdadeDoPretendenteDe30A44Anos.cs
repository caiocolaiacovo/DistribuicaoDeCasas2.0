using System;
using System.Linq;

namespace Selecao.Dominio
{
    public class CriterioDeIdadeDoPretendenteDe30A44Anos : ICriterio
    {
        public string Nome => "Pretendente com idade entre 30 e 44 anos";

        public int Pontos => 2;

        public bool Satisfaz(Familia familia)
        {
            var pretendente = familia.Pessoas.First(p => p.Tipo == TipoPessoa.Pretendente);
            var dataLimiteInicial = DateTime.Today.AddYears(-45);
            var dataLimiteFinal = DateTime.Today.AddYears(-30);

            return pretendente.DataDeNascimento > dataLimiteInicial
                   && pretendente.DataDeNascimento <= dataLimiteFinal;
        }
    }
}