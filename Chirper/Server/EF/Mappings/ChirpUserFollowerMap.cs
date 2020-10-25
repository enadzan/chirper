using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Chirper.Server.DomainModel;

namespace Chirper.Server.EF.Mappings
{
    public class ChirpUserFollowerMap: IEntityTypeConfiguration<ChirpUserFollower>
    {
        public void Configure(EntityTypeBuilder<ChirpUserFollower> builder)
        {
            builder.ToTable("chirp_user_follower");

            builder.HasKey("UserId", "FollowerId")
                .HasName("pk_chirp_user_follower");

            builder.Property(e => e.UserId)
                .HasColumnName("user_id");

            builder.Property(e => e.FollowerId)
                .HasColumnName("follower_id");

            builder.HasOne(e => e.User)
                .WithMany(e => e.Followers)
                .HasForeignKey(e => e.UserId)
                .HasConstraintName("fk_chirp_user_follower_user_id");

            builder.HasOne(e => e.Follower)
                .WithMany()
                .HasForeignKey(e => e.FollowerId)
                .HasConstraintName("fk_chirp_user_follower_follower_id");
        }
    }
}
