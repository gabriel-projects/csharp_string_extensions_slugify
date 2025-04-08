using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Api.GRRInnovations.Wrapper.Package
{
    public static partial class StringExtensions
    {
        public static string ToSlug(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            value = value.ToLowerInvariant();

            var builder = new StringBuilder();

            foreach (char c in value)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                    builder.Append(c);
            }

            var result = builder.ToString().Normalize(NormalizationForm.FormC);
            result = result.ToLowerInvariant();
            result = InvalidCharacterRegex().Replace(result, ""); // remove caracteres inválidos
            result = WhitespaceRegex().Replace(result, "-");         // substitui espaços por hífen
            result = DuplicateHyphenRegex().Replace(result, "-");          // remove hífens duplicados
            return result.Trim('-');
        }

        [GeneratedRegex(@"\s+")]
        private static partial Regex WhitespaceRegex();

        [GeneratedRegex(@"[^a-z0-9\s-]")]
        private static partial Regex InvalidCharacterRegex();

        [GeneratedRegex(@"-+")]
        private static partial Regex DuplicateHyphenRegex();
    }
}
