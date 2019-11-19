using System;

namespace Selecao.Dominio
{
    public class Pessoa
    {
        public string Nome { get; set; }
        public TipoPessoa Tipo { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public decimal Renda { get; set; }
    }
}