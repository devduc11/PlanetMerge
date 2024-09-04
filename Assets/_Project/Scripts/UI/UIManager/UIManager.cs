using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[Serializable]
public class UIModel
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private UIName uiName;

    public UIModel(GameObject prefab, UIName uiName)
    {
        this.prefab = prefab;
        this.uiName = uiName;
    }

    public GameObject Prefab { get => prefab; set => prefab = value; }
    public UIName UIName { get => uiName; set => uiName = value; }
}

public class UIManager : BaseMonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance => instance;

    [SerializeField] private Transform uiParents;
    [SerializeField] private List<UIModel> listUI;

    [SerializeField] private List<BaseUI> listUIOnScene;

    protected override void Awake()
    {
        base.Awake();
        if (instance == null)
        {
            instance = this;
        }
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadListUI();
        LoadUIParents();
    }

    private void LoadUIParents()
    {
        if (uiParents == null)
        {
            uiParents = GameObject.Find("Canvas").transform;
        }
    }

    private void LoadListUI()
    {
#if UNITY_EDITOR
        if (listUI == null || listUI.Count == 0)
        {
            listUI = new List<UIModel>();
            string[] files = Directory.GetFiles("Assets/_Project/Prefabs/UI", "*.prefab");

            foreach (string file in files)
            {
                GameObject prefab = AssetDatabase.LoadAssetAtPath(file, typeof(GameObject)) as GameObject;
                if (prefab != null)
                {
                    listUI.Add(new UIModel(prefab, prefab.GetComponent<BaseUI>().GetUIName()));
                }
            }
        }
#endif
    }

    public BaseUI ShowUI(UIName uiName)
    {
        BaseUI baseUI = listUIOnScene.Find(bp => bp != null && bp.GetUIName() == uiName);
        if (baseUI != null)
        {
            baseUI.SetActive(true);
            baseUI.transform.SetSiblingIndex(baseUI.transform.parent.childCount - 1);
            return baseUI;
        }
        else
        {
            GameObject prefab = GetUIPrefab(uiName);
            if (prefab != null)
            {
                GameObject obj = Instantiate(prefab, uiParents);
                BaseUI bp = obj.GetComponent<BaseUI>();
                listUIOnScene.Add(bp);
                return bp;
            }
        }

        return null;
    }

    public BaseUI HideUI(UIName uiName)
    {
        BaseUI baseUI = listUIOnScene.Find(bp => bp != null && bp.GetUIName() == uiName);
        if (baseUI != null)
        {
            baseUI.SetActive(false);
        }
        return baseUI;
    }

    public void HideAllUI()
    {
        foreach (var item in listUIOnScene)
        {
            item.SetActive(false);
        }
    }

    private GameObject GetUIPrefab(UIName uiName)
    {
        foreach (var item in listUI)
        {
            if (item.UIName == uiName)
            {
                return item.Prefab;
            }
        }
        return null;
    }
}
