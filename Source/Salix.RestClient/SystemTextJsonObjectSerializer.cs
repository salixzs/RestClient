using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Salix.RestClient
{
    /// <summary>
    /// A JSON content serializer.
    /// </summary>
    public class SystemTextJsonObjectSerializer : IObjectSerializer
    {

        /// <summary>
        /// Create a new JSON object serializer with the specified options.
        /// </summary>
        /// <param name="options">Options for Json serializer.</param>
        public SystemTextJsonObjectSerializer(JsonSerializerOptions options = null)
        {
            this.Options = options ?? new JsonSerializerOptions();
            this.Options.Converters.Add(new JsonObjectSerializerTimeSpanConverter()); // Not needed for .Net 6 (laters)
        }

        /// <summary>
        /// Gets or sets the JSON serializer options.
        /// </summary>
        /// <value>
        /// The JSON serializer options.
        /// </value>
        public JsonSerializerOptions Options { get; }

        /// <inheritdoc/>
        public async Task<string> SerializeAsync(object data)
        {
            if (data == null)
            {
                return null;
            }

            using var stream = new MemoryStream();
            await JsonSerializer.SerializeAsync(stream, data, data.GetType(), this.Options);
            stream.Position = 0;
            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<T> DeserializeAsync<T>(string content)
        {
            using var openStream = new MemoryStream(Encoding.UTF8.GetBytes(content));
            return await JsonSerializer.DeserializeAsync<T>(openStream, this.Options).ConfigureAwait(false);
        }

        private static readonly Lazy<SystemTextJsonObjectSerializer> Current = new(() => new SystemTextJsonObjectSerializer());

        /// <summary>
        /// Gets the current singleton instance of JsonContentSerializer.
        /// </summary>
        /// <value>The current singleton instance.</value>
        /// <remarks>
        /// An instance of JsonContentSerializer wont be created until the very first
        /// call to the sealed class. This is a CLR optimization that
        /// provides a properly lazy-loading singleton.
        /// </remarks>
        public static SystemTextJsonObjectSerializer Default => Current.Value;

        /// <inheritdoc/>
        public string SerializerName => "System.Text.Json";
    }
}
