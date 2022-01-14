namespace DRAMA.Models.SystemUnderTest;

public class User
{
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
    public Dictionary<string, string> Context { get; set; }
}
