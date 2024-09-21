using System;

public class Gasto
{
    public string Descricao { get; set; }
    public decimal Valor { get; set; }
    public string Categoria { get; set; }
    public DateTime Data { get; set; }

    public Gasto(string descricao, decimal valor, string categoria, DateTime data)
    {
        Descricao = descricao;
        Valor = valor;
        Categoria = categoria;
        Data = data;
    }
}
