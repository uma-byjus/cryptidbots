using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(ConditionalHidingAttribute))]
public class ConditionalHideDrawer : PropertyDrawer
{
    public override void OnGUI(Rect pos, SerializedProperty property, GUIContent label) {
        ConditionalHidingAttribute condHAtt = (ConditionalHidingAttribute)attribute;

        bool enabled = GetConditionalHideAttributeResult(condHAtt, property);
 
        bool wasEnabled = GUI.enabled;
        GUI.enabled = enabled;
        if (!condHAtt.HideInInspector || enabled) {
            EditorGUI.PropertyField(pos, property, label, true);
        }
 
        GUI.enabled = wasEnabled;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        ConditionalHidingAttribute condHAtt = (ConditionalHidingAttribute)attribute;
        bool enabled = GetConditionalHideAttributeResult(condHAtt, property);

        if (!condHAtt.HideInInspector || enabled) {
            return EditorGUI.GetPropertyHeight(property, label);
        } else {
            return -EditorGUIUtility.standardVerticalSpacing;
        }
    }
    
    private bool GetConditionalHideAttributeResult(ConditionalHidingAttribute condHAtt, SerializedProperty property) {
        bool enabled = true;
        string propertyPath = property.propertyPath;
        string conditionPath = propertyPath.Replace(property.name, condHAtt.ConditionalSourceField);
        SerializedProperty sourcePropertyValue = property.serializedObject.FindProperty(conditionPath);
    
        if (sourcePropertyValue != null) {
            enabled = sourcePropertyValue.boolValue;
        } else {
            Debug.LogWarning("Attempting to use a ConditionalHideAttribute but no matching SourcePropertyValue found in object: " + condHAtt.ConditionalSourceField);
        }
    
        return enabled;
    }
}
