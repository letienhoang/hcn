using FluentValidation;

namespace HCN.Admin.Catalog.Stories
{
    public class CreateUpdateStoryDtoValidator : AbstractValidator<CreateUpdateStoryDto>
    {
        public CreateUpdateStoryDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(256);
            RuleFor(x => x.Slug).NotEmpty().MaximumLength(256);
            RuleFor(x => x.Code).NotEmpty().MaximumLength(128);
            RuleFor(x => x.BriefContent).MaximumLength(1024);
            RuleFor(x => x.Pictures).MaximumLength(512);
            RuleFor(x => x.ReferenceSource).MaximumLength(512);
            RuleFor(x => x.KeywordSEO).MaximumLength(512);
            RuleFor(x => x.DescriptionSEO).MaximumLength(1024);
        }
    }
}