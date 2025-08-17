using Common.Application;
using Shop.Domain.CategoryAgg.Services;
using Shop.Domain.CategoryAgg;

namespace Shop.Application.Categories.AddChild
{
    public class AddChildCategoryCommandHandler : IBaseCommandHandler<AddChildCategoryCommand>
    {
        private readonly ICategoryRepository _repository;
        private readonly ICategoryDomainServices _domainServices;

        public AddChildCategoryCommandHandler(ICategoryRepository repository, ICategoryDomainServices domainServices)
        {
            _repository = repository;
            _domainServices = domainServices;
        }
        public async Task<OperationResult> Handle(AddChildCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetTracking(request.ParentId);
            if (category == null)
                return OperationResult.NotFound();

            category.AddChild(request.Title, request.Slug, request.SeoData, _domainServices);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
