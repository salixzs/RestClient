using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;

namespace Salix.RestClient
{
    /// <summary>
    /// A collection of Name = Value pairs for API operation Path interpolation parts variable replacements.
    /// </summary>
#pragma warning disable CA1010 // Generic interface should also be implemented
    public class PathParameters : NameObjectCollectionBase
#pragma warning restore CA1010 // Generic interface should also be implemented
    {
        /// <summary>
        /// A collection of Name = Value pairs for API operation Path interpolation parts variable replacements.
        /// </summary>
        public PathParameters()
        {
        }

        /// <summary>
        /// A collection of Name = Value pairs for API operation Path interpolation parts variable replacements.<br/>
        /// Constructor immediately adding one path parameter to collection.
        /// </summary>
        /// <param name="name">Name of path parameter (inside curly braces, like "id" in example "/api/cars/{id}").</param>
        /// <param name="value">A value, which is used in place of interpolated path parameter<br/>Like "id" in example "/api/cars/{id}" with value 12 would become "/api/cars/12").</param>
        public PathParameters(string name, object value) => this.Add(name, value);


        /// <summary>
        /// A collection of Name = Value pairs for API operation Path interpolation parts variable replacements.<br/>
        /// Constructor immediately adding two path parameters to collection.
        /// </summary>
        /// <param name="pathName1">Name of path parameter (inside curly braces, like "id" in example "/api/cars/{id}").<br/>Should be specified together with <paramref name="pathValue1"/>!</param>
        /// <param name="pathValue1">A value, which is used in place of interpolated path parameter<br/>Like "id" in example "/api/cars/{id}" with value 12 would become "/api/cars/12").<br/>Should be specified together with <paramref name="pathName1"/>!</param>
        /// <param name="pathName2">Name of path parameter (inside curly braces, like "id" in example "/api/cars/{id}").<br/>Should be specified together with <paramref name="pathValue2"/>!</param>
        /// <param name="pathValue2">A value, which is used in place of interpolated path parameter<br/>Like "id" in example "/api/cars/{id}" with value 12 would become "/api/cars/12").<br/>Should be specified together with <paramref name="pathName2"/>!</param>
        public PathParameters(string pathName1, object pathValue1, string pathName2, object pathValue2)
        {
            this.Add(pathName1, pathValue1);
            this.Add(pathName2, pathValue2);
        }

        /// <summary>
        /// A collection of Name = Value pairs for API operation Path interpolation parts variable replacements.<br/>
        /// Constructor immediately adding multiple path parameters to collection from passed in collection.
        /// </summary>
        /// <param name="nameValues">Any collection of name/value pairs to be added as path parameters.</param>
        public PathParameters(IDictionary nameValues)
        {
            foreach (DictionaryEntry nameValue in nameValues)
            {
                if (nameValue.Key == null || string.IsNullOrWhiteSpace(nameValue.Key.ToString()))
                {
                    throw new RestClientException("Path parameter name cannot be null or empty string");
                }

                this.BaseAdd((string)nameValue.Key, nameValue.Value);
            }
        }

        /// <summary>
        /// A collection of Name = Value pairs for API operation Path interpolation parts variable replacements.<br/>
        /// Constructor immediately adding one or multiple path parameters to collection from anonymous object.
        /// </summary>
        /// <param name="nameValues">Anonymous object. Can be defined like "new { id = 12, "child" = 1232 }".</param>
        public PathParameters(dynamic nameValues)
        {
            if (nameValues == null)
            {
                return;
            }

            foreach (PropertyInfo property in nameValues.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                this.BaseAdd(property.Name, property.GetValue(nameValues, null).ToString());
            }
        }

        /// <summary>
        /// Returns one entry in path parameters based on its index
        /// </summary>
        /// <param name="index">Index of path parameter in collection.</param>
        public DictionaryEntry this[int index] => new(this.BaseGetKey(index), this.BaseGet(index));

        /// <summary>
        /// Gets or sets a value of path parameter in collection by its name.
        /// </summary>
        /// <param name="name">Name of the path parameter in collection.</param>
        public string this[string name]
        {
            get
            {
                if (!this.ContainsName(name))
                {
                    throw new RestClientException($"Path parameter by name {name} does not exist in collection.");
                }

                return this.BaseGet(name).ToString();
            }
            set
            {
                if (!this.ContainsName(name))
                {
                    throw new RestClientException($"Path parameter by name {name} does not exist in collection.");
                }

                this.BaseSet(name, value);
            }
        }

        /// <summary>
        /// Add a new path parameter to collection.
        /// </summary>
        /// <param name="name">Name of path parameter (inside curly braces, like "id" in example "/api/cars/{id}").</param>
        /// <param name="value">A value, which is used in place of interpolated path parameter<br/>Like "id" in example "/api/cars/{id}" with value 12 would become "/api/cars/12").</param>
        /// <exception cref="RestClientException"><paramref name="name"/> is empty string.</exception>
        public void Add(string name, object value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new RestClientException("Path parameter name cannot be null or empty string");
            }

            this.BaseAdd(name, value);
        }

        /// <summary>
        /// Returns all Path parameters as Name/Value tuple.
        /// </summary>
        public IEnumerable<(string Name, string Value)> GetAll()
        {
            for (var i = 0; i < this.Count; i++)
            {
                yield return new(this[i].Key.ToString(), this[i].Value.ToString());
            }
        }

        /// <summary>
        /// Returns al existing path parameter names in this collection.
        /// </summary>
        public string[] Names => this.BaseGetAllKeys();

        /// <summary>
        /// Returns true, if given name exists in this collection.
        /// </summary>
        /// <param name="name">Name of the path parameter.</param>
        public bool ContainsName(string name) => this.Names.Contains(name);

        /// <summary>
        /// Returns true, if path parameter collection is empty, otherwise - false.
        /// </summary>
        public bool IsEmpty => !this.BaseHasKeys();

        /// <summary>
        /// Removes a path parameter from collection by its name.
        /// </summary>
        /// <param name="name">Name of the path parameter.</param>
        public void Remove(string name) => this.BaseRemove(name);

        /// <summary>
        /// Removes a path parameter from collection by its index in this collection.
        /// </summary>
        /// <param name="index">Index of the path parameter in this collection.</param>
        public void Remove(int index) => this.BaseRemoveAt(index);

        /// <summary>
        /// Empties this path parameter collection.
        /// </summary>
        public void Clear() => this.BaseClear();
    }
}
