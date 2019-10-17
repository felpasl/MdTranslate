using Markdig;
using Markdig.Helpers;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using System;
using System.Collections.Generic;
using System.Text;

namespace MdTranslate.Lib
{
    public class MarkdownParser
    {
        private int lastLine = 0;
        public TranslateText Parse(string document)
        {
            var mdDocument = Markdown.Parse(document);
            TranslateText translateLines = new TranslateText();
            foreach (MarkdownObject item in mdDocument)
            {
                var line = TratarItem(item);
                if (line != null)
                    translateLines.AddRange(line);
            }
            translateLines.ForEach((item) => Console.WriteLine($"{item.Line} - {item.OrigTerm}"));
            return translateLines;
        }

        private List<TranslateLine> TratarItem(MarkdownObject item)
        {
            if (item.Line != 0)
            {
                lastLine = item.Line;
            }
            List<TranslateLine> lineCollection = new List<TranslateLine>();
            switch (item)
            {
                case ParagraphBlock paragraph:
                    if (paragraph.Inline != null)
                    {
                        var paragraphs = paragraph.Inline.GetEnumerator();
                        while (paragraphs.MoveNext())
                            if (paragraphs.Current != null)
                                lineCollection.AddRange(TratarItem(paragraphs.Current));

                    }
                    break;

                case ListBlock listBlock:

                    if (listBlock != null && listBlock.Count > 0)
                        foreach (ListItemBlock listItem in listBlock)
                        {
                            var line = TratarItem(listItem);
                            if (line != null)
                                lineCollection.AddRange(line);
                        }
                    break;
                case ContainerBlock containerBlock:
                    if (containerBlock != null && containerBlock.Count > 0)
                        foreach (MarkdownObject listItem in containerBlock)
                        {
                            var line = TratarItem(listItem);
                            if (line != null)
                                lineCollection.AddRange(line);
                        }
                    break;
                case HeadingBlock headingBlock:
                    return TratarItem(headingBlock.Inline);
                case HtmlBlock htmlBlock:
                    foreach (StringLine line in htmlBlock.Lines)
                    {
                        lineCollection.Add(TratarItem(line));
                    }
                    break;
                case EmphasisInline emphasisInline:
                    var emphasisInlines = emphasisInline.GetEnumerator();
                    while (emphasisInlines.MoveNext())
                        if (emphasisInlines.Current != null)
                            lineCollection.AddRange(TratarItem(emphasisInlines.Current));
                    break;
                case LiteralInline literalInline:
                    lineCollection.Add(new TranslateLine() { Line = lastLine, OrigTerm = literalInline.ToString() });
                    break;
                case DelimiterInline delimiterInline:
                    lineCollection.Add(new TranslateLine() { Line = lastLine, OrigTerm = delimiterInline.ToString() });
                    break;
                case LineBreakInline lineBreakInline:
                    lastLine++;
                    break;
                case LinkInline linkInline:
                    Console.WriteLine(linkInline.Title);
                    lineCollection.Add(new TranslateLine() { Line = lastLine, OrigTerm = linkInline.Title });
                    break;
                case ContainerInline containerInline:
                    var containerInlines = containerInline.GetEnumerator();
                    while (containerInlines.MoveNext())
                        if (containerInlines.Current != null)
                            lineCollection.AddRange(TratarItem(containerInlines.Current));
                    break;
                case HtmlInline htmlInline:
                    Console.WriteLine(htmlInline);
                    break;
                default:
                    return lineCollection;
            }
            return lineCollection;
        }
        TranslateLine TratarItem(StringLine line)
        {
            TranslateLine translateLine = new TranslateLine();
            translateLine.Line = line.Line;
            translateLine.OrigTerm = line.ToString();
            return translateLine;
        }

    }
}
