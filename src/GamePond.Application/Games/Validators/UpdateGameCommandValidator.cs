using FluentValidation;
using GamePond.Application.Games.Models;

namespace GamePond.Application.Games.Validators;

public class UpdateGameCommandValidator : AbstractValidator<UpdateGameCommand>
{
    public UpdateGameCommandValidator()
    {
        RuleFor(g => g.Title).NotEmpty().MaximumLength(200);
        RuleFor(g => g.Description).MaximumLength(2000);
    }
}