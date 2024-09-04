using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class BaseMonoBehaviour : MonoBehaviour
{
    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {

    }

    protected virtual void Reset()
    {
        LoadComponents();
        ResetValue();
    }

    protected virtual void Update()
    {

    }

    protected virtual void FixedUpdate()
    {

    }

    protected virtual void OnEnable()
    {

    }

    protected virtual void OnDisable()
    {

    }

    protected virtual void LoadComponents()
    {

    }

    protected virtual void ResetValue()
    {

    }

    protected T LoadAssetAtPath<T>(string assetPath) where T : Object
    {
#if UNITY_EDITOR
        return AssetDatabase.LoadAssetAtPath<T>(assetPath);
#else
        return null;
#endif
    }

    #region GetComponentInChildren
    protected T GetComponentInChildren<T>(string childName) where T : Component
    {
        return FindChildByName(childName).GetComponent<T>();
    }

    protected GameObject FindChildByName(string name)
    {
        return FindChildByName(gameObject, name);
    }

    protected GameObject FindChildByName(GameObject topParentGameObject, string gameObjectName)
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
    #endregion

    #region GetComponentInParent
    protected T GetComponentInParent<T>(string parentName) where T : Component
    {
        return FindParentByName(parentName).GetComponent<T>();
    }

    protected GameObject FindParentByName(string name)
    {
        return FindParentByName(transform, name);
    }

    protected GameObject FindParentByName(Transform trans, string name)
    {
        if (trans.parent != null)
        {
            if (trans.parent.name == name)
            {
                return trans.parent.gameObject;
            }
            else
            {
                return FindParentByName(trans.parent, name);
            }
        }
        return null;
    }
    #endregion
}
