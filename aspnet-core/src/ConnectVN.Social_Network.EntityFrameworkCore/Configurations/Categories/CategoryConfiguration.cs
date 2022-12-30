using ConnectVN.Social_Network.Categories;
using ConnectVN.Social_Network.Common.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ConnectVN.Social_Network.Configurations.Categories
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(Social_NetworkConsts.DbTablePrefix + nameof(Category));
            builder.HasKey(x => x.Id);
        }
    }
}
