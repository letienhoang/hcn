using FluentValidation;

namespace HCN.Admin.Catalog.Materials
{
    public class CreateUpdateMaterialDtoValidator : AbstractValidator<CreateUpdateMaterialDto>
    {
        public CreateUpdateMaterialDtoValidator()
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