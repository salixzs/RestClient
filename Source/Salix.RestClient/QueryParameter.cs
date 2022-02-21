using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;

namespace Salix.RestClient;

/// <summary>
/// To handle Query parameters for API calls.
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public class QueryParameter
{
    private object? _value;
    private string? _encodedValue;

    private string _name = null!;

    /// <summary>
    /// The name (left side) of the query parameter.
    /// </summary>
    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new RestClientException("Name for Query parameter cannot be empty/whitespace string.");
            }

            _name = value;
        }
    }

    /// <summary>
    /// The value (right side) of the query parameter.
    /// </summary>
    public object? Value
    {
        get => _value;

        set
        {
            _value = value;
            _encodedValue = null;
        }
    }

    /// <summary>
    /// Creates a new instance of a query parameter.
    /// </summary>
    /// <param name="name">The name of parameter.</param>
    /// <param name="value">The value of parameter.</param>
    public QueryParameter(string name, object? value)
    {
        this.Name = name;
        this.Value = value;
    }

    /// <summary>
    /// Creates a new instance of a query parameter. Allows specifying whether string value provided has
    /// already been URL-encoded.
    /// </summary>
    /// <param name="name">The name of a parameter.</param>
    /// <param name="value">The value of parameter.</param>
    /// <param name="isEncoded">if set to <c>true</c> - parameter is HTML encoded already.</param>
    public QueryParameter(string name, string? value, bool isEncoded)
    {
        this.Name = name;
        if (isEncoded)
        {
            _encodedValue = value;
            _value = Uri.UnescapeDataString((_encodedValue ?? string.Empty).Replace("+", " "));
        }
        else
        {
            this.Value = value;
        }
    }

    /// <summary>
    /// Returns the string ("name=value") representation of the query parameter.
    /// </summary>
    public override string ToString()
    {
        // First check if value is a list of something (except string, of course)
        if (this.Value is IEnumerable value and not string)
        {
            return string.Join(
                "&",
                value.Cast<object>()
                    .Where(v => v != null)
                    .Select(v => new { v, encoded = Uri.EscapeDataString(v.ToString()) })
                    .Select(t => $"{this.Name}={t.encoded}"));
        }

        var encoded = _encodedValue ?? Uri.EscapeDataString((_value ?? string.Empty).ToString());
        return $"{this.Name}={encoded}";
    }

    private string DebuggerDisplay => this.ToString();
}
