using UnityEditor;
using UnityEngine;

namespace PlayTestToolkit.Runtime.Data
{
    [CustomPropertyDrawer(typeof(Collector))]
    public class CollectorDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty active = property.FindPropertyRelative("active");
            SerializedProperty name = property.FindPropertyRelative("name");

            active.boolValue = EditorGUILayout.Toggle(name.stringValue, active.boolValue);

            EditorGUI.EndProperty();
        }
    }
}
