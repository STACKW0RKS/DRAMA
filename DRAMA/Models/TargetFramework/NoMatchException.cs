namespace DRAMA.Models.TargetFramework;

/// <summary>
///     The exception that is thrown when the criteria specified for querying the elements in a collection does not match any elements in the collection.
///     Can also be used when comparing single values, e.g. string comparison or glob/regex pattern matching.
/// </summary>
[Serializable]
public class NoMatchException : SystemException
{
    /// <summary>
    ///     Initialises a NoMatchException that takes no arguments.
    /// </summary>
    public NoMatchException() { }

    /// <summary>
    ///     Initialises a NoMatchException that takes a nullable exception message argument.
    /// </summary>
    public NoMatchException(string? message) : base(message) { }

    /// <summary>
    ///     Initialises a NoMatchException that takes a nullable exception message and a nullable inner exception for arguments.
    /// </summary>
    public NoMatchException(string? message, Exception? innerException) : base(message, innerException) { }

    /// <summary>
    ///     Initialises a NoMatchException that takes serialisation information and a streaming context for arguments.
    /// </summary>
    protected NoMatchException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
