using System.Net;
using AngleSharp.Html.Parser;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Abstractions;
using System.Text;

namespace ThienTriTue.Theme.Tests;

public class ThienTriTueWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");
    }
}

public class WebSmokeTests : IClassFixture<ThienTriTueWebApplicationFactory>
{
    private static readonly HtmlParser HtmlParser = new();

    private readonly HttpClient _client;
    private readonly ITestOutputHelper _output;

    public record PageExpectation(string Path, string TitleFragment, IReadOnlyList<string> RequiredSelectors)
    {
        public override string ToString() => Path;
    }

    public WebSmokeTests(ThienTriTueWebApplicationFactory factory, ITestOutputHelper output)
    {
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });

        _output = output;
    }

    public static TheoryData<PageExpectation> PublicPages => new()
    {
        {
            new PageExpectation(
                "/",
                "Trang Chủ",
                new[]
                {
                    "a[href='/giao-duc']",
                    "a[href='/phat-giao']",
                    "a[href='/nhan-qua']"
                })
        },
        {
            new PageExpectation(
                "/giao-duc",
                "Khoa Học",
                new[]
                {
                    "h1",
                    "a[href='/giao-duc/nghien-cuu-pctt']"
                })
        },
        {
            new PageExpectation(
                "/phat-giao",
                "Phật Giáo",
                new[]
                {
                    "h1",
                    "a[href='/phat-giao/bao-cao-chuyen-sau']"
                })
        },
        {
            new PageExpectation(
                "/nhan-qua",
                "Nhân Quả",
                new[]
                {
                    "h1",
                    "a[href='/nhan-qua/long-biet-on']"
                })
        }
    };

    [Theory]
    [MemberData(nameof(PublicPages))]
    public async Task PublicPages_RenderWithoutServerErrors(PageExpectation expectation)
    {
        using var response = await _client.GetAsync(expectation.Path);
        var html = await response.Content.ReadAsStringAsync();
        var document = await HtmlParser.ParseDocumentAsync(html);
        var pageText = NormalizeForComparison(document.Body?.TextContent ?? string.Empty);
        var pageTitle = NormalizeForComparison(document.Title ?? string.Empty);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.False(string.IsNullOrWhiteSpace(html), $"Page '{expectation.Path}' rendered empty content");
        Assert.False(string.IsNullOrWhiteSpace(pageText), $"Page '{expectation.Path}' rendered without visible content.");

        Assert.DoesNotContain("Exception", html, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Stack trace", html, StringComparison.OrdinalIgnoreCase);
        Assert.Contains(NormalizeForComparison(expectation.TitleFragment), pageTitle, StringComparison.OrdinalIgnoreCase);

        foreach (var selector in expectation.RequiredSelectors)
        {
            var element = document.QuerySelector(selector);
            Assert.True(element != null, $"Expected to find selector '{selector}' on page '{expectation.Path}'.");
        }
    }

    [Theory]
    [MemberData(nameof(PublicPages))]
    public async Task PublicPages_ReportBrokenLinksAsWarnings(PageExpectation expectation)
    {
        using var response = await _client.GetAsync(expectation.Path);
        var html = await response.Content.ReadAsStringAsync();
        var document = await HtmlParser.ParseDocumentAsync(html);

        var testedLinks = new HashSet<Uri>();
        var warnings = new List<string>();

        foreach (var anchor in document.QuerySelectorAll("a"))
        {
            var href = anchor.GetAttribute("href");
            if (string.IsNullOrWhiteSpace(href) || href.StartsWith("#", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            if (href.StartsWith("mailto:", StringComparison.OrdinalIgnoreCase) ||
                href.StartsWith("javascript:", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            if (href.StartsWith("http", StringComparison.OrdinalIgnoreCase) &&
                !href.StartsWith(_client.BaseAddress!.GetLeftPart(UriPartial.Authority), StringComparison.OrdinalIgnoreCase))
            {
                // External link - log that it was skipped for clarity.
                _output.WriteLine($"Skipped external link '{href}' found on '{expectation.Path}'.");
                continue;
            }

            var targetUri = href.StartsWith("http", StringComparison.OrdinalIgnoreCase)
                ? new Uri(href)
                : new Uri(_client.BaseAddress!, href);

            if (!testedLinks.Add(targetUri))
            {
                continue;
            }

            using var linkResponse = await _client.GetAsync(targetUri);

            if ((int)linkResponse.StatusCode >= 400)
            {
                warnings.Add($"{expectation.Path} -> {targetUri} returned {(int)linkResponse.StatusCode} {linkResponse.StatusCode}");
            }
        }

        if (warnings.Count > 0)
        {
            _output.WriteLine($"Broken link warnings for '{expectation.Path}':");
            foreach (var warning in warnings)
            {
                _output.WriteLine(warning);
            }
        }

        if (testedLinks.Count == 0)
        {
            _output.WriteLine($"No navigable links were discovered on '{expectation.Path}'.");
        }
    }

    private static string NormalizeForComparison(string value)
    {
        return value.Normalize(NormalizationForm.FormC);
    }
}
