using ConnectVN.Social_Network.Admin.Infrastructure.Extentions;
using ConnectVN.Social_Network.Admin.StoryContract;
using ConnectVN.Social_Network.Storys;
using FluentValidation;

namespace ConnectVN.Social_Network.Admin.Rule.Storys
{
    public class CreateUpdateStoryRule : AbstractValidator<CreateUpdateStory>
    {
        public CreateUpdateStoryRule()
        {
            RuleFor(x => x.Story_Title).MaximumLength(255)
                .WithErrorCode(EnumStoryErrorCode.ST01.ToString())
                .WithMessage(EnumStoryErrorCode.ST01.ToString().GetMessage())
                .MinimumLength(3)
                .WithErrorCode(EnumStoryErrorCode.ST02.ToString())
                .WithMessage(EnumStoryErrorCode.ST02.ToString().GetMessage())
                .NotEmpty()
                .WithErrorCode(EnumStoryErrorCode.ST00.ToString())
                .WithMessage(EnumStoryErrorCode.ST00.ToString().GetMessage());

            RuleFor(x => x.Story_Synopsis).MaximumLength(5000)
                .WithErrorCode(EnumStoryErrorCode.ST04.ToString())
                .WithMessage(EnumStoryErrorCode.ST04.ToString().GetMessage())
                .MinimumLength(100)
                .WithErrorCode(EnumStoryErrorCode.ST05.ToString())
                .WithMessage(EnumStoryErrorCode.ST05.ToString().GetMessage())
                .NotEmpty()
                .WithErrorCode(EnumStoryErrorCode.ST03.ToString())
                .WithMessage(nameof(EnumStoryErrorCode.ST03));

            RuleFor(x => x.Img_Url).MaximumLength(1000)
               .WithErrorCode(EnumStoryErrorCode.ST10.ToString())
               .WithMessage(EnumStoryErrorCode.ST10.ToString().GetMessage())
               .NotEmpty()
               .WithErrorCode(EnumNotificationStoryErrorCodes.NT01.ToString())
               .WithMessage(EnumNotificationStoryErrorCodes.NT01.ToString().GetMessage());

            RuleFor(x => x.IsShow)
              .NotEmpty()
              .WithErrorCode(EnumStoryErrorCode.ST08.ToString())
              .WithMessage(EnumStoryErrorCode.ST08.ToString().GetMessage());
        }

    }
}