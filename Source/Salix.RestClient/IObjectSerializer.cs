using System.Threading.Tasks;

namespace Salix.RestClient
{
    /// <summary>
    /// 
    /// </summary>
    public interface IObjectSerializer
    {
        /// <summary>
        /// Name of the serializer to distinguish in case there are many implementations for this contract.
        /// </summary>
        string SerializerName { get; }

        /// <summary>
        /// Serializes an object to a string representation.
        /// </summary>
        Task<string> SerializeAsync(object data);

        /// <summary>
        /// Deserializes an object from a string representation.
        /// </summary>
        Task<T> DeserializeAsync<T>(string content);
    }
}
