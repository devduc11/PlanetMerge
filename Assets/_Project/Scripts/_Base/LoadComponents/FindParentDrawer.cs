using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(FindParentAttribute))]
public class FindParentDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        FindChildrenAttribute attr = (FindChildrenAttribute)attribute;

        EditorGUI.BeginProperty(position, label, property);

        if (property.propertyType == SerializedPropertyType.ObjectReference)
        {
            if (property.objectReferenceValue == null)
            {
                GameObject gameObject = (property.serializedObject.targetObject as MonoBehaviour).gameObject;

                GameObject children = FindParentByName(gameObject, attr.name);

                if (children != null)
                {
                    property.objectReferenceValue = children;
                }
            }
        }

        EditorGUI.PropertyField(position, property, label);

        EditorGUI.EndProperty();
    }

    protected GameObject FindParentByName(GameObject gameObject, string name)
    {
        if (gameObject.transform.parent != null)
        {
            if (gameObject.transform.parent.name == name)
            {
                return gameObject.transform.parent.gameObject;
            }
            else
            {
                return FindParentByName(gameObject.transform.parent.gameObject, name);
            }
        }
        return null;
    }
}
