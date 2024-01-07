using ETradeAPI.Application.ViewModels.Products;
using FluentValidation;

namespace ETradeAPI.Application.Validators.Products;

public class CreateProductValidator : AbstractValidator<VM_Create_Product>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name field can not be empty")
            .MaximumLength(150)
            .MinimumLength(5)
            .WithMessage("Name field must be between 5 and 150 characters");

        RuleFor(x => x.Stock)
            .NotEmpty()
            .NotNull()
            .WithMessage("Stock field can not be empty")
            .Must(x => x >= 0)
            .WithMessage("Stock field must be greater than 0");

        RuleFor(x => x.Price)
            .NotEmpty()
            .NotNull()
            .WithMessage("Price field can not be empty")
            .Must(x => x >= 0)
            .WithMessage("Price field must be greater than 0");
    }
}