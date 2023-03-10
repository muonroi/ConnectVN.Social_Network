using ConnectVN.Social_Network.Common.Domain;
using ConnectVN.Social_Network.Storys;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ConnectVN.Social_Network.Configurations.Storys
{
    public class StoryPublishConfiguration : IEntityTypeConfiguration<StoryPublish>
    {
        public void Configure(EntityTypeBuilder<StoryPublish> builder)
        {
            builder.ToTable(Social_NetworkConsts.DbTablePrefix + nameof(StoryPublish));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasOne(x => x.UserMember).WithMany(x => x.StoryPublish).HasForeignKey(x => x.UserGuid);
        }
    }
}
