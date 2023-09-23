using BancoUCS;
using BancoUCS.EF.Context;
using BancoUCS.EF.Models;

bool sair = false;
Menu menuPrincipal = new Menu("Menu", null);
Menu menuCliente = new Menu("Clientes", "Menu");
Menu menuContas = new Menu("Contas Bancarias", "Menu");

menuPrincipal.AddOpcoes(new List<string> {
    "Clientes",
    "Contas Bancarias",
    "Sair"
});

menuCliente.AddOpcoes(new List<string> {
    "Listar todos",
    "Cadastrar",
    "Atualizar nome",
    "Excluir"
});

menuContas.AddOpcoes(new List<string> {
    "Listar todas",
    "Cadastrar Nova",
    "Depositar",
    "Sacar",
    "Extrato",
});

string opc = "";

do
{
    menuPrincipal.showMenu();
    opc = Console.ReadLine();

    switch (opc)
    {
        case "1":
            menuCliente.showMenu();
            opc = Console.ReadLine();

            switch (opc)
            {
                case "1":
                    MeuBancoContext.listClientes();

                    break;
                case "2":
                    Console.WriteLine("Digite o nome do cliente: ");
                    Cliente cli = new Cliente(Console.ReadLine());

                    MeuBancoContext.createCliente(cli);

                    break;
                case "3":
                    Console.WriteLine("Digite o codigo do cliente para alterar: ");
                    int cod = 0;
                    Int32.TryParse(Console.ReadLine(), out cod);

                    Console.WriteLine("\nDigite o novo nome: ");
                    string nome = Console.ReadLine();

                    MeuBancoContext.updateNomeCliente(cod, nome);

                    break;
            }
            break;
        case "2":
            menuContas.showMenu();
            opc = Console.ReadLine();

            switch (opc) {
                case "1":
                    MeuBancoContext.listContasBancarias();

                    break;
                case "2":
                    Console.WriteLine("Digite o codigo do cliente para adicionar a conta: ");
                    int cod = 0;
                    Decimal saldo = 0;
                    Int32.TryParse(Console.ReadLine(), out cod);
                    Console.WriteLine("Digite saldo inicial para a conta: ");
                    Decimal.TryParse(Console.ReadLine(), out saldo);

                    MeuBancoContext.createContaBancaria(cod, saldo);

                    break;
                case "3":
                    Console.WriteLine("Digite o codigo da conta bancaria: ");
                    int didCB;
                    Int32.TryParse(Console.ReadLine(), out didCB);
                    Console.WriteLine("Digite o valor para deposito: ");
                    decimal valorD = decimal.Parse(Console.ReadLine());
                    Console.WriteLine("Descricao caso houver: ");
                    MeuBancoContext.deposit(didCB, valorD, Console.ReadLine());

                    break;
                case "4":
                    Console.WriteLine("Digite o codigo da conta bancaria: ");
                    int sidCB;                    
                    Int32.TryParse(Console.ReadLine(), out sidCB);
                    Console.WriteLine("Digite o valor para saque: ");
                    decimal valorS = decimal.Parse(Console.ReadLine());
                    Console.WriteLine("Descricao caso houver: ");
                    MeuBancoContext.withdraw(sidCB, valorS, Console.ReadLine());

                    break;
                case "5":
                    Console.WriteLine("Digite o codigo da conta bancaria: ");
                    MeuBancoContext.listExtrato(Int32.Parse(Console.ReadLine()));

                    break;
            }

            break;
        case "3":
            sair = true;
            break;
        default:
            break;
    }

} while (!sair);