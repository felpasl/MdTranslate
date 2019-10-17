using System;
using System.Collections.Generic;
using System.Text;

namespace MdTranslate.Lib
{
    public class TranslateOptions
    {
        public string Key { get; set; }
        public ProviderEnum Provider { get; set; }
        public string Endpoint { get; set; }
        public enum ProviderEnum
        {
            Amazon,
            Azure,
            Google,
        };
    }
 


}
