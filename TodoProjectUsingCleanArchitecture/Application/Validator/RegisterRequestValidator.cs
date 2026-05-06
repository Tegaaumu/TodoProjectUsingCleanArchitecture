using FluentValidation;
using TodoProjectUsingCleanArchitecture.Contract.Request;

namespace TodoProjectUsingCleanArchitecture.Application.Validator;
public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Username is required.")
                .MinimumLength(3)
                .WithMessage("Username must be at least 3 characters.")
                .Must(x => !x.Any(char.IsWhiteSpace))
                .WithMessage("Username cannot contain spaces.");


            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Please enter a valid email address.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .MinimumLength(8)
                .WithMessage("Password must be at least 8 characters.")
                .Matches("[A-Z]")
                .WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[0-9]")
                .WithMessage("Password must contain at least one number.");
        }
    }