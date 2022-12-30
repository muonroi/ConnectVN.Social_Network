using ConnectVN.Social_Network.Categories;
using ConnectVN.Social_Network.Common.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ConnectVN.Social_Network.Configurations.Categories
{
    public class CategoriesInStoryConfiguration : IEntityTypeConfiguration<CategoryInStory>
    {
        public void Configure(EntityTypeBuilder<CategoryInStory> builder)
        {
            builder.ToTable(Social_NetworkConsts.DbTablePrefix + nameof(CategoryInStory));
            builder.HasKey(x => new { x.CategoryId, x.StoryGuid });

            builder.HasOne(x => x.Category)
                .WithMany(x => x.CategoryInStory)
                .HasForeignKey(x => x.CategoryId);

            builder.HasOne(x => x.Story)
              .WithMany(x => x.CategoryInStory)
              .HasForeignKey(x => x.StoryGuid);
        }
    }
}
