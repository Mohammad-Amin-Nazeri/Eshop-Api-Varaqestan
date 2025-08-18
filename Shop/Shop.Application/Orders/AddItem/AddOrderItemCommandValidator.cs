using FluentValidation;

namespace Shop.Application.Orders.AddItem
{
    public class AddOrderItemCommandValidator : AbstractValidator<AddOrderItemCommand>
    {
        public AddOrderItemCommandValidator()
        {
            RuleFor(f => f.Count).GreaterThanOrEqualTo(1).WithMessage("تعداد باید بیتشر از 0 باشد");
        }
    }
}
