using FluentValidation;

namespace HCN.Admin.Catalog.Tools
{
    public class CreateUpdateToolDtoValidator : AbstractValidator<CreateUpdateToolDto>
    {
        public CreateUpdateToolDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(256);
            RuleFor(x => x.Slug).NotEmpty().MaximumLength(256);
            RuleFor(x => x.Code).NotEmpty().MaximumLength(128);
            RuleFor(x => x.Pictures).MaximumLength(512);
            RuleFor(x => x.KeywordSEO).MaximumLength(512);
            RuleFor(x => x.DescriptionSEO).MaximumLength(1024);
        }
    }
}