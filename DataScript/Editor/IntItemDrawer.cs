using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(IntItem))]
public class IntItemDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        var idRect = new Rect(position.x, position.y, position.width / 3 * 2, position.height);
        var dataRect = new Rect(position.x + (position.width / 3 * 2) + 5, position.y, position.width / 3, position.height);

        EditorGUI.PropertyField(idRect, property.FindPropertyRelative("id"), GUIContent.none);
        EditorGUI.PropertyField(dataRect, property.FindPropertyRelative("data"), GUIContent.none);

        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }
}
