using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : BaseMonoBehaviour
{
    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
    }
    #endregion

    protected override void Start()
    {
        UIManager.Instance.ShowUI(UIName.HOME_UI);
    }
}
