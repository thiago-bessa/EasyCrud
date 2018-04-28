using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EasyCrud.Core.Translation
{
    public class Dictionary
    {
        #region Fields 

        private readonly Dictionary<string, string> _translations;

        #endregion

        #region Constructors

        private Dictionary(string jsonDictionary)
        {
            _translations = new Dictionary<string, string>();
            SetUp(jsonDictionary);
        }

        #endregion

        #region Static Methods

        public static Dictionary GetDictionary(string languageCode = "en-us")
        {
            var jsonDictionary = LoadDictionaryFromFile(languageCode);
            return new Dictionary(jsonDictionary);
        }

        private static string LoadDictionaryFromFile(string languageCode)
        {
            var path = HttpContext.Current.Server.MapPath($"~/Dictionaries/{languageCode}.json");

            using (var reader = new StreamReader(path))
            {
                return reader.ReadToEnd();
            }
        }

        #endregion

        #region Public Methods

        public string GetTranslation(string token)
        {
            var key = token.ToLowerInvariant().Replace("{translation:", string.Empty).Replace("}", string.Empty);
            return _translations.ContainsKey(key) ? _translations[key] : token.ToLowerInvariant().Replace("translation", "unknown");
        }

        #endregion

        #region SetUp

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

        #endregion
    }
}
