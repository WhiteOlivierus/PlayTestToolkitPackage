using PlayTestToolkit.Runtime.DataRecorders;
using UnityEditor;
using UnityEngine;

namespace PlayTestToolkit.Editor.UI
{
    [CustomPropertyDrawer(typeof(BaseRecorder))]
    public class BaseRecorderDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            bool active = property.FindPropertyRelative("active").boolValue;
            string recorderName = property.FindPropertyRelative("recorderName").stringValue;

            active = EditorGUI.Toggle(position, new GUIContent(recorderName), active);

            EditorGUI.EndProperty();

        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 15;
        }
    }
}
