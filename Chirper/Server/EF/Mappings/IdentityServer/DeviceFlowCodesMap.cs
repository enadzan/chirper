using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using IdentityServer4.EntityFramework.Entities;

namespace Chirper.Server.EF.Mappings.IdentityServer
{
    public class DeviceFlowCodesMap: IEntityTypeConfiguration<DeviceFlowCodes>
    {
        public void Configure(EntityTypeBuilder<DeviceFlowCodes> builder)
        {
            builder.ToTable("device_code", "ids");

            builder.HasKey(e => e.UserCode)
                .HasName("pk_ids_device_code");

            builder.Property(e => e.UserCode)
                .HasColumnName("user_code")
                .IsRequired()
                .HasMaxLength(200);

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

            builder.Property(e => e.DeviceCode)
                .HasColumnName("device_code")
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.Expiration)
                .HasColumnName("expiration");

            builder.Property(e => e.SubjectId)
                .HasColumnName("subject_id")
                .HasMaxLength(200);

            builder.HasIndex(e => e.DeviceCode)
                .HasName("uq_ids_device_code_device_code")
                .IsUnique();

            builder.HasIndex(e => e.Expiration)
                .HasName("ix_ids_device_code_expiration");
        }
    }
}
