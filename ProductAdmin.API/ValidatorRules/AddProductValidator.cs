namespace ProductAdmin.API.ValidatorRules;

using FluentValidation;
using ProductAdmin.API.Controllers.V1.UseCases.Inventory.Request;

public class AddProductValidator : AbstractValidator<AddProductRequest>
{
    public AddProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required");

        RuleFor(x => x.Price)
            .NotEmpty()
            .WithMessage("Price is required")
            .GreaterThan(0)
            .WithMessage("Product price must be greater than zero");

        RuleFor(x => x.Stock)
            .NotEmpty()
            .WithMessage("Stock is required")
            .GreaterThan(0)
            .WithMessage("Product stock must be greater than zero");

        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .WithMessage("CategoryId is required")
            .GreaterThan(0)
            .WithMessage("You must select a category for the product");
    }
}