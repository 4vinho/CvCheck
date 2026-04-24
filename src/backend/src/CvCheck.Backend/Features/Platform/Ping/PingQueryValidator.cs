using FluentValidation;

namespace CvCheck.Backend.Features.Platform.Ping;

public sealed class PingQueryValidator : AbstractValidator<PingQuery>
{
    public PingQueryValidator()
    {
        RuleFor(request => request.Message)
            .NotEmpty()
            .MaximumLength(128);
    }
}
