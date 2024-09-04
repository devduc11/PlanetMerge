using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomPropertyDrawer(typeof(GetComponentsInParentAttribute))]
public class GetComponentsInParentDrawer : PropertyDrawer
{
    private ReorderableList reorderableList;

    private void Initialize(SerializedProperty property)
    {
        SerializedProperty arrayProperty = property.FindPropertyRelative("Components");

        reorderableList ??= new ReorderableList(property.serializedObject, arrayProperty, true, true, false, false)
        {
            //drawHeaderCallback = (Rect rect) =>
            //{
            //    EditorGUI.LabelField(rect, property.displayName);
            //},
            drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                SerializedProperty element = arrayProperty.GetArrayElementAtIndex(index);
                rect.y += 2;
                // Tạo nhãn cho từng phần tử
                Rect labelRect = new(rect.x + 12, rect.y, EditorGUIUtility.labelWidth, EditorGUIUtility.singleLineHeight);
                EditorGUI.LabelField(labelRect, $"Element {index}");
                // Vẽ phần tử
                Rect fieldRect = new(rect.x + EditorGUIUtility.labelWidth - 15, rect.y, rect.width - EditorGUIUtility.labelWidth + 15, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(fieldRect, element, GUIContent.none);
            },
            elementHeightCallback = (index) =>
            {
                return EditorGUIUtility.singleLineHeight + 2;
            }
        };
        reorderableList.headerHeight = 0;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GetComponentsInParentAttribute attr = (GetComponentsInParentAttribute)attribute;

        EditorGUI.BeginProperty(position, label, property);

        // Initialize the reorderable list
        Initialize(property);

        // Calculate and draw the foldout
        Rect foldoutRect = new(position.x - 3, position.y, 15, EditorGUIUtility.singleLineHeight);
        property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, GUIContent.none);

        // Reserve space for the label
        Rect lable = position;
        lable.x += 12;
        EditorGUI.PrefixLabel(lable, GUIUtility.GetControlID(FocusType.Passive), label);

        // Draw the size field on the right of the label
        Rect sizeRect = new(position.x + position.width - 53, position.y, 50, EditorGUIUtility.singleLineHeight);

        if (property.isExpanded)
        {
            // Calculate the total height of the reorderable list
            Rect listRect = new(position.x - 3, position.y + EditorGUIUtility.singleLineHeight, position.width, reorderableList.GetHeight());

            SerializedProperty arrayProperty = property.FindPropertyRelative("Components");

            if (arrayProperty != null && arrayProperty.isArray)
            {
                GameObject gameObject = (property.serializedObject.targetObject as MonoBehaviour).gameObject;

                Component[] components = null;
                if (attr.name == "")
                {
                    components = gameObject.GetComponentsInParent(attr.type);
                }
                else
                {
                    GameObject go = FindParentByName(gameObject, attr.name);
                    if (go != null)
                    {
                        components = go.GetComponents(attr.type);
                    }
                }

                if (components != null)
                {
                    arrayProperty.arraySize = components.Length;

                    // Draw the size field
                    GUI.enabled = false;
                    arrayProperty.arraySize = EditorGUI.IntField(sizeRect, arrayProperty.arraySize);
                    GUI.enabled = true;
                    GetComponents(arrayProperty, components);

                    // Draw the reorderable list
                    reorderableList.DoList(listRect);
                }
            }
            else
            {
                EditorGUI.LabelField(position, "Use LoadComponents with an array of Components.");
            }
        }

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

    private void GetComponents(SerializedProperty arrayProperty, Component[] components)
    {
        for (int i = 0; i < components.Length; i++)
        {
            SerializedProperty elementProp = arrayProperty.GetArrayElementAtIndex(i);
            if (elementProp.propertyType == SerializedPropertyType.ObjectReference)
            {
                if (elementProp.objectReferenceValue == null)
                {
                    elementProp.objectReferenceValue = components[i];
                }
            }
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (property.isExpanded)
        {
            Initialize(property);  // Ensure reorderableList is initialized
            return reorderableList.GetHeight();
        }
        return EditorGUIUtility.singleLineHeight;
    }
}
