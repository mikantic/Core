using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public class EnableIfChildAttribute : PropertyAttribute
{
    public string TargetFieldName { get; }
    public Type RequiredChildType { get; }

    public EnableIfChildAttribute(string targetFieldName, Type requiredChildType)
    {
        TargetFieldName = targetFieldName;
        RequiredChildType = requiredChildType;
    }
}
