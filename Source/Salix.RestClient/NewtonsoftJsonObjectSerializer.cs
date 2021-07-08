using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Salix.RestClient
{
    /// <summary>
    /// A JSON content serializer.
    /// </summary>
    public class NewtonsoftJsonObjectSerializer : IObjectSerializer
    {

        /// <summary>
        /// Create a new JSON object serializer with the specified options.
        /// </summary>
        /// <param name="options">Options for Json serializer.</param>
        public NewtonsoftJsonObjectSerializer(JsonSerializerSettings options = null) =>
            this.Options = options ?? new JsonSerializerSettings();

        /// <summary>
        /// Gets or sets the JSON serializer options.
        /// </summary>
        /// <value>
        /// The JSON serializer options.
        /// </value>
        public JsonSerializerSettings Options { get; }

        /// <inheritdoc/>
        public async Task<string> SerializeAsync(object data) =>
            data == null ? null : JsonConvert.SerializeObject(data, Formatting.None, this.Options);

        /// <inheritdoc/>
        public async Task<T> DeserializeAsync<T>(string content) =>
            JsonConvert.DeserializeObject<T>(content, this.Options);

        private static readonly Lazy<NewtonsoftJsonObjectSerializer> Current = new(() => new NewtonsoftJsonObjectSerializer());

        /// <summary>
        /// Gets the current singleton instance of NewtonsoftJsonObjectSerializer.
        /// </summary>
        /// <value>The current singleton instance.</value>
        /// <remarks>
        /// An instance of NewtonsoftJsonObjectSerializer wont be created until the very first
        /// call to the sealed class. This is a CLR optimization that
        /// provides a properly lazy-loading singleton.
        /// </remarks>
        public static NewtonsoftJsonObjectSerializer Default => Current.Value;

        /// <inheritdoc/>
        public string SerializerName => "Newtonsoft";
    }
}
