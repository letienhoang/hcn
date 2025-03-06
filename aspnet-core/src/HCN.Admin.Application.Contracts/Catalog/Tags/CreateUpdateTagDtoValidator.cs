using FluentValidation;

namespace HCN.Admin.Catalog.Tags
{
    public class CreateUpdateTagDtoValidator : AbstractValidator<CreateUpdateTagDto>
    {
        public CreateUpdateTagDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(256);
            RuleFor(x => x.Slug).NotEmpty().MaximumLength(256);
        }
    }
}