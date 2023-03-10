using ConnectVN.Social_Network.Chapters;
using ConnectVN.Social_Network.Common.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ConnectVN.Social_Network.Configurations.Chapters
{
    public class ChapterConfiguration : IEntityTypeConfiguration<Chapter>
    {
        public void Configure(EntityTypeBuilder<Chapter> builder)
        {
            builder.ToTable(Social_NetworkConsts.DbTablePrefix + nameof(Chapter));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasOne(x => x.Story)
                .WithMany(x => x.Chapters)
                .HasForeignKey(x => x.StoryGuid);
        }
    }
}
