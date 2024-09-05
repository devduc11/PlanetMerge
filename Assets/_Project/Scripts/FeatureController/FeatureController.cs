using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatureController : BaseMonoBehaviour
{
    [SerializeField]
    public bool isPauseFeature;
    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
    }
    #endregion

    protected override void Start()
    {
        base.Start();
        isPauseFeature = true;
    }

    public void CheckPauseFeature(bool isPause)
    {
        isPauseFeature = isPause;
    }
}
