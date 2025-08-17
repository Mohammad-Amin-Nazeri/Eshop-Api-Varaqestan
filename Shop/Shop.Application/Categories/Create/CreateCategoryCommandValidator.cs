using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Categories.Create
{
    public class CreateCategoryCommandValidator:AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty().WithMessage(ValidationMessages.required("عنوان"));

            RuleFor(x => x.Slug).NotNull().NotEmpty().WithMessage(ValidationMessages.required("Slug"));

        }
    }
}
