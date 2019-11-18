namespace Selecao.Dominio
{
    public interface ICriterio
    {
        string Nome { get; }
        int Pontos { get; }
        bool Satisfaz(Familia familia);
    }
}