using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : BaseMonoBehaviour
{
    [SerializeField, GetComponent()]
    private Image image;

    [SerializeField, GetComponentInChildren("Children 2")]
    private Image imageInChild;

    [SerializeField, GetComponentInParent("Parent")]
    private Image imageInParent;

    [SerializeField, LoadAssetAtPath(typeof(SaveManager), "Assets/_Project/Prefabs/Save/SaveManager.prefab")]
    private SaveManager saveManager;

    [SerializeField, FindChildren("Children 2")]
    private Image childrenA;

    [SerializeField, FindParent("Parent")]
    private GameObject parentB;

    [SerializeField, GetComponents(typeof(Image))]
    private ListComponent<Image> saveManagers;

    [SerializeField, GetComponentsInChildren(typeof(SaveManager), "Children 2")]
    private ListComponent<SaveManager> saveManagerInChildren;

    [SerializeField, GetComponentsInParent(typeof(SaveManager), "Parent")]
    private ListComponent<SaveManager> saveManagerInParant;

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
    }
    #endregion

    protected override void Start()
    {
        base.Start();
        TimeManager.Instance.Request();
    }
}
