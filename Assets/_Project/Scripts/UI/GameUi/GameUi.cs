using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUi : BaseUI
{
    public static GameUi Instance;
    [SerializeField]
    private Image imageNextBall;
    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
    }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }

    protected override UIName GetName()
    {
        return UIName.GAME_UI;
    }

    public Image GetImageNextBall()
    {
        return imageNextBall;
    }
}
