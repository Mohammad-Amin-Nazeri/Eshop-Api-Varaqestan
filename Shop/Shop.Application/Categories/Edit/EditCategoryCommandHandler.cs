using Common.Application;
using Shop.Domain.CategoryAgg.Services;
using Shop.Domain.CategoryAgg;

namespace Shop.Application.Categories.Edit
{
    public class EditCategoryCommandHandler : IBaseCommandHandler<EditCategoryCommand>
    {
        private readonly ICategoryRepository _repository;
        private readonly ICategoryDomainServices _domainServices;

        public EditCategoryCommandHandler(ICategoryRepository repository, ICategoryDomainServices domainServices)
        {
            _repository = repository;
            _domainServices = domainServices;
        }
        public async Task<OperationResult> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
        {
            var category= await _repository.GetTracking(request.Id);
            if (category == null)
                return OperationResult.NotFound();

            category.Edit(request.Title,request.Slug,request.SeoData,_domainServices);
            await _repository.Save();
            return OperationResult.Success();   

        }
    }
}
