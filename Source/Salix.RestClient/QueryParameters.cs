using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace Salix.RestClient;

/// <summary>
/// A collection of request operation URL query parameters (operation?parm1=val1&amp;parm2=val2).
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public class QueryParameters : List<QueryParameter>
{
    /// <summary>
    /// A collection of request operation URL query parameters (operation?parm1=val1&amp;parm2=val2).<br/>
    /// Creates a new empty query parameter collection.<br/><br/>
    /// <code>
    /// var queryParameters = new QueryParameters();
    /// </code>
    /// </summary>
    public QueryParameters()
    {
    }

    /// <summary>
    /// A collection of request operation URL query parameters (operation?parm1=val1&amp;parm2=val2).<br/>
    /// Creates a new query parameter collection with one query parameter (name=value).<br/><br/>
    /// <code>
    /// // Will end in operation?audit=true
    /// var queryParameters = new QueryParameters("audit", true);
    /// 
    /// // Will end in operation?id=1&amp;id=2&amp;id=3
    /// var queryParameters = new QueryParameters("id", new int[] { 1, 2, 3 });
    /// </code>
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    public QueryParameters(string key, object? value)
    {
        if (value == null)
        {
            this.Add(new QueryParameter(key, null));
            return;
        }

        foreach (var val in this.SplitCollection(value).ToList().Where(val => val != null))
        {
            this.Add(new QueryParameter(key, val));
        }
    }

    /// <summary>
    /// A collection of request operation URL query parameters (operation?parm1=val1&amp;parm2=val2).<br/>
    /// Creates a new query parameter collection with two query parameters (name=value&amp;name=value).<br/><br/>
    /// <code>
    /// // Will end in operation?skip=0&amp;take=15
    /// var queryParameters = new QueryParameters("skip", 0, "take", 15);
    /// 
    /// // Will end in operation?id=1&amp;id=2&amp;id=3&amp;page=1
    /// var queryParameters = new QueryParameters("id", new int[] { 1, 2, 3 }, "page", 1);
    /// </code>
    /// </summary>
    /// <param name="key1">The key/name of the first parameter.</param>
    /// <param name="value1">The value for the first parameter. Can be array of values.</param>
    public QueryParameters(string key1, object? value1, string key2, object? value2)
    {
        if (value1 == null)
        {
            this.Add(new QueryParameter(key1, null));
        }
        else
        {
            foreach (var val in this.SplitCollection(value1).ToList().Where(val => val != null))
            {
                this.Add(new QueryParameter(key1, val));
            }
        }

        if (value2 == null)
        {
            this.Add(new QueryParameter(key2, null));
            return;
        }

        foreach (var val in this.SplitCollection(value2).ToList().Where(val => val != null))
        {
            this.Add(new QueryParameter(key2, val));
        }

    }

    /// <summary>
    /// A collection of request operation URL query parameters (operation?parm1=val1&amp;parm2=val2).<br/>
    /// Creates a new query parameter collection with all properties in given anonymous object.<br/><br/>
    /// <code>
    /// var queryParameters = new QueryParameters(new { skip = 100, take = 20 });
    /// </code>
    /// </summary>
    /// <param name="nameValues">Anonymous object. Can be defined like "new { id = 12, childId = 1232 }".</param>
    public QueryParameters(dynamic nameValues)
    {
        if (nameValues == null)
        {
            return;
        }

        foreach (PropertyInfo property in nameValues.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
        {
            this.Add(property.GetValue(nameValues, null) == null
                ? new QueryParameter(property.Name, string.Empty)
                : new QueryParameter(property.Name, property.GetValue(nameValues, null).ToString()));
        }
    }

    /// <summary>
    /// Returns serialized, encoded query string with all parameters in this collection. Insertion order is preserved.<br/><br/>
    /// Example:<br/>
    /// parm1=val1&amp;parm2=val2&amp;parm3=val3
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

    private IEnumerable<object> SplitCollection(object value)
    {
        switch (value)
        {
            case string s:
                yield return s;
                break;
            case IEnumerable en:
            {
                foreach (var item in en.Cast<object>().SelectMany(this.SplitCollection).ToList())
                {
                    yield return item;
                }

                break;
            }
            default:
                yield return value;
                break;
        }
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    [ExcludeFromCodeCoverage]
    private string DebuggerDisplay => this.ToString();
}
