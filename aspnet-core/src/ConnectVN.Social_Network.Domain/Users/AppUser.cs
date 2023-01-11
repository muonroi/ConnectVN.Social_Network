using ConnectVN.Social_Network.Roles;
using ConnectVN.Social_Network.Storys;
using ConnectVN.Social_Network.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Identity;
namespace ConnectVN.Social_Network.Users
{
    /// <summary>
    /// Table User Members
    /// </summary>
    public class AppUser : IdentityUser
    {
        public AppUser()
        {

        }
        public AppUser(
        Guid id,
        [NotNull] string userName,
        [NotNull] string email,
        Guid? tenantId = null)
        {
            Check.NotNull(userName, nameof(userName));
            Check.NotNull(email, nameof(email));
            Id = id;
            TenantId = tenantId;
            UserName = userName;
            NormalizedUserName = userName.ToUpperInvariant();
            Email = email;
            NormalizedEmail = email.ToUpperInvariant();
            ConcurrencyStamp = Guid.NewGuid().ToString("N");
            SecurityStamp = Guid.NewGuid().ToString();
            IsActive = true;

            Roles = new Collection<IdentityUserRole>();
            Claims = new Collection<IdentityUserClaim>();
            Logins = new Collection<IdentityUserLogin>();
            Tokens = new Collection<IdentityUserToken>();
            OrganizationUnits = new Collection<IdentityUserOrganizationUnit>();
        }
        public AppUser(Guid guidId, string firstName, string lastName, string userName, string password, string email, string phoneNumber, string address, DateTime birthDate, EnumGender gender, string avatarLink)
        {
            Id = guidId;
            Name = firstName;
            Surname = lastName;
            UserName = userName;
            PasswordHash = password;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            BirthDate = birthDate;
            Gender = gender;
            Avatar = avatarLink;
        }
        /// <summary>
        /// FirstName''s User
        /// </summary>
        [Required(ErrorMessage = nameof(EnumUserErrorCodes.USR03C))]
        [MaxLength(100, ErrorMessage = nameof(EnumUserErrorCodes.USR08C))]
        public override string Name { get; set; }
        /// <summary>
        /// LastName''s User
        /// </summary>
        [Required(ErrorMessage = nameof(EnumUserErrorCodes.USR04C))]
        [MaxLength(100, ErrorMessage = nameof(EnumUserErrorCodes.USR09C))]
        public override string Surname { get; set; }
        /// <summary>
        /// UserName''s User
        /// </summary>
        [Required(ErrorMessage = nameof(EnumUserErrorCodes.USR05C))]
        [MaxLength(100, ErrorMessage = nameof(EnumUserErrorCodes.USR10C))]
        [MinLength(5, ErrorMessage = nameof(EnumUserErrorCodes.USR15C))]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9_\.]{3,99}[a-z0-9](\@([a-zA-Z0-9][a-zA-Z0-9\.]+[a-zA-Z0-9]{2,}){1,5})?$", ErrorMessage = nameof(EnumUserErrorCodes.USR14C))]
        public override string UserName { get; protected set; }
        /// <summary>
        /// Password''s User
        /// </summary>
        [Required(ErrorMessage = nameof(EnumUserErrorCodes.USR06C))]
        [MaxLength(1000, ErrorMessage = nameof(EnumUserErrorCodes.USR11C))]
        [MinLength(8, ErrorMessage = nameof(EnumUserErrorCodes.USR26C))]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = nameof(EnumUserErrorCodes.USR17C))]
        public override string PasswordHash { get; protected set; }
        /// <summary>
        /// Email''s User
        /// </summary>
        [MaxLength(1000, ErrorMessage = nameof(EnumUserErrorCodes.USR20C))]
        [EmailAddress(ErrorMessage = nameof(EnumUserErrorCodes.USR19C))]
        public override string Email { get; protected set; }
        /// <summary>
        /// PhoneNumber''s User
        /// </summary>
        [RegularExpression(@"^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$", ErrorMessage = nameof(EnumUserErrorCodes.USR21C))]
        public override string PhoneNumber { get; protected set; }
        /// <summary>
        /// Address''s User
        /// </summary>
        [MaxLength(1000, ErrorMessage = nameof(EnumUserErrorCodes.USR18C))]
        public string Address { get; set; }
        /// <summary>
        /// BirthDate''s User
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// Gender''s User
        /// </summary>
        public EnumGender Gender { get; set; }
        /// <summary>
        /// Last login date
        /// </summary>
        /// <value></value>
        public DateTime LastLogin { get; set; }
        /// <summary>
        /// Avatar Link
        /// </summary>
        /// <value></value>
        [MaxLength(1000, ErrorMessage = nameof(EnumNotificationStoryErrorCodes.NT01))]
        public string Avatar { get; set; }

        /// <summary>
        /// Status of account
        /// </summary>
        /// <value></value>
        [Required(ErrorMessage = nameof(EnumUserErrorCodes.USR22C))]
        public EnumAccountStatus Status { get; set; }

        /// <summary>
        /// Ghi chú cho tài khoản
        /// </summary>
        /// <value></value>
        public string Note { get; set; }

        /// <summary>
        /// Lý do khóa tài khoản
        /// </summary>
        /// <value></value>
        public string LockReason { get; set; }

        /// <summary>
        /// GroupId of account
        /// </summary>
        public int? GroupId { get; set; }
        public GroupUserMember GroupUserMember { get; set; }
        public List<BookMarkStory> BookMarkStory { get; set; }
        public List<StoryNotifications> StoryNotifications { get; set; }
        public List<StoryPublish> StoryPublish { get; set; }
        public List<StoryReview> StoryReview { get; set; }
        public List<FollowingAuthor> FollowingAuthor { get; set; }
    }
}