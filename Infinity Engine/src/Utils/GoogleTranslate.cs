#pragma warning disable XS0001 // Find APIs marked as TODO in Mono

using SimpleJSON;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

namespace InfinityEngine.Utils
{
    /// <summary>
    /// A translated text information
    /// </summary>
    public struct TranslationInfo
    {
        /// <summary>
        /// The translated text
        /// </summary>
        public string Text
        {
            get;
            private set;
        }

        /// <summary>
        /// The source language of the translation
        /// </summary>
        public string SourceLanguage
        {
            get;
            private set;
        }

        /// <summary>
        /// The target language of the translation
        /// </summary>
        public string TargetLanguage
        {
            get;
            private set;
        }

        /// <summary>
        /// A value indicating whether the translation is valid
        /// </summary>
        public bool IsValid
        {
            get;
            private set;
        }

        internal TranslationInfo(string text, string sourceLang, string targetLang, bool isValid)
        {
            Text = text;
            SourceLanguage = sourceLang;
            TargetLanguage = targetLang;
            IsValid = isValid;
        }
    }

    /// <summary>
    /// Provides a static functions allowing to translates a text using Google Translate api .
    /// </summary>
    public static class GoogleTranslate
    {
        private static string translatedText = "";

        private static string GetUrl(string sourceLang, string targetLang, string sourceText)
        {
            return "https://translate.googleapis.com/translate_a/single?client=gtx&sl=" + sourceLang + "&tl=" + targetLang + "&dt=t&q=" + WWW.EscapeURL(sourceText);
        }

        /// <summary>
        /// Translates the given text from <paramref name="sourceLang" /> to <paramref name="targetLang" />
        /// </summary>
        /// <param name="sourceLang">The source language</param>
        /// <param name="targetLang">The target language</param>
        /// <param name="sourceText">The text to translate</param>
        /// <param name="callback">A callback function invoked after the translation</param>
        public static void Translate(string sourceLang, string targetLang, string sourceText, Action<TranslationInfo> callback)
        {
            Infinity.DOCoroutine(Process(sourceLang, targetLang, sourceText, callback));
        }

        /// <summary>
        /// Translates the given text to <paramref name="targetLang" />. The function detects automatically the souce language
        /// </summary>
        /// <param name="targetLang">The target language</param>
        /// <param name="sourceText">The text to translate</param>
        /// <param name="callback">A callback function invoked after the translation</param>
        public static void Translate(string targetLang, string sourceText, Action<TranslationInfo> callback)
        {
            Infinity.DOCoroutine(Process("auto", targetLang, sourceText, callback));
        }

        internal static IEnumerator Process(string sourceLang, string targetLang, string sourceText, Action<TranslationInfo> callback)
        {
            string url = GetUrl(sourceLang, targetLang, sourceText);
            WWW www = new WWW(url);
            yield return www;
            if (www.isDone)
            {
                if (string.IsNullOrEmpty(www.error))
                {
                    JSONNode jSONNode = JSONNode.Parse(Encoding.UTF8.GetString(www.bytes));
                    var stringBuilder = new StringBuilder();
                    JSONNode jSONNode2 = jSONNode[0];
                    int count = jSONNode2.Count;
                    for (int i = 0; i < count; i++)
                    {
                        stringBuilder.Append((string)jSONNode2[i][0]);
                    }
                    string text = stringBuilder.ToString();
                    JSONNode d = jSONNode[2];
                    callback(new TranslationInfo(text, d, targetLang, isValid: true));
                }
            }
            else
            {
                callback(new TranslationInfo(translatedText, sourceLang, targetLang, isValid: false));
            }
        }
    }
}
