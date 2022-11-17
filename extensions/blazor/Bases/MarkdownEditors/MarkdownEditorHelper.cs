using Markdig;

namespace FMFT.Extensions.Blazor.Bases.MarkdownEditors
{
    public class MarkdownEditorHelper
    {
        public static string ParseToHtml(string markdown, bool disableHtml = true)
        {
            if (string.IsNullOrEmpty(markdown))
            {
                return string.Empty;
            }

            MarkdownPipelineBuilder builder = new MarkdownPipelineBuilder()
                    .UseEmojiAndSmiley()
                    .UseAdvancedExtensions()
                    .UseAutoLinks();

            if (disableHtml)
                builder.DisableHtml();

            return Markdown.ToHtml(markdown, builder.Build());
        }
    }
}
