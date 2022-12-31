using ConnectVN.Social_Network.Admin.Infrastructure.Extentions;
using ConnectVN.Social_Network.Admin.StoryContract;
using ConnectVN.Social_Network.Storys;
using FluentValidation;

namespace ConnectVN.Social_Network.Admin.Rule
{
    public class CreateUpdateStoryRule : AbstractValidator<CreateUpdateStory>
    {
        public CreateUpdateStoryRule()
        {
            RuleFor(x => x.Story_Title).MaximumLength(255)
                .WithErrorCode(EnumStoryErrorCode.ST01.ToString())
                .WithMessage(GetErrorMessage.GetMessage(EnumStoryErrorCode.ST01.ToString()))
                .MinimumLength(3)
                .WithErrorCode(EnumStoryErrorCode.ST02.ToString())
                .WithMessage(GetErrorMessage.GetMessage(EnumStoryErrorCode.ST02.ToString()))
                .NotEmpty()
                .WithErrorCode(EnumStoryErrorCode.ST00.ToString())
                .WithMessage(GetErrorMessage.GetMessage(EnumStoryErrorCode.ST00.ToString()));

            RuleFor(x => x.Story_Synopsis).MaximumLength(5000)
                .WithErrorCode(EnumStoryErrorCode.ST04.ToString())
                .WithMessage(GetErrorMessage.GetMessage(EnumStoryErrorCode.ST04.ToString()))
                .MinimumLength(100)
                .WithErrorCode(EnumStoryErrorCode.ST05.ToString())
                .WithMessage(GetErrorMessage.GetMessage(EnumStoryErrorCode.ST05.ToString()))
                .NotEmpty()
                .WithErrorCode(EnumStoryErrorCode.ST03.ToString())
                .WithMessage(nameof(EnumStoryErrorCode.ST03));

            RuleFor(x => x.Img_Url).MaximumLength(1000)
               .WithErrorCode(EnumStoryErrorCode.ST10.ToString())
               .WithMessage(GetErrorMessage.GetMessage(EnumStoryErrorCode.ST10.ToString()))
               .NotEmpty()
               .WithErrorCode(EnumNotificationStoryErrorCodes.NT01.ToString())
               .WithMessage(GetErrorMessage.GetMessage(EnumNotificationStoryErrorCodes.NT01.ToString()));

            RuleFor(x => x.IsShow)
              .NotEmpty()
              .WithErrorCode(EnumStoryErrorCode.ST08.ToString())
              .WithMessage(GetErrorMessage.GetMessage(EnumStoryErrorCode.ST08.ToString()));
        }

    }
}