using System.Net;
using System.Net.Http.Json;
using CvCheck.Backend.Contracts;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CvCheck.Backend.Tests;

public sealed class PlatformEndpointsTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task GetHealth_Should_ReturnOk()
    {
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/health");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetPlatform_Should_ReturnPlatformMetadata()
    {
        using var client = factory.CreateClient();

        var response = await client.GetFromJsonAsync<PlatformInfoResponse>("/api/platform");

        response.Should().NotBeNull();
        response!.Name.Should().Be("CvCheck API");
        response.Environment.Should().NotBeNullOrWhiteSpace();
        response.Version.Should().NotBeNullOrWhiteSpace();
    }
}
