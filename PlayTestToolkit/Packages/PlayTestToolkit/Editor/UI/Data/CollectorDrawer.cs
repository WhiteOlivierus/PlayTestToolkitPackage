using UnityEditor;
using UnityEngine;

namespace PlayTestToolkit.Runtime.Data
{
    [CustomPropertyDrawer(typeof(DataCollector))]
    public class CollectorDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty active = property.FindPropertyRelative("active");
            SerializedProperty recorder = property.FindPropertyRelative("recorder");

            active.boolValue = EditorGUI.Toggle(position, recorder.objectReferenceValue.ToString(), active.boolValue);

            position.y += EditorGUI.GetPropertyHeight(property);

            EditorGUI.EndProperty();
        }
    }
}
