using FluentValidation;

namespace HCN.Admin.Catalog.Units
{
    public class CreateUpdateUnitDtoValidator : AbstractValidator<CreateUpdateUnitDto>
    {
        public CreateUpdateUnitDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(256);
            RuleFor(x => x.BriefContent).MaximumLength(1024);
        }
    }
}