using System.Globalization;
using ClientLab;

SistemaDAO sys = new SistemaDAO();

bool continuar = true;

while (continuar)
{
    Console.Clear();

    Console.WriteLine("========================================");
    Console.WriteLine("           Sistema ClientLab           ");
    Console.WriteLine("========================================");
    Console.WriteLine("1. Cadastrar Novo Cliente");
    Console.WriteLine("2. Registrar Venda");
    Console.WriteLine("3. Consultar Vendas");
    Console.WriteLine("4. Gerar Relatório CSV");
    Console.WriteLine("5. Sair");
    Console.WriteLine("========================================");
    Console.Write("Escolha uma opção: ");

    int op = int.Parse(Console.ReadLine());

    switch (op)
    {
        case 1:
            Console.Write("Pessoa Física (f) ou Jurídica (j): ");
            string tipo = Console.ReadLine().ToLower();

            Console.Write("\nNome: ");
            string nome = Console.ReadLine();

            Console.Write("Endereço: ");
            string end = Console.ReadLine();

            if (tipo == "f")
            {
                Console.Write("CPF: ");
                string cpf = Console.ReadLine();

                Console.Write("RG: ");
                string rg = Console.ReadLine();

                sys.InserirPF(new Pessoa_Fisica(nome, end, cpf, rg));
            }
            else if (tipo == "j")
            {
                Console.Write("CNPJ: ");
                string cnpj = Console.ReadLine();

                Console.Write("IE: ");
                string ie = Console.ReadLine();

                sys.InserirPJ(new Pessoa_Juridica(nome, end, cnpj, ie));
            }
            else
            {
                Console.WriteLine("\n[ERRO] Tipo inválido!");
            }

            break;

        case 2:
            Console.WriteLine("--- Registro de Venda ---");

            Console.Write("Pessoa Física (f) ou Jurídica (j): ");
            string tipoVenda = Console.ReadLine().ToLower();

            int id = 0;
            double valor = 0;

            while (id <= 0)
            {
                Console.Write("\nID do Cliente: ");
                id = int.Parse(Console.ReadLine());

                if (id <= 0)
                    Console.WriteLine("[ERRO] ID inválido!");
            }

            while (valor <= 0)
            {
                Console.Write("Valor da compra: ");
                valor = double.Parse(Console.ReadLine());

                if (valor <= 0)
                    Console.WriteLine("[ERRO] Valor inválido!");
            }

            if (tipoVenda == "f")
            {
                var pf = new Pessoa_Fisica { ID = id };
                sys.RegistrarVenda(pf, valor);
            }
            else if (tipoVenda == "j")
            {
                var pj = new Pessoa_Juridica { ID = id };
                sys.RegistrarVenda(pj, valor);
            }
            else
            {
                Console.WriteLine("\n[ERRO] Tipo inválido!");
            }

            break;

        case 3:
            sys.ListarVendas();
            break;

        case 4:
            Console.WriteLine("--- Relatório CSV ---");
            sys.GerarRelatorioCSV();
            break;

        case 5:
            Console.WriteLine("\nEncerrando sistema...");
            continuar = false;
            break;

        default:
            Console.WriteLine("\n[ERRO] Opção inválida!");
            break;
    }
}

if (continuar)
{
    Console.WriteLine("\n----------------------------------------");
    Console.WriteLine("Pressione qualquer tecla para continuar...");
    Console.ReadKey();
}

Console.WriteLine("\nSistema finalizado.");