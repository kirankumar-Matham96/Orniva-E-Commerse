using FluentValidation;
using OrnivaApi.DTOs.Product;

namespace OrnivaApi.Validators.Product
{
    public class CreateProductValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name)
                .Must(name => !string.IsNullOrWhiteSpace(name))
                .WithMessage("Product name is required.")
                .MaximumLength(200)
                .WithMessage("Product name cannot exceed 200 characters.");

            RuleFor(x => x.Slug)
                .Must(slug => !string.IsNullOrWhiteSpace(slug))
                .WithMessage("Slug is required.")
                .MaximumLength(250)
                .WithMessage("Slug cannot exceed 250 characters.")
                .Matches("^[a-z0-9]+(?:-[a-z0-9]+)*$")
                .WithMessage("Slug can contain only lowercase letters, numbers, and hyphens.");

            RuleFor(x => x.Description)
                .Must(description => !string.IsNullOrWhiteSpace(description))
                .WithMessage("Description is required.")
                .MaximumLength(5000)
                .WithMessage("Description cannot exceed 5000 characters.");

            RuleFor(x => x.ShortDescription)
                .MaximumLength(500)
                .WithMessage("Short description cannot exceed 500 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.ShortDescription));

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than zero.");

            RuleFor(x => x.DiscountPrice)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Discount price cannot be negative.")
                .LessThan(x => x.Price)
                .WithMessage("Discount price must be less than the original price.")
                .When(x => x.DiscountPrice.HasValue);

            RuleFor(x => x.StockQuantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Stock quantity cannot be negative.");

            RuleFor(x => x.Brand)
                .MaximumLength(100)
                .WithMessage("Brand cannot exceed 100 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Brand));

            RuleFor(x => x.SKU)
                .Must(sku => !string.IsNullOrWhiteSpace(sku))
                .WithMessage("SKU is required.")
                .MaximumLength(100)
                .WithMessage("SKU cannot exceed 100 characters.");

            // For using direct links from external sources
            RuleFor(x => x.ImageUrl)
                .MaximumLength(500)
                .WithMessage("Image URL cannot exceed 500 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.ImageUrl));

            // For using Azure Blob Storage or AWS S3 Buckets or Cloudinary
            //RuleFor(x => x.ImageUrl)
            //    .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            //    .When(x => !string.IsNullOrWhiteSpace(x.ImageUrl));

            RuleFor(x => x.CategoryId)
                .GreaterThan(0)
                .WithMessage("A valid category must be selected.");
        }
    }
}