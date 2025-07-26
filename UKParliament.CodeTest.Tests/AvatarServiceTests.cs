using System.Net;
using FakeItEasy;
using Microsoft.Extensions.Caching.Memory;
using UKParliament.CodeTest.Services.Services;
using Xunit;

namespace UKParliament.CodeTest.Tests;

public class AvatarServiceTests
{
    [Fact]
    public async Task GetAvatar_CallsUrlWithSeed_Correctly()
    {
        var baseUrl = "https://test.com/personas/png";
        var fakeCache = A.Fake<IMemoryCache>();
        var httpMessageHandler = A.Fake<HttpMessageHandler>();
        A.CallTo(httpMessageHandler)
            .Where(call => call.Method.Name == "SendAsync")
            .WithReturnType<Task<HttpResponseMessage>>()
            .Returns(
                Task.FromResult(
                    new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent("test response"),
                    }
                )
            );

        var httpClient = new HttpClient(httpMessageHandler) { BaseAddress = new Uri(baseUrl) };

        var service = new AvatarService(httpClient, fakeCache);

        _ = await service.GetAvatar("Alex");

        A.CallTo(httpMessageHandler)
            .Where(call =>
                call.Method.Name == "SendAsync"
                && ((HttpRequestMessage)call.Arguments[0]!)!.RequestUri!.ToString()
                    == baseUrl + "?size=150&mouth=smile&eyes=open&seed=Alex"
            )
            .MustHaveHappenedOnceExactly();
    }
}
