using CvCheck.Backend.Features.Platform.Ping;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace CvCheck.Backend.Tests;

public sealed class ApplicationPipelineTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task Mediator_Should_HandlePingQuery()
    {
        using var client = factory.CreateClient();
        using var scope = factory.Services.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        var result = await mediator.Send(new PingQuery("ping"));

        result.Message.Should().Be("ping");
    }

    [Fact]
    public async Task Mediator_Should_ValidatePingQuery()
    {
        using var client = factory.CreateClient();
        using var scope = factory.Services.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        var action = async () => await mediator.Send(new PingQuery(string.Empty));

        await action.Should().ThrowAsync<ValidationException>();
    }
}
