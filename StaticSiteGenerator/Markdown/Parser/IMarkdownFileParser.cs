using System.Collections.Generic;
using StaticSiteGenerator.Markdown.BlockElement;

namespace StaticSiteGenerator.Markdown.Parser
{
    public interface IMarkdownFileParser
    {
        IList<IBlockElement> ReadFile(string filePath);
        IEnumerable<IMarkdownFile> ReadFiles(IEnumerable<string> filePaths);
    }
}
