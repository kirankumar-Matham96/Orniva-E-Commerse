using FluentValidation;
using OrnivaApi.DTOs.Category;

namespace OrnivaApi.Validators.Category
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryValidator()
        {

            RuleFor(x => x.Name)
                            .Must(name => !string.IsNullOrWhiteSpace(name))
                            .WithMessage("Category name is required.")
                            .MaximumLength(100)
                            .WithMessage("Category name cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage("Description cannot exceed 500 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Description));
        }
    }
}
