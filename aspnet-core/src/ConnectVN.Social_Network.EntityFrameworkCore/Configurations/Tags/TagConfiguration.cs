using ConnectVN.Social_Network.Common.Domain;
using ConnectVN.Social_Network.Storys;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ConnectVN.Social_Network.Tags;

namespace ConnectVN.Social_Network.Configurations.Tags
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable(Social_NetworkConsts.DbTablePrefix + nameof(Tag));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
        }
    }
}
