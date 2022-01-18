namespace DRAMA.Models.SystemUnderTest;

/// <summary>
///     The model for the a user of the system under test.
/// </summary>
public class User
{
    /// <summary>
    ///     Instantiates a new user with the defined identifier and password.
    /// </summary>
    public User(string identifier, string password)
    {
        Identifier = identifier;
        Password = password;
    }

    /// <summary>
    ///     The user account's identifier, e.g. username or email address.
    /// </summary>
    public string Identifier { get; init; }

    /// <summary>
    ///     The user account's password.
    /// </summary>
    public string Password { get; init; }

    /// <summary>
    ///     A store for domain-specific user data, e.g. user roles or permissions.
    /// </summary>
    public Dictionary<string, string>? Context { get; set; }
}
