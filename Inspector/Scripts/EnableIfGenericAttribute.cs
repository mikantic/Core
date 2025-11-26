using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public class EnableIfGenericAttribute : PropertyAttribute
{
    public Type RequiredType { get; }

    public EnableIfGenericAttribute(Type requiredType)
    {
        RequiredType = requiredType;
    }
}
