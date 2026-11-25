using Microsoft.EntityFrameworkCore;
using SmartManut.API.Models;

namespace SmartManut.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Perfil> Perfis { get; set; }
        public DbSet<Utilizador> Utilizadores { get; set; }
        public DbSet<Setor> Setores { get; set; }
        public DbSet<Equipamento> Equipamentos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Peca> Pecas { get; set; }
        public DbSet<OrdemServico> OrdensServico { get; set; }
        public DbSet<HistoricoManutencao> HistoricoManutencoes { get; set; }
    }
}
