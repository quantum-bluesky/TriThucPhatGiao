using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Razor.Language;
using Xunit;

namespace ThienTriTue.Theme.Tests;

public class ViewCompilationTests
{
    private static readonly string SolutionRoot = Path.GetFullPath(
        Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));

    private static readonly string ViewsRoot = Path.Combine(
        SolutionRoot, "ThienTriTue.Web", "ThienTriTue.Theme","Views");

    [Fact]
    public void AllViewsCanBeParsedByRazorEngine()
    {
        Assert.True(Directory.Exists(ViewsRoot), $"Views folder not found at: {ViewsRoot}");

        var viewFiles = Directory.GetFiles(ViewsRoot, "*.cshtml", SearchOption.AllDirectories);
        Assert.NotEmpty(viewFiles);

        var fileSystem = RazorProjectFileSystem.Create(ViewsRoot);
        var configuration = RazorConfiguration.Create(
            RazorLanguageVersion.Latest,
            "TestSetup",
            Array.Empty<RazorExtension>());

        var engine = RazorProjectEngine.Create(configuration, fileSystem, builder =>
        {
            builder.SetNamespace("ThienTriTue.Theme.Views");
        });

        var failures = new List<string>();

        foreach (var file in viewFiles)
        {
            var relativePath = "/" + Path.GetRelativePath(ViewsRoot, file).Replace('\\', '/');
            var projectItem = fileSystem.GetItem(relativePath);

            if (!projectItem.Exists)
            {
                failures.Add($"Unable to load Razor project item for {relativePath}");
                continue;
            }

            var codeDocument = engine.Process(projectItem);
            var errors = codeDocument.GetCSharpDocument().Diagnostics
                .Where(d => d.Severity == RazorDiagnosticSeverity.Error)
                .ToArray();

            if (errors.Length > 0)
            {
                failures.Add($"{relativePath}:{Environment.NewLine}{string.Join(Environment.NewLine, errors.Select(e => e.ToString()))}");
            }
        }

        if (failures.Count > 0)
        {
            var message = "Razor parsing failed for the following views:" +
                          Environment.NewLine +
                          string.Join(Environment.NewLine + "---" + Environment.NewLine, failures);

            Assert.True(false, message);
        }
    }
}
