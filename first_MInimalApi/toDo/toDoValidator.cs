using FluentValidation;
namespace first_MinimalApi;

public class toDoValidator : AbstractValidator<toDo>
{
    public toDoValidator()
    {
        RuleFor(t => t.Value)
            .NotEmpty()
            .MinimumLength(5)
            .WithMessage("Value of a todo must by at least 5 characters");
    }
}
