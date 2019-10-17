using System;
using System.Collections.Generic;
using System.Text;

namespace MdTranslate.Lib.TranslateProviders
{
    public class Amazon: ITranslateProvider
    {
        private string key;

        public Amazon(string key)
        {
            this.key = key;
        }
        public TranslateText Translate(TranslateText text, string originalLanguage, string destinationLanguage)
        {
            throw new NotImplementedException();
        }
    }
}
