using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(LoadAssetAtPathAttribute))]
public class LoadAssetAtPathDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        LoadAssetAtPathAttribute attr = (LoadAssetAtPathAttribute)attribute;

        EditorGUI.BeginProperty(position, label, property);

        if (property.propertyType == SerializedPropertyType.ObjectReference)
        {
            if (property.objectReferenceValue == null)
            {
                //GameObject go = AssetDatabase.LoadAssetAtPath<GameObject>(attr.path);

                property.objectReferenceValue = AssetDatabase.LoadAssetAtPath(attr.path, attr.type);
            }
        }

        EditorGUI.PropertyField(position, property, label);

        EditorGUI.EndProperty();
    }
}
