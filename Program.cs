using System.Globalization;
using ClientLab;

SistemaDAO sys = new SistemaDAO();

bool continuar = true;

while(continuar)
{
    Console.Clear();
    Console.WriteLine("========================================");
    Console.WriteLine("    Sistema de Cadastro de Clientes     ");
    Console.WriteLine("========================================");
    Console.WriteLine("1. Cadastrar Novo Cliente");
    Console.WriteLine("2. Registrar Venda");
    Console.WriteLine("3. Consultar Vendas");
    Console.WriteLine("4. Gerar Relatório CSV");
    Console.WriteLine("5. Sair");
    Console.WriteLine("========================================");
    Console.Write("Escolha uma opção: ");
    int opcao = int.Parse(Console.ReadLine());

    {
    switch (opcao)
        case 1:
            string nome;
            string endereco;

            Console.WriteLine("\n-- Cadastro de Cliente --");
            Console.Write("Pessoa Física (f) ou Jurídica (j)?");
            string escolha_cad = Console.ReadLine();

            switch (escolha_cad)
                case "f":
                {
                    Console.Write("Nome: ");
                    nome = Console.ReadLine();
                    Console.Write("Endereço: ");
                    endereco = Console.ReadLine();
                    Console.Write("CPF: ");
                    string cpf = Console.ReadLine();
                    Console.Write("RG: ");
                    string rg = Console.ReadLine();
                    Pessoa_Fisica clientef = new Pessoa_Fisica(nome, endereco, cpf, rg);

                    sys.InserirPF(clientef);
                    break;
                }
                case "j":
                {
                    Console.Write("Nome: ");
                    nome = Console.ReadLine();
                    Console.Write("Endereço: ");
                    endereco = Console.ReadLine();
                    Console.Write("CNPJ: ")
                    string CNPJ = Console.ReadLine();
                    Console.Write("IE: ")
                    string IE = Console.ReadLine();
                    Pessoa_Juridica clientej = new Pessoa_Juridica(nome, endereco, CNPJ, IE);

                    sys.InserirPJ(clientej);
                    break;
                }
        case 2:
            Console.WriteLine("\nRegistrar Venda para:");
            Console.WriteLine("1. Pessoa Física");
            Console.WriteLine("2. Pessoa Jurídica");
            Console.Write("Escolha o tipo de cliente: ");
            if (!int.TryParse(Console.ReadLine(), out int tipoCliente))
            {
                Console.WriteLine("\nEntrada inválida! Por favor, digite apenas números.");
                Console.WriteLine("Pressione qualquer tecla para tentar novamente...");
                Console.ReadKey();
                continue; 
            }
            if (tipoCliente != 1 && tipoCliente != 2)
            {
                Console.WriteLine("\nOpção inválida! Por favor, escolha 1 para Pessoa Física ou 2 para Pessoa Jurídica.");
                Console.WriteLine("Pressione qualquer tecla para tentar novamente...");
                Console.ReadKey();
                continue; 
            }
            Console.Write("Digite o ID do cliente: ");
            if (!int.TryParse(Console.ReadLine(), out int idCliente))
            {
                Console.WriteLine("\nEntrada inválida! Por favor, digite apenas números.");
                Console.WriteLine("Pressione qualquer tecla para tentar novamente...");
                Console.ReadKey();
                continue; 
            }
            string ID
            Console.Write("Digite o valor da compra: ");
            if (!double.TryParse(Console.ReadLine(), out double valorCompra))
            {
                Console.WriteLine("\nEntrada inválida! Por favor, digite um valor numérico.");
                Console.WriteLine("Pressione qualquer tecla para tentar novamente...");
                Console.ReadKey();
                continue; 
            }
            double valor = double.Parse(Console.ReadLine());
            sys.RegistrarVenda(tipoCliente, idCliente, valorCompra);
            break;
        case 3:
            ConsultarVendas(sistemaDAO);
            break;
        case 4:
            GerarRelatorio(sistemaDAO);
            break;
        case 5:
            continuar = false;
            Console.WriteLine("Encerrando o programa...");
            break;
        default:
            Console.WriteLine("\nOpção inválida! Por favor, escolha uma opção entre 1 e 6.");
            Console.WriteLine("Pressione qualquer tecla para tentar novamente...");
            Console.ReadKey();
            break;
    }
}

if (continuar)
{
    // Dá uma pausa na tela para o aluno poder ler os resultados antes do Console.Clear() agir
    Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
    Console.ReadKey();
}