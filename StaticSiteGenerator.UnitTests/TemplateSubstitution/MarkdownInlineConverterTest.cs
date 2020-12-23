using System;
using System.Collections.Generic;
using Xunit;

using StaticSiteGenerator.TemplateSubstitution;
using StaticSiteGenerator.TemplateSubstitution.MarkdownHtmlConverters;
using StaticSiteGenerator.TemplateSubstitution.InlineConverterStrategies;
using StaticSiteGenerator.Markdown.InlineElement;
using StaticSiteGenerator.Utilities.StrategyPattern;
using Test.Markdown.Parser;

namespace Test.TemplateSubstitution
{
    public class MarkdownInlineConverterTest
    {
        private StrategyCollectionMockFactory mockFactory => new StrategyCollectionMockFactory();

        [Fact]
        public void ConverterCallsCorrectStrategyWhenExists()
        {
            TestConverter testConverter = new TestConverter();
            Dictionary<string, IInlineConverterStrategy> strategyMappings = new Dictionary<string, IInlineConverterStrategy>
            {
                { nameof(Text), testConverter }
            };

            Moq.Mock<StrategyCollection<IInlineConverterStrategy>> mock = mockFactory.Get<IInlineConverterStrategy>(strategyMappings);
            var converter = new MarkdownInlineConverter(mock.Object);

            var inline = new Text();

            converter.Convert(inline);

            Assert.True(testConverter.ConverterCalled);
        }

        [HtmlConverterFor(nameof(Text))]
        private class TestConverter: IInlineConverterStrategy
        {
            public bool ConverterCalled = false;

            public string Convert(IInlineElement _)
            {
                ConverterCalled = true;
                return String.Empty;
            }
        }
      }
}
