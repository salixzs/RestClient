using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Salix.RestClient;

/// <summary>
/// Class QueryParameterCollection is to manage query parameters for service call.
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public class QueryParameterCollection : List<QueryParameter>
{
    /// <summary>
    /// Creates new empty Query Parameters collection.
    /// </summary>
    public QueryParameterCollection()
    {
    }

    /// <summary>
    /// Creates Query parameter collection with one value. More can be added by <see cref="Add(string, object)"/> method.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    public QueryParameterCollection(string key, object? value) =>
        this.Add(new QueryParameter(key, value));

    /// <summary>
    /// Returns serialized, encoded query string. Insertion order is preserved.
    /// </summary>
    public override string ToString() => string.Join("&", this.Select(p => p.ToString()));

    /// <summary>
    /// Adds a new query parameter.
    /// </summary>
    /// <param name="key">The parameter name.</param>
    /// <param name="value">The value of parameter.</param>
    public void Add(string key, object? value) => this.Add(new QueryParameter(key, value));

    /// <summary>
    /// Adds a new query parameter, allowing you to specify whether the value is already encoded.
    /// </summary>
    /// <param name="key">The name of a parameter.</param>
    /// <param name="value">The value of parameter.</param>
    /// <param name="isEncoded">if set to <c>true</c> - parameter is HTML encoded already.</param>
    public void Add(string key, string? value, bool isEncoded) => this.Add(new QueryParameter(key, value, isEncoded));

    /// <summary>
    /// True if the collection contains a query parameter with the given name.
    /// </summary>
    /// <param name="name">The name of parameter.</param>
    /// <returns><c>true</c> if the collection contains parameter with this key; otherwise, <c>false</c>.</returns>
    public bool ContainsKey(string name) => this.Any(p => p.Name == name);

    /// <summary>
    /// Removes all parameters of the given name.
    /// </summary>
    /// <param name="name">The name of parameter.</param>
    /// <returns>The number of parameters that were removed.</returns>
    /// <exception cref="System.ArgumentNullException"><paramref name="name" /> is null.</exception>
    public int Remove(string name) => this.RemoveAll(p => p.Name == name);

    /// <summary>
    /// Gets or sets a query parameter value by name. A query may contain multiple values of the same name
    /// (i.e. "x=1&amp;x=2"), in which case the value is an array, which works for both getting and setting.
    /// </summary>
    /// <param name="name">The query parameter name.</param>
    /// <returns>The query parameter value or array of values.</returns>
    public object? this[string name]
    {
        get
        {
            var all = this.Where(p => p.Name == name).Select(p => p.Value).ToArray();
            switch (all.Length)
            {
                case 0:
                    return null;
                case 1:
                    return all[0];
                default:
                    break;
            }

            return all;
        }

        set
        {
            var parameters = this.Where(p => p.Name == name).ToArray();
            var values = value is IEnumerable enumValue and not string ? enumValue.Cast<object>().ToArray() : new[] { value };
            for (var i = 0; ; i++)
            {
                if (i < parameters.Length && i < values.Length)
                {
                    switch (values[i])
                    {
                        case null:
                            this.Remove(parameters[i]);
                            break;
                        case QueryParameter queryParameter:
                            this[this.IndexOf(parameters[i])] = queryParameter;
                            break;
                        default:
                            parameters[i].Value = values[i];
                            break;
                    }
                }
                else if (i < parameters.Length)
                {
                    this.Remove(parameters[i]);
                }
                else if (i < values.Length)
                {
                    switch (values[i])
                    {
                        case null:
                            continue;
                        case QueryParameter queryParameter:
                            this.Add(queryParameter);
                            break;
                        default:
                            this.Add(name, values[i]);
                            break;
                    }
                }
                else
                {
                    break;
                }
            }
        }
    }

    private string DebuggerDisplay => this.ToString();
}
