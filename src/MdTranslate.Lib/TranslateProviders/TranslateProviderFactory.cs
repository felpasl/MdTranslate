using System;
using System.Collections.Generic;
using System.Text;

namespace MdTranslate.Lib.TranslateProviders
{
    public class TranslateProviderFactory
    {
        public static ITranslateProvider CreateProvider(TranslateOptions translateOptions)
        {
            switch (translateOptions.Provider)
            {
                case TranslateOptions.ProviderEnum.Amazon: return new Amazon(translateOptions.Key);
                case TranslateOptions.ProviderEnum.Azure: return new Azure.AzureProvider(translateOptions.Key, translateOptions.Endpoint);
                case TranslateOptions.ProviderEnum.Google: return new Google(translateOptions.Key);
                default: return new Azure.AzureProvider(translateOptions.Key, translateOptions.Endpoint);
            }
        }
    }
}
