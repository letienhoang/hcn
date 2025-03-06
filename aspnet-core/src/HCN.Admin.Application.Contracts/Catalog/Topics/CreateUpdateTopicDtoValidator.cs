using FluentValidation;

namespace HCN.Admin.Catalog.Topics
{
    public class CreateUpdateTopicDtoValidator : AbstractValidator<CreateUpdateTopicDto>
    {
        public CreateUpdateTopicDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(256);
            RuleFor(x => x.Slug).NotEmpty().MaximumLength(256);
            RuleFor(x => x.Code).NotEmpty().MaximumLength(128);
            RuleFor(x => x.KeywordSEO).MaximumLength(512);
            RuleFor(x => x.DescriptionSEO).MaximumLength(1024);
        }
    }
}