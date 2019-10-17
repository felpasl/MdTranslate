using MdTranslate.Lib.TranslateProviders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MdTranslate.Lib
{
    public class TranslateMarkdown
    {
        MarkdownParser Parser;
        ITranslateProvider Provider;
        public TranslateMarkdown(TranslateOptions options)
        {
            if (options == null)
                throw new ArgumentNullException("TranslateOptions");
            if (options.Key == null)
                throw new ArgumentNullException("TranslateOptions.Key");
            this.Parser = new MarkdownParser();
            this.Provider = TranslateProviderFactory.CreateProvider(options);
        }

        public string Translate(string document, string destinationLanguage)
        {
            return Translate(document, null, destinationLanguage);
        }
        public string Translate(string document, string originalLanguage, string destinationLanguage)
        {
            TranslateText text = Parser.Parse(document);
            text = Provider.Translate(text, originalLanguage, destinationLanguage);
            return text.Replace(document);
        }
    }
}
