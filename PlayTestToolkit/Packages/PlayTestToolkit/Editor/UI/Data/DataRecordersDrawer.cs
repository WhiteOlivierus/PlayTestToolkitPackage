using PlayTestToolkit.Runtime.Data;
using UnityEditor;
using UnityEngine;

namespace PlayTestToolkit.Editor.UI
{
    [CustomPropertyDrawer(typeof(DataRecorders))]
    public class DataRecordersDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty dataCollectors = property.FindPropertyRelative("collectors");

            for (int i = 0; i < dataCollectors.arraySize; i++)
                EditorGUI.PropertyField(position, dataCollectors.GetArrayElementAtIndex(i));

            EditorGUI.EndProperty();
        }

    }
}
