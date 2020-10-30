using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Chirper.Server.DomainModel;

namespace Chirper.Server.EF.Mappings
{
    public class HashTagMap: IEntityTypeConfiguration<HashTag>
    {
        public void Configure(EntityTypeBuilder<HashTag> builder)
        {
            builder.ToTable("hashtag");

            builder.HasKey("Tag", "TimeUtc", "ChirpId")
                .HasName("pk_hashtag");

            builder.Property(e => e.Tag)
                .HasColumnName("hashtag")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.TimeUtc)
                .HasColumnName("time_utc");

            builder.Property(e => e.ChirpId)
                .HasColumnName("chirp_id");

            builder.HasOne(e => e.Chirp)
                .WithMany()
                .HasForeignKey(e => e.ChirpId)
                .HasConstraintName("fk_hashtag_chirp_id");

            builder.HasIndex(e => e.TimeUtc)
                .HasName("ix_hashtag_time_utc")
                .IncludeProperties("Tag", "ChirpId");
        }
    }
}
