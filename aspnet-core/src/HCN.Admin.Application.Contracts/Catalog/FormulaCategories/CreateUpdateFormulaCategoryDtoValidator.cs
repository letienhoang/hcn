using FluentValidation;

namespace HCN.Admin.Catalog.FormulaCategories
{
    public class CreateUpdateFormulaCategoryDtoValidator : AbstractValidator<CreateUpdateFormulaCategoryDto>
    {
        public CreateUpdateFormulaCategoryDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(256);
            RuleFor(x => x.Slug).NotEmpty().MaximumLength(256);
            RuleFor(x => x.KeywordSEO).MaximumLength(512);
            RuleFor(x => x.DescriptionSEO).MaximumLength(1024);
        }
    }
}