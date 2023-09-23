using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BancoUCS.EF.Models
{
    internal class ContaBancaria
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Cliente_id")]
        public int Cliente_id { get; set; }
        [Required]
        public decimal Saldo { get; set; }

        public ContaBancaria()
        {
            
        }

        public ContaBancaria(int clienteId, decimal saldo)
        {
            Cliente_id = clienteId;
            Saldo = saldo;
        }
    }
}
