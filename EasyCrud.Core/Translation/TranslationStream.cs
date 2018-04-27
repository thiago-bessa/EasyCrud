using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EasyCrud.Core.Translation
{
    public class TranslationStream : MemoryStream
    {
        private readonly Stream _responseStream;

        public TranslationStream(Stream stream)
        {
            _responseStream = stream;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            buffer = GetTranslatedBuffer(buffer);
            _responseStream.Write(buffer, offset, buffer.Length);
        }

        private byte[] GetTranslatedBuffer(byte[] buffer)
        {
            var html = Encoding.UTF8.GetString(buffer);
            var tokens = GetTokens(html);
            var dictionary = Dictionary.GetDictionary();
            var translatedHtml = GetTranslatedHtml(html, tokens, dictionary);
            return Encoding.UTF8.GetBytes(translatedHtml);
        }

        private List<string> GetTokens(string html)
        {
            var regex = new Regex("({TRANSLATION:)([A-Z/.]*)(})", RegexOptions.IgnoreCase);

            return regex.Matches(html)
                .Cast<Match>()
                .Select(m => m.Value)
                .Distinct()
                .ToList();
        }

        private string GetTranslatedHtml(string html, List<string> tokens, Dictionary dictionary)
        {
            var translatedHtml = html;

            foreach (var token in tokens)
            {
                translatedHtml = translatedHtml.Replace(token, dictionary.GetTranslation(token));
            }

            return translatedHtml;
        }
    }
}
