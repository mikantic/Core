using System;
using UnityEditor;
using UnityEngine;

namespace Core.Properties.Editor
{
    [CustomPropertyDrawer(typeof(Property<>), true)]
    public class PropertyDrawer : UnityEditor.PropertyDrawer
    {
        public override void OnGUI(
            Rect position,
            SerializedProperty property,
            GUIContent label)
        {
            SerializedProperty value = property.FindPropertyRelative("_value");

            TryAssignComponent(property, value);

            EditorGUI.PropertyField(position, value, label);
        }

        private void TryAssignComponent(
            SerializedProperty property,
            SerializedProperty value)
        {
            // Not an object reference → definitely not a Component.
            if (value.propertyType != SerializedPropertyType.ObjectReference)
                return;

            // Get the type inside Property<T>.
            Type propertyType = fieldInfo.FieldType;

            if (!propertyType.IsGenericType)
                return;

            Type valueType = propertyType.GetGenericArguments()[0];

            // T isn't a Component → do nothing.
            if (!typeof(Component).IsAssignableFrom(valueType))
                return;

            // The object containing the Property<T> needs to be a Component.
            if (property.serializedObject.targetObject is not Component owner)
                return;

            // Don't overwrite an existing assignment.
            if (value.objectReferenceValue != null)
                return;

            Component component = owner.GetComponent(valueType);

            if (component != null)
            {
                value.objectReferenceValue = component;
                property.serializedObject.ApplyModifiedProperties();
            }
        }

        // public override float GetPropertyHeight(
        //     SerializedProperty property,
        //     GUIContent label)
        // {
        //     return EditorGUI.GetPropertyHeight(
        //         property.FindPropertyRelative("_value"),
        //         label);
        // }
    }
}