using FluentValidation;

namespace HCN.Admin.Catalog.ToolCategories
{
    public class CreateUpdateToolCategoryDtoValidator : AbstractValidator<CreateUpdateToolCategoryDto>
    {
        public CreateUpdateToolCategoryDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(256);
            RuleFor(x => x.Slug).NotEmpty().MaximumLength(256);
            RuleFor(x => x.KeywordSEO).MaximumLength(512);
            RuleFor(x => x.DescriptionSEO).MaximumLength(1024);
        }
    }
}