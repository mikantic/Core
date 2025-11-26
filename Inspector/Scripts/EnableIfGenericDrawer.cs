using UnityEditor;
using UnityEngine;
using System;

[CustomPropertyDrawer(typeof(EnableIfGenericAttribute))]
public class EnableIfGenericTypeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EnableIfGenericAttribute attr = (EnableIfGenericAttribute)attribute;

        object target = property.serializedObject.targetObject;
        Type targetType = target.GetType();

        bool enabled = false;

        // Walk up the inheritance hierarchy to find the generic base class
        while (targetType != null && !targetType.IsGenericType)
        {
            targetType = targetType.BaseType;
        }

        if (targetType != null && targetType.IsGenericType)
        {
            Type genericArg = targetType.GetGenericArguments()[0];
            enabled = attr.RequiredType.IsAssignableFrom(genericArg);
        }

        GUI.enabled = enabled;
        EditorGUI.PropertyField(position, property, label);
        GUI.enabled = true;
    }
}
