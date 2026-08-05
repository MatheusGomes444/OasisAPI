using Microsoft.EntityFrameworkCore;
using OasisApi.Domain.Entities;

namespace OasisApi.Infrastructure.Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
         public DbSet<User> Users { get; set; }
        public DbSet<Morador> Moradores { get; set; }
        public DbSet<Alojamento> Alojamentos { get; set; }
        public DbSet<FilaDeEspera> FilasDeEspera { get; set; } // Adicionado

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Uuid).IsUnique();

            // Configuração de Alojamento
            modelBuilder.Entity<Alojamento>().ToTable("Alojamentos");
            modelBuilder.Entity<Alojamento>().HasKey(a => a.Id);
            modelBuilder.Entity<Alojamento>().HasIndex(a => a.Uuid).IsUnique();
            modelBuilder.Entity<Alojamento>().Property(a => a.Nome).IsRequired(false).HasMaxLength(200);
            modelBuilder.Entity<Alojamento>().Property(a => a.Equipe).HasMaxLength(100);
            modelBuilder.Entity<Alojamento>().Property(a => a.Telefone);
            modelBuilder.Entity<Alojamento>().Property(a => a.Email).HasMaxLength(100);
            modelBuilder.Entity<Alojamento>().Property(a => a.CapacidadeMaxima);
            modelBuilder.Entity<Alojamento>().Property(a => a.Pet).IsRequired(false);
            modelBuilder.Entity<Alojamento>().Property(a => a.Sexo).IsRequired(false);
            modelBuilder.Entity<Alojamento>().Property(a => a.Pertences).IsRequired(false);
            modelBuilder.Entity<Alojamento>().Property(a => a.Refeicoes).IsRequired(false);

            // Configuração de Morador
            modelBuilder.Entity<Morador>().ToTable("Moradores");
            modelBuilder.Entity<Morador>().HasKey(m => m.Id);
            modelBuilder.Entity<Morador>().HasIndex(m => m.Uuid).IsUnique();
            modelBuilder.Entity<Morador>().Property(m => m.Nome).IsRequired(false).HasMaxLength(200);
            modelBuilder.Entity<Morador>().Property(m => m.CPF).IsRequired(false).HasMaxLength(11);
            modelBuilder.Entity<Morador>().Property(m => m.RG).IsRequired(false).HasMaxLength(9);
            modelBuilder.Entity<Morador>().Property(m => m.Telefone).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Morador>().Property(m => m.Endereco).IsRequired(false).HasMaxLength(200);
            modelBuilder.Entity<Morador>().Property(m => m.Idade).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Morador>().Property(m => m.Observacoes).IsRequired(false);

            // Configurar relacionamento entre Alojamento e Moradores
            modelBuilder.Entity<Morador>()
                .HasOne(m => m.Alojamento)
                .WithMany(a => a.Moradores)
                .HasForeignKey(m => m.AlojamentoId);

            // Trilha de auditoria de Morador (quem criou/atualizou)
            modelBuilder.Entity<Morador>()
                .HasOne(m => m.CreatedByUser)
                .WithMany()
                .HasForeignKey(m => m.CreatedByUserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Morador>()
                .HasOne(m => m.UpdatedByUser)
                .WithMany()
                .HasForeignKey(m => m.UpdatedByUserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Trilha de auditoria de Alojamento (quem criou/atualizou)
            modelBuilder.Entity<Alojamento>()
                .HasOne(a => a.CreatedByUser)
                .WithMany()
                .HasForeignKey(a => a.CreatedByUserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Alojamento>()
                .HasOne(a => a.UpdatedByUser)
                .WithMany()
                .HasForeignKey(a => a.UpdatedByUserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configuração de FilaDeEspera
            modelBuilder.Entity<FilaDeEspera>().ToTable("FilasDeEspera");
            modelBuilder.Entity<FilaDeEspera>().HasKey(f => f.Id);
            modelBuilder.Entity<FilaDeEspera>().HasIndex(f => f.Uuid).IsUnique();

            // Relacionamento com Morador
            modelBuilder.Entity<FilaDeEspera>()
                .HasOne(f => f.Morador)
                .WithMany()
                .HasForeignKey(f => f.MoradorId)
                .OnDelete(DeleteBehavior.NoAction); // Evita exclusão em cascata

            // Relacionamento com Alojamento
            modelBuilder.Entity<FilaDeEspera>()
                .HasOne(f => f.Alojamento)
                .WithMany()
                .HasForeignKey(f => f.AlojamentoId)
                .OnDelete(DeleteBehavior.NoAction); // Evita exclusão em cascata

            // Dados iniciais para Alojamentos
            var seedCreatedAt = new DateTime(2024, 12, 5, 0, 0, 0, DateTimeKind.Utc);

            modelBuilder.Entity<Alojamento>().HasData(
                new Alojamento
                {
                    Id = 1,
                    Uuid = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Nome = "Albergue Vila Maria",
                    Equipe = "Equipe A",
                    Telefone = 123456789,
                    Email = "exemplo1@dominio.com",
                    CapacidadeMaxima = 10,
                    Pet = "Apenas cães",
                    Sexo = "Masculino",
                    Pertences = "Roupas e sapatos",
                    Refeicoes = "Café e janta",
                    CreatedAt = seedCreatedAt
                }
            );

            // Dados iniciais para Moradores
            modelBuilder.Entity<Morador>().HasData(
                new Morador
                {
                    Id = 1,
                    Uuid = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Nome = "Pedro",
                    CPF = "12345678900",
                    RG = "603456789",
                    Telefone = 912345678,
                    Endereco = "Rua Alcântara, 113",
                    Idade = 18,
                    Datanascimento = 0,
                    Nacionalidade = "Brasileiro",
                    Observacoes = "Sem observações",
                    AlojamentoId = 1,
                    Ativo = true,
                    CreatedAt = seedCreatedAt
                }
            );
        }
    }
}
