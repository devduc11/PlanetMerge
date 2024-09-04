using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(GetComponentInParentAttribute))]
public class GetComponentInParentDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GetComponentInParentAttribute attr = (GetComponentInParentAttribute)attribute;

        EditorGUI.BeginProperty(position, label, property);

        if (property.propertyType == SerializedPropertyType.ObjectReference)
        {
            if (property.objectReferenceValue == null)
            {
                GameObject gameObject = (property.serializedObject.targetObject as MonoBehaviour).gameObject;

                GameObject go = FindParentByName(gameObject, attr.name);

                if (go != null)
                {
                    property.objectReferenceValue = go;
                }

                //Component component = null;
                //if (attr.name == "")
                //{
                //    component = gameObject.GetComponentInParent(attr.type);
                //}
                //else
                //{
                //    GameObject go = FindParentByName(gameObject, attr.name);

                //    if (go != null)
                //    {
                //        component = go.GetComponent(attr.type);
                //    }
                //}

                //if (component != null)
                //{
                //    property.objectReferenceValue = component;
                //}
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
