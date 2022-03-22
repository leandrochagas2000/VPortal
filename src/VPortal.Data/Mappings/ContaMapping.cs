using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VPortal.Business.Models;

namespace VPortal.Data.Mappings
{
    public class ContaMapping : IEntityTypeConfiguration<Conta>
    {
        public void Configure(EntityTypeBuilder<Conta> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(x => x.Documento)
                .IsRequired()
                .HasColumnType("varchar(14)");

            // 1 : 1 => Conta : Endereco
            builder.HasOne(c => c.Endereco)
                .WithOne(e => e.Conta);
                //.HasForeignKey<Endereco>(c => c.ContaId);

            // 1 : N => Conta : Produtos
            builder.HasMany(c => c.Produtos)
                .WithOne(x => x.Conta)
                .HasForeignKey(x => x.ContaId);

            builder.ToTable("Contas");
        }
    }
}
