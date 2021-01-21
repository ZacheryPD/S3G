using System;
using System.Collections.Generic;
using Moq;
using StaticSiteGenerator.Markdown;
using StaticSiteGenerator.Markdown.BlockElement;

namespace StaticSiteGenerator.UnitTests.Doubles
{
    public static class MarkdownFileParserMockFactory
    {
        public static Mock<IMarkdownFileParser> Get(IDictionary<string, IList<IBlockElement>> input)
        {
            var mock = new Mock<IMarkdownFileParser>();

            mock
                .Setup(m => m.ReadFile(It.IsAny<string>()))
                .Returns<string>(fileName => input[fileName]);

            return mock;
        }
    }
}
