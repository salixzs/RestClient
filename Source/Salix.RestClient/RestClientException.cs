using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;

namespace Salix.RestClient;

/// <summary>
/// General API Exception for handling exceptional situations.
/// Added so it can be easier to distinguish custom thrown API exceptions and system default exceptions.
/// </summary>
[DebuggerDisplay("{Message}")]
[Serializable]
public class RestClientException : Exception
{
    /// <summary>
    /// In case exception is created from Api Client, calling some service - it may contain StatusCode returned by server.
    /// </summary>
    public HttpStatusCode? StatusCode { get; set; }

    /// <summary>
    /// Reason for failure, usually sent by server together with message.
    /// </summary>
    public string? ReasonPhrase { get; set; }

    /// <summary>
    /// In case exception is created from Api Client, calling some service - it may contain Method (GET, PUT, POST), used when called service.
    /// </summary>
    public HttpMethod? Method { get; set; }

    /// <summary>
    /// In case exception is created from Api Client, calling some service - it may contain any received content from API (custom error data).
    /// </summary>
    public string? ResponseContent { get; set; }

    /// <summary>
    /// General API Exception for handling exceptional situations.
    /// Added so it can be easier to distinguish custom thrown API exceptions and system default exceptions.
    /// </summary>
    public RestClientException()
    {
    }

    /// <summary>
    /// General API Exception for handling exceptional situations.
    /// Added so it can be easier to distinguish custom thrown API exceptions and system default exceptions.
    /// </summary>
    /// <param name="message">A message that describes the error.</param>
    public RestClientException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// General API Exception for handling exceptional situations.
    /// Added so it can be easier to distinguish custom thrown API exceptions and system default exceptions.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">
    /// The exception that is the cause of the current exception.
    /// If the innerException parameter is not a null reference,
    /// the current exception is raised in a catch block that handles the inner exception.
    /// </param>
    public RestClientException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// General API Exception for handling exceptional situations.
    /// Added so it can be easier to distinguish custom thrown API exceptions and system default exceptions.
    /// Constructor for serialization and deserialization needs.
    /// </summary>
    /// <param name="serializationInfo">Serialization information.</param>
    /// <param name="streamingContext">Serialization Stream context.</param>
    protected RestClientException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
    {
    }
}
