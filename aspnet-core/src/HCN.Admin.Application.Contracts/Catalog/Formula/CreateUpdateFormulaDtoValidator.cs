using FluentValidation;

namespace HCN.Admin.Catalog.Formulas
{
    public class CreateUpdateFormulaDtoValidator : AbstractValidator<CreateUpdateFormulaDto>
    {
        public CreateUpdateFormulaDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(256);
            RuleFor(x => x.Slug).NotEmpty().MaximumLength(256);
            RuleFor(x => x.Code).NotEmpty().MaximumLength(128);
            RuleFor(x => x.BriefContent).MaximumLength(1024);
            RuleFor(x => x.VideoUrl).MaximumLength(512);
            RuleFor(x => x.ReferenceSource).MaximumLength(512);
            RuleFor(x => x.KeywordSEO).MaximumLength(512);
            RuleFor(x => x.DescriptionSEO).MaximumLength(1024);
        }
    }
}