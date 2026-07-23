using FluentValidation;
using OrnivaApi.DTOs.Common;

namespace OrnivaApi.Validators.Common
{
    public class ProductQueryParametersValidator : AbstractValidator<ProductQueryParameters>
    {
        public ProductQueryParametersValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0)
                .WithMessage("Page number must be greater than zero.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100)
                .WithMessage("Page size must be between 1 and 100.");

            RuleFor(x => x.SortOrder)
                .Must(order =>
                    string.IsNullOrWhiteSpace(order) ||
                    order.Equals("asc", StringComparison.OrdinalIgnoreCase) ||
                    order.Equals("desc", StringComparison.OrdinalIgnoreCase))
                .WithMessage("Sort order must be either 'asc' or 'desc'.");

            RuleFor(x => x.SortBy)
                .Must(sortBy =>
                    string.IsNullOrWhiteSpace(sortBy) ||
                    new[]
                    {
                        "name",
                        "price",
                        "stock",
                        "brand"
                    }.Contains(sortBy.ToLower()))
                .WithMessage("SortBy must be one of: name, price, stock, brand.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0)
                .When(x => x.CategoryId.HasValue)
                .WithMessage("CategoryId must be greater than zero.");

            RuleFor(x => x.MinPrice)
                .GreaterThanOrEqualTo(0)
                .When(x => x.MinPrice.HasValue)
                .WithMessage("Minimum price cannot be negative.");

            RuleFor(x => x.MaxPrice)
                .GreaterThanOrEqualTo(0)
                .When(x => x.MaxPrice.HasValue)
                .WithMessage("Maximum price cannot be negative.");

            RuleFor(x => x)
                .Must(x =>
                    !x.MinPrice.HasValue ||
                    !x.MaxPrice.HasValue ||
                    x.MinPrice <= x.MaxPrice)
                .WithMessage("Minimum price cannot be greater than maximum price.");
        }
    }
}