using ConnectVN.Social_Network.Common.Domain;
using ConnectVN.Social_Network.Storys;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ConnectVN.Social_Network.Tags;

namespace ConnectVN.Social_Network.Configurations.Tags
{
    public class TagInStoryConfiguration : IEntityTypeConfiguration<TagInStory>
    {
        public void Configure(EntityTypeBuilder<TagInStory> builder)
        {
            builder.ToTable(Social_NetworkConsts.DbTablePrefix + nameof(TagInStory));
            builder.HasKey(x => new { x.StoryGuid, x.TagId });
            builder.HasOne(x => x.Tag)
                .WithMany(x => x.TagInStory)
                .HasForeignKey(x => x.TagId);

            builder.HasOne(x => x.Story)
                .WithMany(x => x.TagInStory)
                .HasForeignKey(x => x.StoryGuid);
        }
    }
}
