using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : BaseUI
{
    // public static GameUI Instance;
    [SerializeField, GetComponentInChildren("nextBallImage")]
    private Image nextBallImage;
    [SerializeField, LoadAssetAtPath(typeof(SpriteBall), "Assets/_Project/ScriptableObject/SpriteBall.asset")]
    private SpriteBall spriteBall;
    // public static event Action<GameUI> OnNextBallImage;
    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
    }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        // nextBallImage.sprite = spriteBall.Sprites[0].Sprites[1];
        // Instance = this;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        BallController.OnNextBallImage += CheckNextBallImage;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        BallController.OnNextBallImage -= CheckNextBallImage;
    }

    private void CheckNextBallImage(BallController ballController)
    {
        nextBallImage.transform.localScale = Vector3.zero;
        nextBallImage.transform.DOScale(0f, 0.2f)
        .OnComplete(() =>
        {
            nextBallImage.sprite = spriteBall.Sprites[ballController.idShop].Sprites[ballController.idBallNext];
            nextBallImage.transform.DOScale(1f, 0.2f);
        });
    }


    protected override UIName GetName()
    {
        return UIName.GAME_UI;
    }
}
