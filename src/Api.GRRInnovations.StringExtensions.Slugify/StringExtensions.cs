using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Api.GRRInnovations.Wrapper.Package
{
    /// <summary>
    /// Provides extension methods for string manipulation, specifically for generating URL-friendly slugs.
    /// </summary>
    public static partial class StringExtensions
    {
        /// <summary>
        /// Converts a string into a URL-friendly slug.
        /// </summary>
        /// <param name="value">The string to convert into a slug.</param>
        /// <returns>A URL-friendly slug version of the input string.</returns>
        public static string ToSlug(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            value = value.ToLowerInvariant();

            var builder = new StringBuilder();

            var normalized = value.Normalize(NormalizationForm.FormD);

            foreach (char c in normalized)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                    builder.Append(c);
            }

            var result = builder.ToString().Normalize(NormalizationForm.FormC);
            result = result.ToLowerInvariant();
            result = InvalidCharacterRegex().Replace(result, ""); // Remove invalid characters
            result = WhitespaceRegex().Replace(result, "-");     // Replace spaces with hyphens
            result = DuplicateHyphenRegex().Replace(result, "-"); // Remove duplicate hyphens
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
