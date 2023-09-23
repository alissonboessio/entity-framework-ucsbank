
namespace BancoUCS
{
    internal class Menu
    {
        private string NomeLista;
        private List<string> Opcoes = new List<string>();
        private string? MenuAnterior;

        public Menu(string NomeLista, string? MenuAnterior)
        {
            this.NomeLista = NomeLista;
            this.MenuAnterior = MenuAnterior;
        }

        public void AddOpcoes(List<string> Opcoes)
        {
            foreach (string opc in Opcoes)
            {
                this.Opcoes.Add(opc);
            }
        }

        public void showMenu()
        {
            Console.WriteLine($"---{NomeLista}\n");
            int i = 1;

            foreach (string opc in Opcoes)
            {
                Console.WriteLine($"{i++} --> {opc}");
            }

            if (!String.IsNullOrEmpty(MenuAnterior))
            {
                Console.WriteLine($"{i} --> Voltar para {MenuAnterior}\n");

            }
        }

    }
}
