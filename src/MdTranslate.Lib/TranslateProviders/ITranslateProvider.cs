using System;
using System.Collections.Generic;
using System.Text;

namespace MdTranslate.Lib.TranslateProviders
{
    public interface ITranslateProvider
    {
        TranslateText Translate(TranslateText text, string originalLanguage, string destinationLanguage);
    }
}
