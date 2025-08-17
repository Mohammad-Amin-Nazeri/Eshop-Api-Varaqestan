using Common.Application;
using Shop.Domain.CategoryAgg;
using Shop.Domain.CategoryAgg.Services;

namespace Shop.Application.Categories.Create
{
    public class CreateCategoryCommandHandler : IBaseCommandHandler<CreateCategoryCommand>
    {
       private readonly ICategoryRepository _repository;
       private readonly ICategoryDomainServices _domainServices;

        public CreateCategoryCommandHandler(ICategoryRepository repository, ICategoryDomainServices domainServices)
        {
            _repository = repository;
            _domainServices = domainServices;
        }

        public async Task<OperationResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category (request.Title , request.Slug , request.SeoData, _domainServices);
             _repository.Add(category);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
