using System;
using System.Diagnostics.CodeAnalysis;

namespace RestClient.Tests
{
    /// <summary>
    /// A special DTO POCO to test all common types for serialization.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AllTypesDto
    {
        public int NumInteger { get; set; }
        public long NumLong { get; set; }
        public short NumShort { get; set; }
        public byte NumByte { get; set; }
        public bool Boolean { get; set; }
        public double NumDouble { get; set; }
        public float NumFloat { get; set; }
        public decimal NumDecimal { get; set; }
        public DateTime DtDateTime { get; set; }
        public TimeSpan DtTimeSpan { get; set; }
        public DateTimeOffset DtDateTimeOffset { get; set; }
        public string Txt { get; set; }
        public byte[] BinBinary { get; set; }
        public Guid UniqueGuid { get; set; }

        public int? NumIntegerNull { get; set; }
        public long? NumLongNull { get; set; }
        public short? NumShortNull { get; set; }
        public byte? NumByteNull { get; set; }
        public bool? BooleanNull { get; set; }
        public double? NumDoubleNull { get; set; }
        public float? NumFloatNull { get; set; }
        public decimal? NumDecimalNull { get; set; }
        public DateTime? DtDateTimeNull { get; set; }
        public TimeSpan? DtTimeSpanNull { get; set; }
        public DateTimeOffset? DtDateTimeOffsetNull { get; set; }
        public Guid? UniqueGuidNull { get; set; }
    }
}
