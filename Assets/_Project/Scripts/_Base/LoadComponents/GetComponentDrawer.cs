using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(GetComponentAttribute))]
public class GetComponentDrawer : PropertyDrawer
{

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //GetComponentAttribute attr = (GetComponentAttribute)attribute;

        EditorGUI.BeginProperty(position, label, property);

        if (property.propertyType == SerializedPropertyType.ObjectReference)
        {
            if (property.objectReferenceValue == null)
            {
                GameObject gameObject = (property.serializedObject.targetObject as MonoBehaviour).gameObject;
                property.objectReferenceValue = gameObject;

                //Component component = gameObject.GetComponent(attr.type);

                //if (component != null)
                //{
                //    property.objectReferenceValue = component;
                //}
            }
        }

        EditorGUI.PropertyField(position, property, label);

        EditorGUI.EndProperty();
    }
}
