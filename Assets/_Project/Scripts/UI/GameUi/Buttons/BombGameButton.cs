using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BombGameButton : BaseButton
{
    public static BombGameButton Instance;
    [SerializeField]
    private int sumBomb;
    [SerializeField]
    private FeatureController featureController;
    [SerializeField]
    private bool isMinusBomb;

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadFeatureController();
    }

    private void LoadFeatureController()
    {
        featureController = GetComponentInParent<FeatureController>();
    }

    #endregion

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }

    protected override void OnClick()
    {
        CheckBomb();
    }

    public void CheckBomb()
    {
        if (BallSpawner.Instance.transform.childCount == 0 || sumBomb <= 0 || !featureController.isPauseFeature) return;
        print("CheckBomb");
        for (int i = 0; i < BallSpawner.Instance.balls.Count; i++)
        {
            Ball ball = BallSpawner.Instance.balls[i];
            ball.isBomb = true;
            // Ball ball = BallSpawner.Instance.transform.GetChild(i).GetComponent<Ball>();
            // // print(ball.name);
            // if (ball.gameObject.activeSelf)
            // {
            //     ball.isBomb = true;
            // }
        }

        isMinusBomb = true;
        featureController.isPauseFeature = false;
        GameController.Instance.isPauseGame = false;
        if (isMinusBomb)
        {
            sumBomb--;
            isMinusBomb = false;
        }
    }

    public void CheckBomb(Ball ball)
    {
        ball.transform.DOScale(0, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
        {
            ball.gameObject.SetActive(false);
        });
        // EffectItemsClone(itemsClone, 0, 0.25f, () =>
        // {
        //     itemsClone.gameObject.SetActive(false);
        //     gameScene.objectPoolItemsClone.Add(itemsClone);
        //     for (int i = 0; i < gameScene.ItemsCloneParent.childCount; i++)
        //     {
        //         ItemsClone itemsClone = gameScene.ItemsCloneParent.GetChild(i).GetComponent<ItemsClone>();
        //         itemsClone.isBomb = false;
        //     }
        //     gameScene.isPauseGame = true;
        //     isPauseFeature = true;
        //     StopInfiniteLoopEffectSelect();
        // });
        // gameScene.CheckScore(itemsClone.coin);
    }
}
