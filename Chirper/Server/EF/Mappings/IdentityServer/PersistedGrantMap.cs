using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using IdentityServer4.EntityFramework.Entities;

namespace Chirper.Server.EF.Mappings.IdentityServer
{
    public class PersistedGrantMap: IEntityTypeConfiguration<PersistedGrant>
    {
        public void Configure(EntityTypeBuilder<PersistedGrant> builder)
        {
            builder.ToTable("persisted_grant", "ids");

            builder.HasKey(e => e.Key)
                .HasName("pk_ids_persisted_grant");

            builder.Property(e => e.Key)
                .HasColumnName("grant_key")
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.Type)
                .HasColumnName("grant_type")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.ClientId)
                .HasColumnName("client_id")
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.CreationTime)
                .HasColumnName("creation_time")
                .IsRequired();

            builder.Property(e => e.Data)
                .HasColumnName("data")
                .HasColumnType("nvarchar(max)")
                .IsRequired()
                .HasMaxLength(50000);

            builder.Property(e => e.Expiration)
                .HasColumnName("expiration");

            builder.Property(e => e.SubjectId)
                .HasColumnName("subject_id")
                .HasMaxLength(200);

            builder.HasIndex(e => e.Expiration)
                .HasName("ix_ids_persistent_grant_expiration");

            builder.HasIndex("SubjectId", "ClientId", "Type")
                .HasName("ix_ids_persistent_grant_subject_id");
        }
    }
}
