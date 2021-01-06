using UnityEditor;
using UnityEngine;

namespace PlayTestToolkit.Runtime.Data
{
    [CustomPropertyDrawer(typeof(Collectors))]
    public class CollectorsDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //EditorGUI.BeginProperty(position, label, property);

            //EditorGUI.LabelField(position, property.displayName);

            //SerializedProperty dataCollectors = property.FindPropertyRelative("collectors");

            //for (int i = 0; i < dataCollectors.arraySize; i++)
            //{
            //    SerializedProperty dataCollector = dataCollectors.GetArrayElementAtIndex(i);

            //    EditorGUI.PropertyField(position,dataCollector);
            //}

            //EditorGUI.EndProperty();
        }
    }
}
