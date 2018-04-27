using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EasyCrud.Core.Translation
{
    public class Dictionary
    {
        private Dictionary<string, string> _translations;

        private Dictionary(string jsonDictionary)
        {
            _translations = new Dictionary<string, string>();
            SetUp(jsonDictionary);
        }
        
        public static Dictionary GetDictionary(string languageCode = null)
        {
            var exampleDictionary = "{ easycrud: 'The Wonderful EasyCrud!', complex:{ path: 'Got it!'} }";

            return new Dictionary(exampleDictionary);
        }

        public string GetTranslation(string token)
        {
            var key = token.ToLowerInvariant().Replace("{translation:", string.Empty).Replace("}", string.Empty);
            return _translations[key];
        }

        private void SetUp(string jsonDictionary)
        {
            var dictionary = (JObject)JsonConvert.DeserializeObject(jsonDictionary);

            foreach (var entry in dictionary)
            {
                SetUpToken(entry.Value);
            }
        }

        private void SetUpToken(JToken token)
        {
            switch (token.Type)
            {
                case JTokenType.Object:
                case JTokenType.Property:
                    foreach (var childToken in token.Children())
                    {
                        SetUpToken(childToken);
                    }

                    break;

                case JTokenType.String:
                    _translations.Add(token.Path, token.Value<string>());
                    break;
            }
        }
    }
}
