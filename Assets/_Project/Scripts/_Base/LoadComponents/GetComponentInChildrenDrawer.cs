using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(GetComponentInChildrenAttribute))]
public class GetComponentInChildrenDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GetComponentInChildrenAttribute attr = (GetComponentInChildrenAttribute)attribute;

        EditorGUI.BeginProperty(position, label, property);

        if (property.propertyType == SerializedPropertyType.ObjectReference)
        {
            if (property.objectReferenceValue == null)
            {
                GameObject gameObject = (property.serializedObject.targetObject as MonoBehaviour).gameObject;

                GameObject go = FindChildByName(gameObject, attr.name);

                if (go != null)
                {
                    property.objectReferenceValue = go;
                }

                //Component component = null;
                //if (attr.name == "")
                //{
                //    component = gameObject.GetComponentInChildren(attr.type);
                //}
                //else
                //{
                //    GameObject go = FindChildByName(gameObject, attr.name);

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

    private GameObject FindChildByName(GameObject topParentGameObject, string gameObjectName)
    {
        for (int i = 0; i < topParentGameObject.transform.childCount; i++)
        {
            if (topParentGameObject.transform.GetChild(i).name == gameObjectName)
            {
                return topParentGameObject.transform.GetChild(i).gameObject;
            }

            GameObject tmp = FindChildByName(topParentGameObject.transform.GetChild(i).gameObject, gameObjectName);

            if (tmp != null)
            {
                return tmp;
            }
        }

        return null;
    }
}
