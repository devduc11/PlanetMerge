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
    public static GameController Instance;
    public bool isPauseGame;

    protected override void Awake()
    {
        base.Awake();
        Instance = this;

    }

    protected override void Start()
    {
        base.Start();
        isPauseGame = true;
        UIManager.Instance.ShowUI(UIName.GAME_UI);
        // UIManager.Instance.HideUI(UIName.GAME_UI);
    }
}
