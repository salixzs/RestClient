using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using FluentAssertions;
using Salix.RestClient;
using Xunit;

namespace RestClient.Tests
{
    [ExcludeFromCodeCoverage]
    public class JsonSerializerTests
    {
        [Fact]
        public async Task Serialize_FilledTypes_AllSerialized()
        {
            var filled = new AllTypesDto
            {
                NumInteger = 1,
                NumIntegerNull = 2,
                NumLong = 3,
                NumLongNull = 4,
                NumShort = 5,
                NumShortNull = 6,
                NumByte = 7,
                NumByteNull = 8,
                NumDouble = 9.1,
                NumDoubleNull = 9.2,
                NumFloat = 10.1F,
                NumFloatNull = 10.2F,
                NumDecimal = 11.1M,
                NumDecimalNull = 11.2M,
                Boolean = true,
                BooleanNull = true,
                DtDateTime = new DateTime(1968, 4, 3, 8, 30, 1),
                DtDateTimeNull = new DateTime(1999, 9, 18, 12, 15, 2),
                DtDateTimeOffset = new DateTimeOffset(1942, 5, 20, 21, 10, 4, TimeSpan.FromHours(2)),
                DtDateTimeOffsetNull = new DateTimeOffset(1945, 10, 7, 18, 59, 5, TimeSpan.FromHours(2)),
                DtTimeSpan = TimeSpan.FromSeconds(7).Add(TimeSpan.FromTicks(98253204)),
                DtTimeSpanNull = TimeSpan.FromSeconds(8).Add(TimeSpan.FromTicks(738459)),
                Txt = "wow",
                BinBinary = new byte[] { 192, 223, 34 },
                UniqueGuid = new Guid("2F709566-8B67-4691-A66C-0A3B2EEDFFDB"),
                UniqueGuidNull = new Guid("437B9783-3123-4D8E-ABF0-718E52722FDF")
            };

            var testable = await SystemTextJsonObjectSerializer.Default.SerializeAsync(filled);
            testable.Should().Contain("\"NumInteger\":1");
            testable.Should().Contain("\"NumIntegerNull\":2");
            testable.Should().Contain("\"NumLong\":3");
            testable.Should().Contain("\"NumLongNull\":4");
            testable.Should().Contain("\"NumShort\":5");
            testable.Should().Contain("\"NumShortNull\":6");
            testable.Should().Contain("\"NumByte\":7");
            testable.Should().Contain("\"NumByteNull\":8");
            testable.Should().Contain("\"NumDouble\":9.1");
            testable.Should().Contain("\"NumDoubleNull\":9.2");
            testable.Should().Contain("\"NumFloat\":10.1");
            testable.Should().Contain("\"NumFloatNull\":10.2");
            testable.Should().Contain("\"NumDecimal\":11.1");
            testable.Should().Contain("\"NumDecimalNull\":11.2");
            testable.Should().Contain("\"NumDecimal\":11.1");
            testable.Should().Contain("\"Boolean\":true");
            testable.Should().Contain("\"BooleanNull\":true");
            testable.Should().Contain("\"DtDateTime\":\"1968-04-03T08:30:01\"");
            testable.Should().Contain("\"DtDateTimeNull\":\"1999-09-18T12:15:02\"");
            testable.Should().Contain("\"DtDateTimeOffset\":\"1942-05-20T21:10:04+02:00\"");
            testable.Should().Contain("\"DtDateTimeOffsetNull\":\"1945-10-07T18:59:05+02:00\"");
            testable.Should().Contain("\"DtTimeSpan\":\"00:00:16.8253204\"");
            testable.Should().Contain("\"DtTimeSpanNull\":\"00:00:08.0738459\"");
            testable.Should().Contain("\"Txt\":\"wow\"");
            testable.Should().Contain("\"BinBinary\":\"wN8i\"");
            testable.Should().Contain("\"UniqueGuid\":\"2f709566-8b67-4691-a66c-0a3b2eedffdb\"");
            testable.Should().Contain("\"UniqueGuidNull\":\"437b9783-3123-4d8e-abf0-718e52722fdf\"");
        }

        [Fact]
        public async Task Deserialize_FilledTypes_AllDeserialized()
        {
            var jsonSource = @"{
	""NumInteger"": 1,
	""NumLong"": 3,
	""NumShort"": 5,
	""NumByte"": 7,
	""Boolean"": true,
	""NumDouble"": 9.1,
	""NumFloat"": 10.1,
	""NumDecimal"": 11.1,
	""DtDateTime"": ""1968-04-03T08:30:01"",
	""DtTimeSpan"": ""00:00:16.8253204"",
	""DtDateTimeOffset"": ""1942-05-20T21:10:04+02:00"",
	""Txt"": ""wow"",
	""BinBinary"": ""wN8i"",
	""UniqueGuid"": ""2f709566-8b67-4691-a66c-0a3b2eedffdb"",
	""NumIntegerNull"": 2,
	""NumLongNull"": 4,
	""NumShortNull"": 6,
	""NumByteNull"": 8,
	""BooleanNull"": true,
	""NumDoubleNull"": 9.2,
	""NumFloatNull"": 10.2,
	""NumDecimalNull"": 11.2,
	""DtDateTimeNull"": ""1999-09-18T12:15:02"",
	""DtTimeSpanNull"": ""00:00:08.0738459"",
	""DtDateTimeOffsetNull"": ""1945-10-07T18:59:05+02:00"",
	""UniqueGuidNull"": ""437b9783-3123-4d8e-abf0-718e52722fdf""
}";
            var testable = await SystemTextJsonObjectSerializer.Default.DeserializeAsync<AllTypesDto>(jsonSource);
            testable.Should().BeEquivalentTo(new AllTypesDto
            {
                NumInteger = 1,
                NumIntegerNull = 2,
                NumLong = 3,
                NumLongNull = 4,
                NumShort = 5,
                NumShortNull = 6,
                NumByte = 7,
                NumByteNull = 8,
                NumDouble = 9.1,
                NumDoubleNull = 9.2,
                NumFloat = 10.1F,
                NumFloatNull = 10.2F,
                NumDecimal = 11.1M,
                NumDecimalNull = 11.2M,
                Boolean = true,
                BooleanNull = true,
                DtDateTime = new DateTime(1968, 4, 3, 8, 30, 1),
                DtDateTimeNull = new DateTime(1999, 9, 18, 12, 15, 2),
                DtDateTimeOffset = new DateTimeOffset(1942, 5, 20, 21, 10, 4, TimeSpan.FromHours(2)),
                DtDateTimeOffsetNull = new DateTimeOffset(1945, 10, 7, 18, 59, 5, TimeSpan.FromHours(2)),
                DtTimeSpan = TimeSpan.FromSeconds(7).Add(TimeSpan.FromTicks(98253204)),
                DtTimeSpanNull = TimeSpan.FromSeconds(8).Add(TimeSpan.FromTicks(738459)),
                Txt = "wow",
                BinBinary = new byte[] { 192, 223, 34 },
                UniqueGuid = new Guid("2F709566-8B67-4691-A66C-0A3B2EEDFFDB"),
                UniqueGuidNull = new Guid("437B9783-3123-4D8E-ABF0-718E52722FDF")
            });
        }

        [Fact]
        public async Task Serialize_NullTypes_AllSerialized()
        {
            var filled = new AllTypesDto
            {
                NumInteger = 1,
                NumIntegerNull = null,
                NumLong = 3,
                NumLongNull = null,
                NumShort = 5,
                NumShortNull = null,
                NumByte = 7,
                NumByteNull = null,
                NumDouble = 9.1,
                NumDoubleNull = null,
                NumFloat = 10.1F,
                NumFloatNull = null,
                NumDecimal = 11.1M,
                NumDecimalNull = null,
                Boolean = true,
                BooleanNull = null,
                DtDateTime = new DateTime(1968, 4, 3, 8, 30, 1),
                DtDateTimeNull = null,
                DtDateTimeOffset = new DateTimeOffset(1942, 5, 20, 21, 10, 4, TimeSpan.FromHours(2)),
                DtDateTimeOffsetNull = null,
                DtTimeSpan = TimeSpan.FromSeconds(7).Add(TimeSpan.FromTicks(98253204)),
                DtTimeSpanNull = null,
                Txt = null,
                BinBinary = null,
                UniqueGuid = new Guid("2F709566-8B67-4691-A66C-0A3B2EEDFFDB"),
                UniqueGuidNull = null
            };

            var testable = await SystemTextJsonObjectSerializer.Default.SerializeAsync(filled);
            testable.Should().Contain("\"NumInteger\":1");
            testable.Should().Contain("\"NumIntegerNull\":null");
            testable.Should().Contain("\"NumLong\":3");
            testable.Should().Contain("\"NumLongNull\":null");
            testable.Should().Contain("\"NumShort\":5");
            testable.Should().Contain("\"NumShortNull\":null");
            testable.Should().Contain("\"NumByte\":7");
            testable.Should().Contain("\"NumByteNull\":null");
            testable.Should().Contain("\"NumDouble\":9.1");
            testable.Should().Contain("\"NumDoubleNull\":null");
            testable.Should().Contain("\"NumFloat\":10.1");
            testable.Should().Contain("\"NumFloatNull\":null");
            testable.Should().Contain("\"NumDecimal\":11.1");
            testable.Should().Contain("\"NumDecimalNull\":null");
            testable.Should().Contain("\"NumDecimal\":11.1");
            testable.Should().Contain("\"Boolean\":true");
            testable.Should().Contain("\"BooleanNull\":null");
            testable.Should().Contain("\"DtDateTime\":\"1968-04-03T08:30:01\"");
            testable.Should().Contain("\"DtDateTimeNull\":null");
            testable.Should().Contain("\"DtDateTimeOffset\":\"1942-05-20T21:10:04+02:00\"");
            testable.Should().Contain("\"DtDateTimeOffsetNull\":null");
            testable.Should().Contain("\"DtTimeSpan\":\"00:00:16.8253204\"");
            testable.Should().Contain("\"DtTimeSpanNull\":null");
            testable.Should().Contain("\"Txt\":null");
            testable.Should().Contain("\"BinBinary\":null");
            testable.Should().Contain("\"UniqueGuid\":\"2f709566-8b67-4691-a66c-0a3b2eedffdb\"");
            testable.Should().Contain("\"UniqueGuidNull\":null");
        }

        [Fact]
        public async Task Deserialize_NullTypes_AllDeserialized()
        {
            var jsonSource = @"{
	""NumInteger"": 1,
	""NumLong"": 3,
	""NumShort"": 5,
	""NumByte"": 7,
	""Boolean"": true,
	""NumDouble"": 9.1,
	""NumFloat"": 10.1,
	""NumDecimal"": 11.1,
	""DtDateTime"": ""1968-04-03T08:30:01"",
	""DtTimeSpan"": ""00:00:16.8253204"",
	""DtDateTimeOffset"": ""1942-05-20T21:10:04+02:00"",
	""Txt"": null,
	""BinBinary"": null,
	""UniqueGuid"": ""2f709566-8b67-4691-a66c-0a3b2eedffdb"",
	""NumIntegerNull"": null,
	""NumLongNull"": null,
	""NumShortNull"": null,
	""NumByteNull"": null,
	""BooleanNull"": null,
	""NumDoubleNull"": null,
	""NumFloatNull"": null,
	""NumDecimalNull"": null,
	""DtDateTimeNull"": null,
	""DtTimeSpanNull"": null,
	""DtDateTimeOffsetNull"": null,
	""UniqueGuidNull"": null
}";
            var testable = await SystemTextJsonObjectSerializer.Default.DeserializeAsync<AllTypesDto>(jsonSource);
            testable.Should().BeEquivalentTo(new AllTypesDto
            {
                NumInteger = 1,
                NumIntegerNull = null,
                NumLong = 3,
                NumLongNull = null,
                NumShort = 5,
                NumShortNull = null,
                NumByte = 7,
                NumByteNull = null,
                NumDouble = 9.1,
                NumDoubleNull = null,
                NumFloat = 10.1F,
                NumFloatNull = null,
                NumDecimal = 11.1M,
                NumDecimalNull = null,
                Boolean = true,
                BooleanNull = null,
                DtDateTime = new DateTime(1968, 4, 3, 8, 30, 1),
                DtDateTimeNull = null,
                DtDateTimeOffset = new DateTimeOffset(1942, 5, 20, 21, 10, 4, TimeSpan.FromHours(2)),
                DtDateTimeOffsetNull = null,
                DtTimeSpan = TimeSpan.FromSeconds(7).Add(TimeSpan.FromTicks(98253204)),
                DtTimeSpanNull = null,
                Txt = null,
                BinBinary = null,
                UniqueGuid = new Guid("2F709566-8B67-4691-A66C-0A3B2EEDFFDB"),
                UniqueGuidNull = null
            });
        }
    }
}
