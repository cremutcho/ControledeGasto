using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class SistemaControleGastos
{
    private List<Gasto> gastos = new List<Gasto>();
    private const string caminhoArquivo = "gastos.json";
    public List<string> categorias = new List<string> { "Alimentação", "Transporte", "Lazer", "Educação", "Saúde" };

    public List<string> ObterCategorias()
    {
        return categorias;
    }

    public void AdicionarGasto(string descricao, decimal valor, string categoria, DateTime data)
    {
        Gasto novoGasto = new Gasto(descricao, valor, categoria, data);
        gastos.Add(novoGasto);
        Console.WriteLine("Gasto adicionado com sucesso!");
        SalvarGastos();
    }

    public void ListarGastos()
    {
        Console.WriteLine("\nLista de Gastos:");
        foreach (Gasto g in gastos)
        {
            Console.WriteLine($"Descrição: {g.Descricao}, Valor: R${g.Valor}, Categoria: {g.Categoria}, Data: {g.Data.ToString("dd/MM/yyyy")}");
        }
    }

    public decimal CalcularTotalGastos()
    {
        decimal total = 0;
        foreach (Gasto g in gastos)
        {
            total += g.Valor;
        }
        return total;
    }

    public void SalvarGastos()
    {
        string json = JsonConvert.SerializeObject(gastos, Formatting.Indented);
        File.WriteAllText(caminhoArquivo, json);
        Console.WriteLine("Gastos salvos no arquivo JSON.");
    }

    public void CarregarGastos()
{
    if (File.Exists(caminhoArquivo))
    {
        string json = File.ReadAllText(caminhoArquivo);
        var gastosCarregados = JsonConvert.DeserializeObject<List<Gasto>>(json);

        if (gastosCarregados != null)
        {
            gastos = gastosCarregados;
            Console.WriteLine("Gastos carregados do arquivo JSON.");
        }
        else
        {
            Console.WriteLine("Arquivo JSON vazio ou inválido. Iniciando com uma lista vazia.");
            gastos = new List<Gasto>(); // Inicializa a lista vazia
        }
    }
    else
    {
        Console.WriteLine("Nenhum arquivo de gastos encontrado. Começando com lista vazia.");
        gastos = new List<Gasto>(); // Inicializa a lista vazia
    }
}


    public void ListarCategorias()
    {
        Console.WriteLine("Categorias disponíveis:");
        for (int i = 0; i < categorias.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {categorias[i]}");
        }
        Console.WriteLine($"{categorias.Count + 1}. Adicionar nova categoria");
    }

    public void AdicionarCategoria(string novaCategoria)
    {
        if (!categorias.Contains(novaCategoria))
        {
            categorias.Add(novaCategoria);
            Console.WriteLine($"Categoria '{novaCategoria}' adicionada com sucesso.");
        }
        else
        {
            Console.WriteLine("Essa categoria já existe.");
        }
    }
}

