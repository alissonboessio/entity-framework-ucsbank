using BancoUCS.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace BancoUCS.EF.Context
{
    internal class MeuBancoContext : DbContext
    {
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<ContaBancaria> Conta_bancaria { get; set;}
        public DbSet<Extrato> Extrato { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=localhost;User Id=sa;Password=blog_6109;Database=MeuDinheiro;TrustServerCertificate=True";

            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            base.OnModelCreating(modelBuilder);
        }
        public static void createCliente(Cliente cliente)
        {
            MeuBancoContext ctx = new();
            ctx.Cliente.Add(cliente);
            ctx.SaveChanges();
        }
        public static void listClientes()
        {
            MeuBancoContext ctx = new();

            var clientes = ctx.Cliente.OrderBy(x => x.Nome);

            foreach (var item in clientes)
            {
                Console.WriteLine($"Codigo: {item.Id} -> {item.Nome}");
            }
        }
        public static void updateNomeCliente(int cod, string newNome)
        {
            MeuBancoContext ctx = new();

            var result = ctx.Cliente.SingleOrDefault(x => x.Id == cod);

            if (result != null)
            {
                result.Nome = newNome;
                ctx.SaveChanges();
            }

        }
        public static void createContaBancaria(int Id, decimal saldoInicial)
        {
            MeuBancoContext ctx = new();

            var result = ctx.Cliente.SingleOrDefault(x => x.Id == Id);

            if (result != null)
            {
                ContaBancaria cb = new ContaBancaria(Id, saldoInicial);

                ctx.Conta_bancaria.Add(cb);
                ctx.SaveChanges();

                if (saldoInicial > 0)
                {
                    Extrato ex = new Extrato(cb.Id, saldoInicial, DateTime.UtcNow, "Saldo inicial");
                    ctx.Extrato.Add(ex);
                    ctx.SaveChanges();
                }               

            }
        }
        public static void listContasBancarias()
        {
            MeuBancoContext ctx = new();

            var result = ctx.Conta_bancaria
                .Join(
                    ctx.Cliente, 
                    cb => cb.Cliente_id,
                    c => c.Id,
                    (cb, c) => new 
                    {
                        Id = cb.Id,
                        Saldo = cb.Saldo,
                        Nome = c.Nome
                    });

            foreach (var item in result)
            {
                Console.WriteLine($"Codigo: {item.Id} -> Cliente: {item.Nome}, saldo: {item.Saldo}");
            }
        }
        public static void listExtrato(int IdCB)
        {
            MeuBancoContext ctx = new();

            var result = ctx.Conta_bancaria
                .Join(
                    ctx.Cliente, 
                    cb => cb.Cliente_id,
                    c => c.Id,
                    (cb, c) => new 
                    {
                        Id = cb.Id,
                        Saldo = cb.Saldo,
                        Nome = c.Nome
                    })
                .Where(x => x.Id == IdCB);

            foreach (var item in result)
            {
                Console.WriteLine($"Codigo: {item.Id} -> Cliente: {item.Nome}, saldo: {item.Saldo}");

            }

            var extrato = ctx.Extrato
                .Where(x => x.Conta_bancaria_id == IdCB)
                .OrderBy(x => x.Data);

            foreach (var item in extrato)
            {
                Console.WriteLine($"Data: {item.Data}, {item.Valor}. Descricao: {item.Descricao}");
            }

        }
        public static void withdraw(int IdCB, decimal amount, string? descricao)
        {
            if (amount > 0)
            {
                MeuBancoContext ctx = new();

                var result = ctx.Conta_bancaria.SingleOrDefault(x => x.Id == IdCB);

                if (result != null)
                {
                    if((result.Saldo - amount) >= 0)
                    {
                        result.Saldo -= amount;
                        ctx.SaveChanges();

                        Extrato ext = new Extrato(IdCB, amount, DateTime.UtcNow, ($"{descricao} - saque"));

                        ctx.Extrato.Add(ext);
                        ctx.SaveChanges();
                    }
                }
            }

        }
        public static void deposit(int IdCB, decimal amount, string? descricao)
        {
            if (amount > 0)
            {
                MeuBancoContext ctx = new();

                var result = ctx.Conta_bancaria.SingleOrDefault(x => x.Id == IdCB);

                if (result != null)
                {
                    result.Saldo += amount;
                    ctx.SaveChanges();

                    Extrato ext = new Extrato(IdCB, amount, DateTime.UtcNow, ($"{descricao} - deposito"));

                    ctx.Extrato.Add(ext);
                    ctx.SaveChanges();
                }
            }
        }

    }




}
