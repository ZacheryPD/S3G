using Xunit;
using Microsoft.Extensions.DependencyInjection;

namespace StaticSiteGenerator.IntegrationTests.Tests
{
    public class YamlMetadataIntegrationTests: IntegrationTestBase
    {
        [Fact]
        public void YamlMetadataShouldParseCorrectly()
        {
            // Notice that the --- is the first thing on the line. It must be so
            // for the library to work.
            const string yamlFile = @"---
publish_date: ""12/31/2020""
---

This is some text!";
            // A dictionary mapping file paths to contents
            FileSystemCache.Add("templates/template/tag_templates/p.html", "<p>{{}}</p>");
            FileSystemCache.Add("templates/template/site_template.html", "<html>{{}}</html>");
            FileSystemCache.Add("input/file1.md", yamlFile);

            ServiceProvider.GetService<StaticSiteGenerator>().Start();

            const string expectedContent = @"<html><p>This is some text!</p></html>";
            const string expectedName = "output/file1.html";

            Assert.True(FileSystemCache.ContainsKey(expectedName));
            Assert.Equal(expectedContent, FileSystemCache[expectedName]);
        }
    }
}
