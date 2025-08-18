using Common.Application;
using FluentValidation;
using Shop.Domain.OrderAgg.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Orders.DecreaseItemCount
{
    public record DecreaseOrderItemCountCommand(long userId, long ItemId, int count) : IBaseCommand;

    public class DecreaseOrderItemCountCommandHandler : IBaseCommandHandler<DecreaseOrderItemCountCommand>
    {
        private readonly IOrderRepository _repository;

        public DecreaseOrderItemCountCommandHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(DecreaseOrderItemCountCommand request, CancellationToken cancellationToken)
        {
            var currentOrder = await _repository.GetCurrentUserOrder(request.userId);
            if (currentOrder == null)
                return OperationResult.NotFound();

            currentOrder.DecreaseItemCount(request.ItemId, request.count);
            await _repository.Save();
            return OperationResult.Success();
        }

    }

    public class DecreaseOrderItemCountCommandValidator : AbstractValidator<DecreaseOrderItemCountCommand>
    {
        public DecreaseOrderItemCountCommandValidator()
        {
            RuleFor(f => f.count).GreaterThanOrEqualTo(1).WithMessage("تعداد باید بیتشر از 0 باشد");
        }
    }
}
