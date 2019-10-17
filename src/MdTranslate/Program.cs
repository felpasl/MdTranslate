using Markdig;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace MdTranslate
{
    class Program
    {
        /// <summary>
        /// Translate markdown languages
        /// </summary>
        /// <param name="mdInput">Input Path of markdown</param>
        /// <param name="mdOutput">Output file of translated markdown, if not specified input.language.md is the new file</param>
        /// <param name="outputLanguage">language of output file translated(en-us, en, pt-br)</param>
        /// <param name="inputLanguage">language of input file(en-us, en, pt-br)</param>
        /// <param name="provider">Azure, Amazon, Google</param>
        /// <param name="key">Api key on provider or Environment Variable *PROVIDER*_TRANSLATOR_TEXT_SUBSCRIPTION_KEY i.e. AZURE_TRANSLATOR_TEXT_SUBSCRIPTION_KEY</param>
        /// <param name="endpoint">Url of translation api endpoint or Environment Variable *PROVIDER*_TRANSLATOR_TEXT_ENDPOINT i.e. AZURE_TRANSLATOR_TEXT_ENDPOINT</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Security", "SCS0018:Path traversal: injection possible in {1} argument passed to '{0}'", Justification = "Validation on line 27-28")]
        static void Main(string mdInput, string mdOutput, string outputLanguage, string inputLanguage, Lib.TranslateOptions.ProviderEnum provider, string key, string endpoint)
        {
            string inputFile = mdInput;
            string outputFile = mdOutput;
            string mdText = string.Empty;
            char[] InvalidFilenameChars = Path.GetInvalidFileNameChars();
        
            if (!(mdInput.IndexOfAny(InvalidFilenameChars) >= 0))
            {
                if (File.Exists(Environment.CurrentDirectory + Path.DirectorySeparatorChar + mdInput))
                {
                    mdText = File.ReadAllText(Environment.CurrentDirectory + Path.DirectorySeparatorChar + mdInput);

                    Lib.TranslateMarkdown translateMarkdown = new Lib.TranslateMarkdown(new Lib.TranslateOptions() { Provider = provider, Key = key, Endpoint = endpoint });

                    string mdOutputText = translateMarkdown.Translate(mdText, inputLanguage, outputLanguage);

                    Console.WriteLine(mdOutputText);

                    string filename;
                    if (string.IsNullOrWhiteSpace(mdOutput))
                    {
                        filename = Path.GetFileNameWithoutExtension(mdInput) + "." + outputLanguage + ".md";
                    }
                    else {
                        filename = mdOutput;
                    }
                    File.WriteAllText(filename, mdOutputText, Encoding.UTF8);
                }
            }
            else
            {
                Console.WriteLine("Input File is not valid");
            }

            Console.ReadLine();

        }
    }
}
