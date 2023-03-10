using ConnectVN.Social_Network.Common.Domain;
using ConnectVN.Social_Network.Storys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConnectVN.Social_Network.Configurations.Storys
{
    public class StoryNotificationConfiguration : IEntityTypeConfiguration<StoryNotifications>
    {
        public void Configure(EntityTypeBuilder<StoryNotifications> builder)
        {
            builder.ToTable(Social_NetworkConsts.DbTablePrefix + nameof(StoryNotifications));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasOne(x => x.Story).WithMany(x => x.StoryNotifications).HasForeignKey(x => x.StoryGuid);
            builder.HasOne(x => x.UserMember).WithMany(x => x.StoryNotifications).HasForeignKey(x => x.UserGuid);
        }
    }
}
