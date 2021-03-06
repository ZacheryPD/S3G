using System.Collections.Generic;

using Moq;
using Xunit;
using StaticSiteGenerator.Markdown.InlineElement;
using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;
using Test.Markdown.Parser;
using StaticSiteGenerator.UnitTests.Doubles;
using StaticSiteGenerator.MarkdownHtmlConversion.BlockConverterStrategies;
using Test.TemplateSubstitution.BlockConverterStrategies;
using StaticSiteGenerator.TemplateSubstitution.TagCollection;

namespace Test.MarkdownHtmlConversion.BlockConverterStrategies
{

    public class TestHeaderBlockConverterStrategy
    {
        private StrategyCollectionMockFactory strategyCollectionMockFactory => new StrategyCollectionMockFactory();
        private TemplateCollectionMockFactory templateCollectionMockFactory => new TemplateCollectionMockFactory();
        private MarkdownInlineHtmlConverterMockFactory inlineConverterMockFactory => new MarkdownInlineHtmlConverterMockFactory();

        [Fact]
        public void Test()
        {
            var inlineConverterMock = inlineConverterMockFactory.Get("TestText");

            Mock<ITemplateTagCollection> templateReader = templateCollectionMockFactory
                .Get(new List<TemplateTag> {
                        new TemplateTag {
                            Template ="<h1>{{}}</h1>",
                            Type = TagType.Header1
                        }
                    });

            var templateFillerMock = TemplateFillerMockFactory.Get();

            var converter = new HeaderHtmlConverterStrategy(
                inlineConverterMock.Object,
                templateReader.Object,
                templateFillerMock.Object);

            var headerBlock = new Header
            {
                Inlines = new List<IInlineElement>()
            };
            var result = converter.Convert(headerBlock);

            Assert.Equal("<h1>TestText</h1>", result);
        }
    }
}
