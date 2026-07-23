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
        }
    }
}