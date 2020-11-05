using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Chirper.Server.DomainModel;

namespace Chirper.Server.EF.Mappings
{
    public class ChirpUserMap: IEntityTypeConfiguration<ChirpUser>
    {
        public void Configure(EntityTypeBuilder<ChirpUser> builder)
        {
            builder.ToTable("chirp_user");

            builder.HasKey(e => e.Id)
                .HasName("pk_chirp_user");

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .UseHiLo("seq_chirp_user");

            builder.Property(e => e.Username)
                .HasColumnName("username")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(e => e.UsernameNormalized)
                .HasColumnName("username_normalized")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(e => e.SecurityStamp)
                .HasColumnName("security_stamp")
                .HasMaxLength(255);

            builder.Property(e => e.Password)
                .HasColumnName("password")
                .HasMaxLength(255)
                .IsRequired();

            builder.HasIndex(e => e.UsernameNormalized)
                .IsUnique()
                .HasName("uq_chirp_user_username_normalized");
        }
    }
}
