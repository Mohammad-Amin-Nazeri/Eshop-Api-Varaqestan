using Common.Application.Validation;
using FluentValidation;
using Common.Application.Validation.FluentValidations;

namespace Shop.Application.Products.AddImage
{
    public class AddProductImageCommandValidator : AbstractValidator<AddProductImageCommand>
    {
        public AddProductImageCommandValidator()
        {
            RuleFor(b => b.ImageFile)
                .NotNull().WithMessage(ValidationMessages.required("عکس"))
                .JustImageFile();

            RuleFor(b => b.Sequence)
                .GreaterThanOrEqualTo(0);
        }
    }
}
