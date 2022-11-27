using System.Reflection;

namespace popug.sharedlibs;

[System.AttributeUsage(System.AttributeTargets.Assembly)]
public class PopugEventsDomainAttribute : System.Attribute
{
    public readonly string Domain;

    public PopugEventsDomainAttribute(string domain)
    {
        Domain = domain;
    }

    public static string ReadDomainFromAssemblyContainingType(Type type)
    {
        // todo: cache value by type?
        var assembly = type.Assembly;
        var attr = assembly.GetCustomAttribute(typeof(PopugEventsDomainAttribute));
        var popugEventDomainAttr = attr as PopugEventsDomainAttribute;
        return popugEventDomainAttr.Domain;
    }
}