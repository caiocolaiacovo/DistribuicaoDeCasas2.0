using System;

namespace Selecao.Aplicacao
{
    public class ExcecaoDeAplicacao : Exception
    {
        public ExcecaoDeAplicacao(string mensagem) : base(mensagem) { }
    }
}