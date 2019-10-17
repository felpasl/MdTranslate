using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MdTranslator.Lib
{
    public class TranslateText : List<TranslateLine>
    {
        public string OriginalLanguage { get; set; }
        public string DestinationLanguage { get; set; }

        public string Replace(string document)
        {
            var lines = Regex.Split(document, "\r\n|\r|\n");
            foreach (var item in this)
            {
                if (!string.IsNullOrWhiteSpace(item.OrigTerm) && !string.IsNullOrWhiteSpace(item.TranslatedTerm))
                    lines[item.Line] = lines[item.Line].Replace(item.OrigTerm, item.TranslatedTerm);
            }
            return string.Join("\r\n", lines);
        }

        public void WriteTranslate(string[] translated)
        {
            foreach (string translatedItem in translated)
            {
                if (translatedItem != null)
                {
                    var index = translated.ToList().IndexOf(translatedItem);
                    if (this.ElementAtOrDefault(index) != null)
                        this[index].TranslatedTerm = translatedItem;
                }
            }
        }
    }
}
