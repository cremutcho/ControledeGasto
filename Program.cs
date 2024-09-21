using System;


class Program
{
    static void Main(string[] args)
    {
        SistemaControleGastos sistema = new SistemaControleGastos();
        sistema.CarregarGastos(); 
        int opcao;

        do
        {
            Console.WriteLine("\n--- Sistema de Controle de Gastos ---");
            Console.WriteLine("1. Adicionar Gasto");
            Console.WriteLine("2. Listar Gastos");
            Console.WriteLine("3. Calcular Total de Gastos");
            Console.WriteLine("0. Sair");
            Console.Write("Escolha uma opção: ");
            opcao = Convert.ToInt32(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    AdicionarGastoComCategoria(sistema);
                    break;
                case 2:
                    sistema.ListarGastos();
                    break;
                case 3:
                    decimal total = sistema.CalcularTotalGastos();
                    Console.WriteLine($"Total Gasto: R${total}");
                    break;
                case 0:
                    Console.WriteLine("Saindo...");
                    break;
                default:
                    Console.WriteLine("Opção inválida, tente novamente.");
                    break;
            }
        } while (opcao != 0);
    }

    static void AdicionarGastoComCategoria(SistemaControleGastos sistema)
    {
        string descricao;
        decimal valor;
        string categoria;
        DateTime data; 

        do
        {
            Console.Write("Descrição do Gasto: ");
            descricao = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(descricao))
            {
                Console.WriteLine("Erro: A descrição não pode estar vazia.");
            }
        } while (string.IsNullOrWhiteSpace(descricao));

        do
        {
            Console.Write("Valor do Gasto: R$");
            string inputValor = Console.ReadLine();
            bool isValorValido = decimal.TryParse(inputValor, out valor);

            if (!isValorValido || valor <= 0)
            {
                Console.WriteLine("Erro: O valor deve ser um número positivo.");
            }
        } while (valor <= 0);

        do
        {
            var categorias = sistema.ObterCategorias();
            sistema.ListarCategorias();
            Console.Write("Escolha uma categoria: ");
            string inputCategoria = Console.ReadLine();
            int opcaoCategoria;
            if (int.TryParse(inputCategoria, out opcaoCategoria) && opcaoCategoria > 0 && opcaoCategoria <= categorias.Count + 1)
            {
                if (opcaoCategoria == categorias.Count + 1)
                {
                    Console.Write("Digite o nome da nova categoria: ");
                    string novaCategoria = Console.ReadLine();
                    sistema.AdicionarCategoria(novaCategoria);
                    categoria = novaCategoria;
                }
                else
                {
                    categoria = categorias[opcaoCategoria - 1];
                }
            }
            else
            {
                Console.WriteLine("Opção inválida. Tente novamente.");
                categoria = null;
            }
        } while (categoria == null);

        do
        {
            Console.Write("Data do Gasto (dd/MM/yyyy) ou deixe em branco para usar a data atual: ");
            string inputData = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(inputData))
            {
                data = DateTime.Now; 
                break;
            }
            else if (DateTime.TryParse(inputData, out data))
            {
                break;
            }
            else
            {
                Console.WriteLine("Erro: Data inválida. Tente no formato dd/MM/yyyy.");
            }
        } while (true);

        sistema.AdicionarGasto(descricao, valor, categoria, data);
    }
}
