using FluentValidation;
using GamePond.Application.Games.Models;

namespace GamePond.Application.Games.Validators;

public class CreateGameCommandValidator : AbstractValidator<CreateGameCommand>
{
    public CreateGameCommandValidator()
    {
        RuleFor(g => g.Title).NotEmpty().MaximumLength(200);
        RuleFor(g => g.Description).MaximumLength(2000);
    }
}