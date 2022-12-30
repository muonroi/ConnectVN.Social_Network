using ConnectVN.Social_Network.Common.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ConnectVN.Social_Network.Users;

namespace ConnectVN.Social_Network.Configurations.Users
{
    public class UserMemberConfiguration : IEntityTypeConfiguration<UserMember>
    {
        public void Configure(EntityTypeBuilder<UserMember> builder)
        {
            builder.ToTable(Social_NetworkConsts.DbTablePrefix + nameof(UserMember));
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.GroupUserMember)
                .WithMany(x => x.UserMembers)
                .HasForeignKey(x => x.GroupId);

        }
    }
}
