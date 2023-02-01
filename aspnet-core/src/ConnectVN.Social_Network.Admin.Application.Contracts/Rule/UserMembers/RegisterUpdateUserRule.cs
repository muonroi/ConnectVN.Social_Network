using ConnectVN.Social_Network.Admin.Infrastructure.Extentions;
using ConnectVN.Social_Network.Admin.UserContract;
using ConnectVN.Social_Network.Storys;
using ConnectVN.Social_Network.Users;
using FluentValidation;

namespace ConnectVN.Social_Network.Admin.Rule.UserMembers
{
    public class RegisterUpdateUserRule : AbstractValidator<RegisterUpdateUser>
    {
        public RegisterUpdateUserRule()
        {
            RuleFor(x => x.FirstName).MaximumLength(100)
                .WithErrorCode(nameof(EnumUserErrorCodes.USR08C))
                .WithMessage(ManageMyFunction.GetMessage(nameof(EnumUserErrorCodes.USR08C)))
                .MinimumLength(3)
                .WithErrorCode(nameof(EnumUserErrorCodes.USR30C))
                .WithMessage(ManageMyFunction.GetMessage(nameof(EnumUserErrorCodes.USR30C)))
                .NotEmpty()
                .WithErrorCode(nameof(EnumUserErrorCodes.USR03C))
                .WithMessage(ManageMyFunction.GetMessage(nameof(EnumUserErrorCodes.USR03C)));

            RuleFor(x => x.LastName).MaximumLength(100)
               .WithErrorCode(nameof(EnumUserErrorCodes.USR09C))
               .WithMessage(ManageMyFunction.GetMessage(nameof(EnumUserErrorCodes.USR09C)))
               .MinimumLength(3)
               .WithErrorCode(nameof(EnumUserErrorCodes.USR30C))
               .WithMessage(ManageMyFunction.GetMessage(nameof(EnumUserErrorCodes.USR30C)))
               .NotEmpty()
               .WithErrorCode(nameof(EnumUserErrorCodes.USR04C))
               .WithMessage(ManageMyFunction.GetMessage(nameof(EnumUserErrorCodes.USR04C)));

            RuleFor(x => x.UserName).MaximumLength(100)
               .WithErrorCode(nameof(EnumUserErrorCodes.USR10C))
               .WithMessage(ManageMyFunction.GetMessage(nameof(EnumUserErrorCodes.USR10C)))
               .MinimumLength(5)
               .WithErrorCode(nameof(EnumUserErrorCodes.USR15C))
               .WithMessage(ManageMyFunction.GetMessage(nameof(EnumUserErrorCodes.USR15C)))
               .NotEmpty()
               .WithErrorCode(nameof(EnumUserErrorCodes.USR05C))
               .WithMessage(ManageMyFunction.GetMessage(nameof(EnumUserErrorCodes.USR05C))).Matches(@"^[a-zA-Z][a-zA-Z0-9_\.]{3,99}[a-z0-9](\@([a-zA-Z0-9][a-zA-Z0-9\.]+[a-zA-Z0-9]{2,}){1,5})?$")
               .WithErrorCode(nameof(EnumUserErrorCodes.USR14C))
               .WithMessage(ManageMyFunction.GetMessage(nameof(EnumUserErrorCodes.USR14C)));

            RuleFor(x => x.Password).MaximumLength(1000)
               .WithErrorCode(nameof(EnumUserErrorCodes.USR11C))
               .WithMessage(ManageMyFunction.GetMessage(nameof(EnumUserErrorCodes.USR11C)))
               .MinimumLength(8)
               .WithErrorCode(nameof(EnumUserErrorCodes.USR26C))
               .WithMessage(ManageMyFunction.GetMessage(nameof(EnumUserErrorCodes.USR26C)))
               .NotEmpty()
               .WithErrorCode(nameof(EnumUserErrorCodes.USR06C))
               .WithMessage(ManageMyFunction.GetMessage(nameof(EnumUserErrorCodes.USR06C))).Matches(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")
               .WithErrorCode(nameof(EnumUserErrorCodes.USR17C))
               .WithMessage(ManageMyFunction.GetMessage(nameof(EnumUserErrorCodes.USR17C)));

            RuleFor(x => x.Email).MaximumLength(1000)
               .WithErrorCode(nameof(EnumUserErrorCodes.USR20C))
               .WithMessage(ManageMyFunction.GetMessage(nameof(EnumUserErrorCodes.USR20C)))
               .NotEmpty()
               .WithErrorCode(nameof(EnumUserErrorCodes.USR31C))
               .WithMessage(ManageMyFunction.GetMessage(nameof(EnumUserErrorCodes.USR31C)))
               .EmailAddress()
               .WithErrorCode(nameof(EnumUserErrorCodes.USR19C))
               .WithMessage(ManageMyFunction.GetMessage(nameof(EnumUserErrorCodes.USR19C)));

            RuleFor(x => x.PhoneNumber).Matches(@"^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$")
              .WithErrorCode(nameof(EnumUserErrorCodes.USR21C))
              .WithMessage(ManageMyFunction.GetMessage(nameof(EnumUserErrorCodes.USR21C)));

            RuleFor(x => x.PhoneNumber).Matches(@"^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$")
             .WithErrorCode(nameof(EnumUserErrorCodes.USR21C))
             .WithMessage(ManageMyFunction.GetMessage(nameof(EnumUserErrorCodes.USR21C)));

            RuleFor(x => x.Address)
           .MaximumLength(1000)
               .WithErrorCode(nameof(EnumUserErrorCodes.USR18C))
               .WithMessage(ManageMyFunction.GetMessage(nameof(EnumUserErrorCodes.USR18C)))
               .MinimumLength(10)
               .WithErrorCode(nameof(EnumUserErrorCodes.USRC33C))
               .WithMessage(ManageMyFunction.GetMessage(nameof(EnumUserErrorCodes.USRC33C)))
               .NotEmpty()
               .WithErrorCode(nameof(EnumUserErrorCodes.USR32C))
               .WithMessage(ManageMyFunction.GetMessage(nameof(EnumUserErrorCodes.USR32C)));

            RuleFor(x => x.Avatar)
           .MaximumLength(1000)
               .WithErrorCode(nameof(EnumNotificationStoryErrorCodes.NT01))
               .WithMessage(ManageMyFunction.GetMessage(nameof(EnumNotificationStoryErrorCodes.NT01)));
        }
    }
}
