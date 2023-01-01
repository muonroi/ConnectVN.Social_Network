using ConnectVN.Social_Network.Roles;
using ConnectVN.Social_Network.Storys;
using ConnectVN.Social_Network.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace ConnectVN.Social_Network.Users
{
    /// <summary>
    /// Table User Members
    /// </summary>
    public class UserMember : FullAuditedAggregateRoot<Guid>
    {
        public UserMember()
        {

        }
        public UserMember(Guid guidId, string firstName, string lastName, string userName, string password, string email, string phoneNumber, string address, DateTime birthDate, EnumGender gender, string avatarLink)
        {
            Id = guidId;
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Password = password;
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
        public string FirstName { get; set; }
        /// <summary>
        /// LastName''s User
        /// </summary>
        [Required(ErrorMessage = nameof(EnumUserErrorCodes.USR04C))]
        [MaxLength(100, ErrorMessage = nameof(EnumUserErrorCodes.USR09C))]
        public string LastName { get; set; }
        /// <summary>
        /// UserName''s User
        /// </summary>
        [Required(ErrorMessage = nameof(EnumUserErrorCodes.USR05C))]
        [MaxLength(100, ErrorMessage = nameof(EnumUserErrorCodes.USR10C))]
        [MinLength(5, ErrorMessage = nameof(EnumUserErrorCodes.USR15C))]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9_\.]{3,99}[a-z0-9](\@([a-zA-Z0-9][a-zA-Z0-9\.]+[a-zA-Z0-9]{2,}){1,5})?$", ErrorMessage = nameof(EnumUserErrorCodes.USR14C))]
        public string UserName { get; set; }
        /// <summary>
        /// Password''s User
        /// </summary>
        [Required(ErrorMessage = nameof(EnumUserErrorCodes.USR06C))]
        [MaxLength(1000, ErrorMessage = nameof(EnumUserErrorCodes.USR11C))]
        [MinLength(8, ErrorMessage = nameof(EnumUserErrorCodes.USR26C))]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = nameof(EnumUserErrorCodes.USR17C))]
        public string Password { get; set; }
        /// <summary>
        /// Email''s User
        /// </summary>
        [MaxLength(1000, ErrorMessage = nameof(EnumUserErrorCodes.USR20C))]
        [EmailAddress(ErrorMessage = nameof(EnumUserErrorCodes.USR19C))]
        public string Email { get; set; }
        /// <summary>
        /// PhoneNumber''s User
        /// </summary>
        [RegularExpression(@"^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$", ErrorMessage = nameof(EnumUserErrorCodes.USR21C))]
        public string PhoneNumber { get; set; }
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
        /// Số lần được phép login sai
        /// </summary>
        /// <value></value>
        public int LoginAttemp { get; set; }

        /// <summary>       
        /// Profile
        /// </summary>
        public string Profile { get; set; }

        /// <summary>
        /// Mã kích hoạt tài khoản
        /// </summary>
        /// <value></value>

        public Guid ActiveToken { get; set; }

        /// <summary>
        /// Mã kích hoạt quên mật khẩu
        /// </summary>
        public Guid ForgotToken { get; set; }

        /// <summary>
        /// Thời hạn mã kích hoạt quên mật khẩu
        /// </summary>
        public DateTime? TokenExpiredTime { get; set; }

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