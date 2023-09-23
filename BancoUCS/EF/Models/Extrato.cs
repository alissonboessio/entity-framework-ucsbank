using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BancoUCS.EF.Models
{
    internal class Extrato
    {

        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Conta_bancaria_id")]
        public int Conta_bancaria_id { get; set; }
        public string Descricao { get; set; }
        [Required]
        public decimal Valor { get; set; }
        [Required]
        public DateTime Data { get; set; }

        public Extrato(int conta_bancaria_id)
        {
            Conta_bancaria_id = conta_bancaria_id;
        }

        public Extrato(int conta_bancaria_id, decimal valor, DateTime data, string? descricao)
        {
            Conta_bancaria_id = conta_bancaria_id;
            Descricao = descricao;
            Valor = valor;
            Data = data;
        }
    }
}
