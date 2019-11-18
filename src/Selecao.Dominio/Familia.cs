using System;
using System.Collections.Generic;
using System.Linq;

namespace Selecao.Dominio 
{
    public class Familia 
    {
        private readonly ICollection<Pessoa> _pessoas;
        private readonly ICollection<ICriterio> _criteriosAtendidos;
        public IEnumerable<Pessoa> Pessoas => _pessoas;
        public IEnumerable<ICriterio> Criterios => _criteriosAtendidos;

        public Familia () 
        {
            _pessoas = new List<Pessoa>();
            _criteriosAtendidos = new List<ICriterio>();
        }

        public virtual void AdicionarCriterioAtendido(ICriterio criterio)
        {
            _criteriosAtendidos.Add(criterio);
        }
    }
}