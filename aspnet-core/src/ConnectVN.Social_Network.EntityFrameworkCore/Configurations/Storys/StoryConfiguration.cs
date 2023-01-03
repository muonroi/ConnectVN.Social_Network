using ConnectVN.Social_Network.Common.Domain;
using ConnectVN.Social_Network.Storys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConnectVN.Social_Network.Configurations.Storys
{
    public class StoryConfiguration : IEntityTypeConfiguration<Story>
    {
        public void Configure(EntityTypeBuilder<Story> builder)
        {
            builder.ToTable(Social_NetworkConsts.DbTablePrefix + (nameof(Story)));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IsShow).HasDefaultValue(false);

            builder.HasOne(x => x.Category)
                .WithMany(x => x.Storys)
                .HasForeignKey(x => x.CategoryId);
        }
    }
}
