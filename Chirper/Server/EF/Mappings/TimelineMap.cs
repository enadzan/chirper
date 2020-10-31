using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Chirper.Server.DomainModel;

namespace Chirper.Server.EF.Mappings
{
    public class TimelineMap: IEntityTypeConfiguration<Timeline>
    {
        public void Configure(EntityTypeBuilder<Timeline> builder)
        {
            builder.ToTable("timeline");

            builder.HasKey("UserId", "TimeUtc", "ChirpId")
                .HasName("pk_timeline");

            builder.Property(e => e.UserId)
                .HasColumnName("user_id");

            builder.Property(e => e.TimeUtc)
                .HasColumnName("time_utc");

            builder.Property(e => e.ChirpId)
                .HasColumnName("chirp_id");

            builder.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .HasConstraintName("fk_timeline_user_id");

            builder.HasOne(e => e.Chirp)
                .WithMany()
                .HasForeignKey(e => e.ChirpId)
                .HasConstraintName("fk_timeline_chirp_id");
        }
    }
}
