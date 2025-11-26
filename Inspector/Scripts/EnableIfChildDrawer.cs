using System;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(EnableIfChildAttribute))]
public class EnableIfChildTypeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EnableIfChildAttribute attr = (EnableIfChildAttribute)attribute;

        SerializedProperty targetProp = property.serializedObject.FindProperty(attr.TargetFieldName);

        bool enabled = false;

        if (targetProp != null && targetProp.propertyType == SerializedPropertyType.ObjectReference)
        {
            UnityEngine.Object obj = targetProp.objectReferenceValue;
            if (obj != null)
            {
                Type valueType = obj.GetType();

                if (attr.RequiredChildType.IsAssignableFrom(valueType))
                    enabled = true;
            }
        }

        GUI.enabled = enabled;
        EditorGUI.PropertyField(position, property, label);
        GUI.enabled = true;
    }
}
