using ConnectVN.Social_Network.Common.Domain;
using ConnectVN.Social_Network.Roles;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ConnectVN.Social_Network.Configurations.Roles
{
    public class GroupUserMemberConfiguration : IEntityTypeConfiguration<GroupUserMember>
    {
        public void Configure(EntityTypeBuilder<GroupUserMember> builder)
        {
            builder.ToTable(Social_NetworkConsts.DbTablePrefix + nameof(GroupUserMember));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
        }
    }
}
