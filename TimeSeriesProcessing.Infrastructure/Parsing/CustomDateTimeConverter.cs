using System.ComponentModel;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace TimeSeriesProcessing.Infrastructure.Parsing;

public class CustomDateTimeConverter : ITypeConverter
{
    private const string Format = "yyyy-MM-ddTHH-mm-ss.ffffZ";

    public object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new TypeConverterException(
                this, memberMapData, text, row.Context, "Date is empty");

        if (!DateTime.TryParseExact(
                text,
                Format,
                CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal,
                out var result))
        {
            throw new TypeConverterException(
                this, memberMapData, text, row.Context,
                $"Invalid date format: '{text}'. Expected: '{Format}'");
        }

        return result;
    }

    public string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
    {
        if (value is DateTime dt)
            return dt.ToString(Format, CultureInfo.InvariantCulture);

        return value?.ToString();
    }
}