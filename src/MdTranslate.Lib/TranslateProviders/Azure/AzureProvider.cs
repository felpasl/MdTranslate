using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MdTranslate.Lib.TranslateProviders.Azure
{
    public class AzureProvider : ITranslateProvider
    {
        private string key;
        private const string key_var = "AZURE_TRANSLATOR_TEXT_SUBSCRIPTION_KEY";
        private string subscriptionKey = Environment.GetEnvironmentVariable(key_var);

        private const string endpoint_var = "AZURE_TRANSLATOR_TEXT_ENDPOINT";
        private string endpoint = Environment.GetEnvironmentVariable(endpoint_var);

        public AzureProvider(string key, string endpoint)
        {
            if (subscriptionKey == null)
                this.subscriptionKey = key;
            if (this.endpoint == null)
                this.endpoint = endpoint;
            this.key = key;
        }
        public TranslateText Translate(TranslateText text, string originalLanguage, string destinationLanguage)
        {
            List<StringBuilder> inputText = GenerateRequestList(text);
            string route = $"/translate?api-version=3.0&to={destinationLanguage}";
            string translate = string.Empty;
            foreach (var item in inputText)
            {
                translate += TranslateTextRequest(subscriptionKey, endpoint, route, item.ToString()).Result;

            }
            text.WriteTranslate(Regex.Split(translate, "\r\n|\r|\n"));
            return text;
        }

        private List<StringBuilder> GenerateRequestList(TranslateText text)
        {
            List<StringBuilder> inputText = new List<StringBuilder>();

            int startline = 0;
            while (startline >= 0)
            {
              inputText.Add(GenerateRequest(text, ref startline));
            }
            return inputText;
        }

        private StringBuilder GenerateRequest(TranslateText text, ref int startLine)
        {
            int charcount = 0;
            StringBuilder builder = new StringBuilder();
            while (charcount < 4500)
            {
                if (text.Count <= startLine)
                {
                    startLine = -1;
                    return builder;
                }
                string line = text[startLine].OrigTerm;
                charcount += line.Length + 1;
                builder.AppendLine(line);
                startLine++;
            };
            return builder;
        }

        // This sample requires C# 7.1 or later for async/await.
        // Async call to the Translator Text API
        static private async Task<string> TranslateTextRequest(string subscriptionKey, string endpoint, string route, string inputText)
        {
            object[] body = new object[] { new { Text = inputText } };
            var requestBody = JsonConvert.SerializeObject(body);
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                // Build the request.
                // Set the method to Post.
                request.Method = HttpMethod.Post;
                // Construct the URI and add headers.
                request.RequestUri = new Uri(endpoint + route);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

                // Send the request and get response.
                HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                // Read response as a string.
                string result = await response.Content.ReadAsStringAsync();
                // Deserialize the response using the classes created earlier.
                TranslationResult[] deserializedOutput = JsonConvert.DeserializeObject<TranslationResult[]>(result);
                // Iterate over the deserialized results.
                foreach (TranslationResult o in deserializedOutput)
                {
                    // Print the detected input language and confidence score.
                    Console.WriteLine("Detected input language: {0}\nConfidence score: {1}\n", o.DetectedLanguage.Language, o.DetectedLanguage.Score);
                    // Iterate over the results and print each translation.
                    foreach (Translation t in o.Translations)
                    {
                        return t.Text;
                    }
                }
                return "";
            }
            /*
             * The code for your call to the translation service will be added to this
             * function in the next few sections.
             */
        }

    }
}
