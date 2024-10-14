using Core.Entities;

namespace Core.Specifications;

public class ProductSpecification : BaseSpecification<Product>
{
    public ProductSpecification(ProductSpecParams specParams) : base(x =>
        (string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search)) &&
        (specParams.Brands.Count == 0 || specParams.Brands.Contains(x.Brand)) &&
        (specParams.Types.Count == 0 || specParams.Types.Contains(x.Type)))
    {
        int skip = (specParams.PageIndex - 1) * specParams.PageSize;
        ApplyPaging(skip, specParams.PageSize);

        switch (specParams.Sort)
        {
            case "priceAsc":
                AddOrderByExpression(x => x.Price);
                break;
            case "priceDesc":
                AddOrderByDescendingExpression(x => x.Price);
                break;
            default:
                AddOrderByExpression(x => x.Name);
                break;
        }
    }
}