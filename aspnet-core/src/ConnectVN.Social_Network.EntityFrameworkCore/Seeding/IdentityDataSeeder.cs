using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using IdentityUser = Volo.Abp.Identity.IdentityUser;
using IdentityRole = Volo.Abp.Identity.IdentityRole;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;
using Volo.Abp.Identity;

namespace ConnectVN.Social_Network.Seeding
{
    public class IdentityDataSeeder : ITransientDependency, IIdentityDataSeeder
    {
        protected IGuidGenerator GuidGenerator { get; }
        protected IIdentityRoleRepository IdentityRoleRepository { get; }
        protected IOptions<IdentityOptions> IdentityOptions { get; }
        protected ICurrentTenant CurrentTenant { get; }
        protected IIdentityUserRepository UserRepository { get; }
        protected ILookupNormalizer LookupNormalizer { get; }
        protected IdentityUserManager IdentityUserManager { get; }
        protected IdentityRoleManager IdentityRoleManager { get; }

        public IdentityDataSeeder(IGuidGenerator guidGenerator, IIdentityRoleRepository identityRoleRepository, IOptions<IdentityOptions> identityOptions, ICurrentTenant currentTenant, IIdentityUserRepository userRepository, ILookupNormalizer lookupNormalizer, IdentityUserManager identityUserManager, IdentityRoleManager identityRoleManager)
        {
            GuidGenerator = guidGenerator;
            IdentityRoleRepository = identityRoleRepository;
            IdentityOptions = identityOptions;
            CurrentTenant = currentTenant;
            UserRepository = userRepository;
            LookupNormalizer = lookupNormalizer;
            IdentityUserManager = identityUserManager;
            IdentityRoleManager = identityRoleManager;
        }
        [UnitOfWork]
        public virtual async Task<IdentityDataSeedResult> SeedAsync(string adminEmail, string adminPassword, Guid? tenantId = null)
        {
            using (CurrentTenant.Change(tenantId))
            {
                await IdentityOptions.SetAsync();
                IdentityDataSeedResult result = new();
                IdentityUser existsUserNameAdmin = await UserRepository.FindByNormalizedUserNameAsync(LookupNormalizer.NormalizeName(adminEmail));
                if (existsUserNameAdmin != null)
                {
                    return result;
                }
                existsUserNameAdmin = new IdentityUser(
                    GuidGenerator.Create(),
                    adminEmail,
                    adminEmail,
                    tenantId
                    )
                {
                    Name = adminEmail,
                };
                (await IdentityUserManager.CreateAsync(existsUserNameAdmin, adminPassword, true)).CheckErrors();
                result.CreatedAdminUser = true;

                const string roleName = "Administrator";
                IdentityRole existsRoleName = await IdentityRoleRepository.FindByNormalizedNameAsync(LookupNormalizer.NormalizeName(roleName));
                if (existsRoleName != null)
                {
                    return result;
                }
                existsRoleName = new IdentityRole(
                    GuidGenerator.Create(),
                    roleName,
                    tenantId
                    )
                {
                    IsStatic = true,
                    IsPublic = true,
                };
                (await IdentityRoleManager.CreateAsync(existsRoleName)).CheckErrors();
                result.CreatedAdminRole = true;

                (await IdentityUserManager.AddToRoleAsync(existsUserNameAdmin, roleName)).CheckErrors();
                return result;
            }
        }
    }
}
