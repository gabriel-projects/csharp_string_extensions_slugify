using Api.GRRInnovations.Wrapper.Package;

namespace Api.GRRInnovations.StringExtensions.Slugify.Tests
{
    public class StringExtensions
    {
        [Theory]
        [InlineData("Hello World", "hello-world")]
        [InlineData("Test String 123", "test-string-123")]
        [InlineData("Special!@#$%^&*()", "special")]
        [InlineData("Áéíóú", "aeiou")]
        [InlineData("", "")]
        [InlineData(null, "")]
        [InlineData("   ", "")]
        [InlineData("Multiple   Spaces", "multiple-spaces")]
        [InlineData("Test---String", "test-string")]
        public void ToSlug_ShouldConvertStringToSlug(string input, string expected)
        {
            // Act
            var result = input.ToSlug();

            // Assert
            Assert.Equal(expected, result);
        }
    }
}