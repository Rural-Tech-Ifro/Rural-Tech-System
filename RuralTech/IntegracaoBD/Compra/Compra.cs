using System;

public class Compra
{
    public int Id { get; set; }
    public string Codigo { get; set; }
    public string Funcionario { get; set; }
    public string FormaPagamento { get; set; }
    public string Fornecedor { get; set; }
    public DateTime DataCompra { get; set; }
    public DateTime DataPagamento { get; set; }
    public int QuantidadeParcelas { get; set; }
    public string Produto { get; set; }
}