using FluentValidation;

namespace ProductCatalog.Application.Features.AddProduct;

/// <summary>
/// Validator for the AddProductCommand that ensures all required fields are valid.
/// </summary>
public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AddProductCommandValidator"/> class.
    /// </summary>
    public AddProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters");

        RuleFor(x => x.PriceAmount)
            .GreaterThan(0).WithMessage("Price must be greater than 0");

        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage("Currency is required")
            .Length(3).WithMessage("Currency must be 3 characters");

        RuleFor(x => x.InitialStock)
            .GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative");
    }
}
