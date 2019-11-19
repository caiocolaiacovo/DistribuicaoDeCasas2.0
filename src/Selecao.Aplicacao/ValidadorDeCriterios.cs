using System.Collections.Generic;
using Selecao.Dominio;

namespace Selecao.Aplicacao
{
    public class ValidadorDeCriterios
    {
        private readonly IEnumerable<ICriterio> _criterios;

        public ValidadorDeCriterios(IEnumerable<ICriterio> criterios)
        {
            _criterios = criterios;
        }

        public void Validar(Familia familia)
        {
            if (familia == null)
                throw new ExcecaoDeAplicacao("É obrigatório informar uma família para validar critérios");

            foreach (var criterio in _criterios)
            {
                if (criterio.Satisfaz(familia))
                    familia.AdicionarCriterioAtendido(criterio);
            }
        }
    }
}