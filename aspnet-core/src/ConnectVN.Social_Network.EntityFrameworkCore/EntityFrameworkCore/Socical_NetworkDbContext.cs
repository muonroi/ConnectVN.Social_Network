using ConnectVN.Social_Network.Categories;
using ConnectVN.Social_Network.Chapters;
using ConnectVN.Social_Network.Configurations.Categories;
using ConnectVN.Social_Network.Configurations.Chapters;
using ConnectVN.Social_Network.Configurations.Roles;
using ConnectVN.Social_Network.Configurations.Storys;
using ConnectVN.Social_Network.Configurations.Tags;
using ConnectVN.Social_Network.Configurations.Users;
using ConnectVN.Social_Network.Roles;
using ConnectVN.Social_Network.Storys;
using ConnectVN.Social_Network.Tags;
using ConnectVN.Social_Network.Users;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace ConnectVN.Social_Network.Common.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class Social_NetworkDbContext :
    AbpDbContext<Social_NetworkDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    //Social Network
    public DbSet<Story> Stories { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Chapter> Chapters { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<TagInStory> TagInStories { get; set; }
    public DbSet<BookMarkStory> BookMarkStories { get; set; }
    public DbSet<StoryPublish> StoryPublishes { get; set; }
    public DbSet<StoryNotifications> StoryNotifications { get; set; }
    public DbSet<GroupUserMember> GroupUserMembers { get; set; }
    public DbSet<Role> UserRoles { get; set; }
    public DbSet<FollowingAuthor> FollowingAuthors { get; set; }
    public DbSet<StoryReview> StoryReviews { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }

    #endregion

    public Social_NetworkDbContext(DbContextOptions<Social_NetworkDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();
        builder.ApplyConfiguration(new UserMemberConfiguration());
        builder.ApplyConfiguration(new FollowingAuthorConfiguration());
        builder.ApplyConfiguration(new TagConfiguration());
        builder.ApplyConfiguration(new TagInStoryConfiguration());
        builder.ApplyConfiguration(new StoryReviewConfiguration());
        builder.ApplyConfiguration(new StoryPublishConfiguration());
        builder.ApplyConfiguration(new StoryNotificationConfiguration());
        builder.ApplyConfiguration(new StoryConfiguration());
        builder.ApplyConfiguration(new BookMarkStoryConfiguration());
        builder.ApplyConfiguration(new GroupUserMemberConfiguration());
        builder.ApplyConfiguration(new RoleConfiguration());
        builder.ApplyConfiguration(new ChapterConfiguration());
        builder.ApplyConfiguration(new CategoryConfiguration());
    }
}
