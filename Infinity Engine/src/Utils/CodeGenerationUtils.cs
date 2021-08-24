#pragma warning disable XS0001 // Find APIs marked as TODO in Mono

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InfinityEngine.Extensions;

namespace InfinityEngine.Utils
{
    /// <summary>
    /// Simple class allowing to create a field and validate C# field by using chained methods
    /// </summary>
    [Serializable]
    public static class CodeGenerationUtils
    {
        /// <summary>
        /// Some of the builtin words of c# language.
        /// </summary>
        public static readonly HashSet<string> BuiltInWords = new HashSet<string>
        {
            "public", "get", "set", "protected", "private", "internal", "virtual", "this",  "const", "readonly","lock",
            "abstract", "void", "new", "static", "class", "enum", "struct", "interface", "var",
            "int", "float", "Single", "Single64", "Single32", "bool", "string", "byte", "double", "short", "long",
            "char","Exception", "out", "in", "ref", "try", "catch", "final","finally", "goto", "List" , "using", "namespace", "params",
            "get;", "set;", "typeof", "for", "foreach", "if", "else", "switch", "case", "return","break", "continue", "while", "do", "throw",
            "base", "where", "from", "select", "as", "nameof", "is", "in", "delegate", "event", "null", "true", "false", "object", "sealed",
            "async", "extern", "volatile", "await", "dynamic", "partial", "decimal", "default", "fixed", "unsafe", "__arglist",
            "stackalloc", "Dictionary", "HashSet", "where", "from", "select", "grouby", "uint", "UInt16", "UInt32", "UInt64",
            "ushort", "ulong", "unchecked", "checked"
        };

        /// <summary>
        /// Reserved CSharp word error.
        /// </summary>
        /// <value>The value of this property is "CSHARP_RESERVED_WORD_ERROR" which is translated in the current language of the editor by <see cref="N:InfinityEngine" /> localization system. </value>
        public const string CSHARP_RESERVED_WORD_ERROR = "CSHARP_RESERVED_WORD_ERROR";

        /// <summary>
        /// Empty field name error.
        /// </summary>
        /// <value>The value of this property is "EMPTY_FIELD_NAME_ERROR" which is translated in the current language of the editor by <see cref="N:InfinityEngine" /> localization system. </value>
        public const string EMPTY_FIELD_NAME_ERROR = "EMPTY_FIELD_NAME_ERROR";

        /// <summary>
        /// Space error (field name cannot contains space)
        /// </summary>
        /// <value>The value of this property is "SPACE_ERROR" which is translated in the current language of the editor by <see cref="N:InfinityEngine" /> localization system. </value>
        public const string SPACE_ERROR = "SPACE_ERROR";

        /// <summary>
        /// Bad start char error (field name starts only with a letter or the char '_'.
        /// </summary>
        /// <value>The value of this property is "UNAUTHORIZED__START_CHAR_ERROR" which is translated in the current language of the editor by <see cref="N:InfinityEngine" /> localization system. </value>
        public const string UNAUTHORIZED__START_CHAR_ERROR = "UNAUTHORIZED__START_CHAR_ERROR";

        /// <summary>
        /// Bad char error (field name contains only letters or the char '_'.
        /// </summary>
        /// <value>The value of this property is "UNAUTHORIZED_CHAR_ERROR" which is translated in the current language of the editor by <see cref="N:InfinityEngine" /> localization system. </value>
        public const string UNAUTHORIZED_CHAR_ERROR = "UNAUTHORIZED_CHAR_ERROR";

        public static string MakeTabulation(int level)
        {
            return "\t".Repeat(level);
        }

        public static string MakeXMLTag(string content, string tag, string tabulation = "")
        {
            var spaces = content.Replace("\n", $"\n{tabulation}///");
            content = $"{tabulation}///<{tag}>\n{tabulation}///{spaces}\n{tabulation}///</{tag}>\n";
            return content;
        }

        /// <summary>
        /// Computes the input string by removing any character that cannot constitute an identifier name.
        /// and transform the first char of each word to uppercase.
        /// </summary>
        /// <exception cref="T:System.ArgumentException">
        /// Thrown when the input is null, empty or does not contains any letter.
        /// </exception>
        /// <param name="input">The identifier</param>
        /// <param name="onlyLetter">(Optional) if set to <c>true</c>, the function remove all char different from a letter in the identifier. </param>
        /// <example>
        ///  <code>
        ///  Debug.Log(CodeGenerationUtils.MakeIdentifier("-my_variable"));
        ///  Debug.Log(CodeGenerationUtils.MakeIdentifier("my  variable2", true));
        ///  Debug.Log(CodeGenerationUtils.MakeIdentifier("my  variable2"));
        ///  Debug.Log(CodeGenerationUtils.MakeIdentifier("object"));
        ///  Debug.Log(CodeGenerationUtils.MakeIdentifier(""));
        ///  </code>
        ///  This code procudes the following result : <br />
        ///  &gt; MyVariable <br />
        ///  &gt; MyVariable <br />
        ///  &gt; MyVariable2 <br />
        ///  &gt; ArgumentException<br />
        ///  &gt; ArgumentException<br />
        /// </example>
        /// <returns>A computed version of the input</returns>
        public static string MakeIdentifier(string input, bool onlyLetter = false)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException($"'{nameof(input)}' must not be null or empty");

            var chars = input.ToCharArray();
            if (!chars.Any(it => it.IsInAlphabet()))
                throw new ArgumentException($"'{nameof(input)}' must contains at least one letter");

            var builder = new StringBuilder();
            var shouldMakeUppercase = true;
            var length = chars.Length;
            char first = ' ';

            for (var i = 0; i < length - 1; i++)
            {
                first = input[i];
                if (shouldMakeUppercase)
                {
                    if (first.IsInAlphabet() || (!onlyLetter && char.IsDigit(first)))
                    {
                        builder.Append(first.ToUpper());
                        shouldMakeUppercase = false;
                    }
                }
                else if (first.IsInAlphabet() || (!onlyLetter && char.IsDigit(first)))
                {
                    builder.Append(first);
                }
                else
                {
                    shouldMakeUppercase = true;
                }
            }

            first = input[length - 1];
            if (first.IsInAlphabet() || (!onlyLetter && char.IsDigit(first)))
                builder.Append(shouldMakeUppercase ? first.ToUpper() : first);

            return builder.ToString();
        }

        /// <summary>
        /// Checks if the <paramref name="input"/> is a valid C# identifier name.
        /// </summary>
        /// <example>
        ///     Exemple 1 :
        ///     <code>
        ///     Debug.Log(CodeGenerationUtils.CheckIdentifier("id"));
        ///     </code>
        ///     This code prints <c>null</c>.<br />
        ///
        ///     Exemple 2 :
        ///     <code> Debug.Log(CodeGenerationUtils.CheckIdentifier("1id"));</code>
        ///     This code prints 'SPACE_ERROR' because an identifier cannot starts with a digit.
        /// </example>
        /// <param name="input">The input</param>
        /// <returns>
        /// An error message if the input is not a valid C# identifier name <c>null</c> otherwise
        /// </returns>
        public static string CheckIdentifier(string input)
        {
            if (string.IsNullOrEmpty(input))
                return EMPTY_FIELD_NAME_ERROR;

            if (input.Contains(" "))
                return SPACE_ERROR;

            if (!input[0].IsInAlphabet() && input[0] != '_' && input[0] != '@')
                return UNAUTHORIZED__START_CHAR_ERROR;

            var length = input.Length;

            for (int i = 1; i < length; i++)
            {
                char c = input[i];
                if (!c.IsInAlphabet() && c != '_' && !char.IsDigit(c))
                    return UNAUTHORIZED_CHAR_ERROR;
            }

            if (BuiltInWords.Contains(input))
                return CSHARP_RESERVED_WORD_ERROR;

            return null;
        }

        /// <summary>
        /// Checks if the <paramref name="input"/> can be used as an identifier name.
        /// </summary>
        /// <param name="input">The input</param>
        /// <returns><c>true</c> if the input is a valid identifier name<c>false</c> otherwise</returns>
        public static bool IsIdentifier(string input)
        {
            return CheckIdentifier(input) == null;
        }

    }
}
