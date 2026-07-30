using GamePond.Domain.Games;

namespace GamePond.Application.Games.Models;

using FluentValidation;

public class CreateGameCommandValidator : AbstractValidator<CreateGameCommand>
{
    public CreateGameCommandValidator()
    {
        RuleFor(g => g.Title).NotEmpty().MaximumLength(200);
        RuleFor(g => g.Description).MaximumLength(2000);
    }
}