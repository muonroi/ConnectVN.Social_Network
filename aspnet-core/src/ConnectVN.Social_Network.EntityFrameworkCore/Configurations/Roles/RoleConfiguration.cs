using ConnectVN.Social_Network.Common.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ConnectVN.Social_Network.Roles;

namespace ConnectVN.Social_Network.Configurations.Roles
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable(Social_NetworkConsts.DbTablePrefix + nameof(Role));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasOne(x => x.GroupUserMember)
                .WithMany(x => x.Roles)
                .HasForeignKey(x => x.GroupId);
        }
    }
}
