using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeUI : BaseUI
{

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
    }
    #endregion

    protected override UIName GetName()
    {
        return UIName.HOME_UI;
    }
}
