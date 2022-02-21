using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Salix.RestClient;
using Xunit;

namespace RestClient.Tests
{
    [ExcludeFromCodeCoverage]
    public class QueryParameterTests
    {
        [Fact]
        public void Create_Integer_IsCorrect()
        {
            var testable = new QueryParameter("Identifier", 1001);
            _ = testable.Name.Should().Be("Identifier");
            _ = testable.Value.Should().Be(1001);
            _ = testable.ToString().Should().Be("Identifier=1001");
        }

        [Fact]
        public void Create_String_IsCorrect()
        {
            var testable = new QueryParameter("Label", "MUU");
            _ = testable.Value.Should().Be("MUU");
            _ = testable.ToString().Should().Be("Label=MUU");
        }

        [Fact]
        public void Create_EncodedString_IsCorrect()
        {
            var testable = new QueryParameter("Label", "PčП@");
            _ = testable.Value.Should().Be("PčП@");
            _ = testable.ToString().Should().Be("Label=P%C4%8D%D0%9F%40");
        }

        [Fact]
        public void Create_EncodedString_NoEncoding()
        {
            var testable = new QueryParameter("Label", "PčП@", true);
            _ = testable.Value.Should().Be("PčП@");
            _ = testable.ToString().Should().Be("Label=PčП@");
        }

        [Fact]
        public void Create_StringWithEncoding_IsCorrect()
        {
            var testable = new QueryParameter("Label", "PčП@", false);
            _ = testable.Value.Should().Be("PčП@");
            _ = testable.ToString().Should().Be("Label=P%C4%8D%D0%9F%40");
        }

        [Fact]
        public void Create_SpacedString_IsCorrect()
        {
            var testable = new QueryParameter("Series", "IT Crowd");
            _ = testable.Value.Should().Be("IT Crowd");
            _ = testable.ToString().Should().Be("Series=IT%20Crowd");
        }

        [Fact]
        public void Create_IntegerList_IsCorrect()
        {
            var testable = new QueryParameter("Ids", new List<int> { 7, 11, 21 });
            _ = testable.Value.Should().BeOfType(typeof(List<int>));
            _ = testable.ToString().Should().Be("Ids=7&Ids=11&Ids=21");
        }

        [Fact]
        public void Create_StringList_IsCorrect()
        {
            var testable = new QueryParameter("Labels", new List<string> { "One", "Two", "Three" });
            _ = testable.Value.Should().BeOfType(typeof(List<string>));
            _ = testable.ToString().Should().Be("Labels=One&Labels=Two&Labels=Three");
        }

        [Fact]
        public void Create_IntegerArray_IsCorrect()
        {
            var testable = new QueryParameter("Id", new int[] { 12, 23, 69 });
            _ = testable.Value.Should().BeOfType(typeof(int[]));
            _ = testable.ToString().Should().Be("Id=12&Id=23&Id=69");
        }
    }
}
