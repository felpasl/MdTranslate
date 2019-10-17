namespace MdTranslator.Lib
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
