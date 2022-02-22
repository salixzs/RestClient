using System.Threading.Tasks;

namespace Salix.RestClient;

/// <summary>
/// Interface, defining methods necessary to have serialization and deserialization of request content (both sending object and receiving object from REST Service).
/// </summary>
public interface IObjectSerializer
{
    /// <summary>
    /// Name of the serializer to distinguish in case there are many implementations for this contract.
    /// </summary>
    string SerializerName { get; }

    /// <summary>
    /// Serializes a POCO object to a string representation.
    /// </summary>
    Task<string?> SerializeAsync(object data);

    /// <summary>
    /// Deserializes a POCO object from a string representation.
    /// </summary>
    Task<T?> DeserializeAsync<T>(string content);
}
