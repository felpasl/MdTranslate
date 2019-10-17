using System;
using System.Collections.Generic;
using System.Text;

namespace MdTranslator.Lib.TranslateProviders
{
    public interface ITranslateProvider
    {
        TranslateText Translate(TranslateText text, string originalLanguage, string destinationLanguage);
    }
}
