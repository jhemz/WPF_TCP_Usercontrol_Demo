using System;

public enum PropertyType {Internal, Display }
[AttributeUsage(AttributeTargets.Property |
                       System.AttributeTargets.Struct)
]
public class PropertyAttribute : Attribute
{
    public PropertyType PropertyType;
    public string DisplayName;

    public PropertyAttribute(PropertyType propertyType, string displayName = "")
    {
        this.PropertyType = propertyType;
        this.DisplayName = displayName;
    }
}
