using System;
public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public double PrecoCusto { get; set; }
    public int Quantidade { get; set; }
    public DateTime DataVencimento { get; set; }
    public string UnidadeEntrada { get; set; }
    public string UnidadeSaida { get; set; }
    public double PrecoVenda { get; set; }
    public string Observacao { get; set; }
}