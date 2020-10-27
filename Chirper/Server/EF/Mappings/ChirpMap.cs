using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Chirper.Server.DomainModel;

namespace Chirper.Server.EF.Mappings
{
    public class ChirpMap: IEntityTypeConfiguration<Chirp>
    {
        public void Configure(EntityTypeBuilder<Chirp> builder)
        {
            builder.ToTable("chirp");

            builder.HasKey(e => e.Id)
                .HasName("pk_chirp");

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .UseHiLo("seq_chirp");

            builder.Property(e => e.UserId)
                .HasColumnName("user_id");

            builder.Property(e => e.ChirpType)
                .HasColumnName("chirp_type_id");

            builder.Property(e => e.ChirpTimeUtc)
                .HasColumnName("chirp_time_utc")
                .HasColumnType("datetime");

            builder.Property(e => e.Contents)
                .HasColumnName("contents")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.OriginalChirpId)
                .HasColumnName("original_chirp_id");

            builder.Property(e => e.Score)
                .HasColumnName("score");

            builder.Property(e => e.IsTimelineSyncStarted)
                .HasColumnName("is_timeline_sync_started")
                .HasDefaultValue(false);

            builder.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .HasConstraintName("fk_chirp_user_id");

            builder.HasIndex(e => e.UserId)
                .HasName("ix_chirp_user_id");

            builder.HasIndex(e => e.OriginalChirpId)
                .HasName("ix_chirp_original_chirp_id");
        }
    }
}
