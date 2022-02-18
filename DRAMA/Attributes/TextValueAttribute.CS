namespace DRAMA.Attributes;

[AttributeUsage(AttributeTargets.Field, Inherited = false)]
public class TextValueAttribute : Attribute
{
    public TextValueAttribute(string text)
        => Text = text;

    public string Text { get; set; }
}
