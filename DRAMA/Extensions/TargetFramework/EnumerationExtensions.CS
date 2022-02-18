using System.Reflection;
using DRAMA.Attributes;

namespace DRAMA.Extensions.TargetFramework;

public static class EnumerationExtensions
{
    public static string ToTextValue(this Enum enumeration)
    {
        Type type = enumeration.GetType();

        MemberInfo[] memberInfo = type.GetMember(enumeration.ToString());

        if (memberInfo.None()) return enumeration.ToString();

        object[] attributes = memberInfo.Single().GetCustomAttributes(typeof(TextValueAttribute), false);

        return attributes.Any() ? ((TextValueAttribute)attributes.Single()).Text : enumeration.ToString();
    }
}
