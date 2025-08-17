using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Categories.Edit
{
    public class EditCategoryCommandValidator:AbstractValidator<EditCategoryCommand>    
    {
        public EditCategoryCommandValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty().WithMessage(ValidationMessages.required("عنوان"));

            RuleFor(x => x.Slug).NotNull().NotEmpty().WithMessage(ValidationMessages.required("Slug"));

        }

    }
}
