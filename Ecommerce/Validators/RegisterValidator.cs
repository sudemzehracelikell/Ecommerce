using Ecommerce.Models;
using FluentValidation;

namespace ECommerce.Validators;

public class RegisterValidator : AbstractValidator<RegisterModel>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x =>x.Name).NotEmpty();
        RuleFor(x=>x.PhoneNumber).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
        
    }
}