using System.ComponentModel.DataAnnotations;

namespace BancoUCS.EF.Models
{
    internal class Cliente
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }

        public Cliente()
        {
        }
        public Cliente(string nome)
        {
            Nome = nome;
        }
        public Cliente(int id)
        {
            Id = id;
        }
        public Cliente(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
        public Cliente(Cliente cli)
        {
            Id = cli.Id;
            Nome = cli.Nome;
        }
    }
    

}
