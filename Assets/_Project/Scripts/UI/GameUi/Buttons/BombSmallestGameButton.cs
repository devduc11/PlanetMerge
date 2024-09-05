using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class BombSmallestGameButton : BaseButton
{
    [SerializeField]
    private FeatureController featureController;
    [SerializeField]
    private int sumBombSmallest;
    [SerializeField, GetComponentInChildren("Text")]
    protected TextMeshProUGUI text;

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

    protected override void Start()
    {
        base.Start();
        UpText();
    }

    protected override void OnClick()
    {
        CheckBtn();
    }

    public void CheckBtn()
    {
        if (BallSpawner.Instance.transform.childCount == 0 || sumBombSmallest <= 0 || BallSpawner.Instance.balls.Count == 0 || !featureController.isPauseFeature) return;
        featureController.isPauseFeature = false;
        GameController.Instance.isPauseGame = false;
        int smallestIdBall = int.MaxValue;
        for (int i = 0; i < BallSpawner.Instance.balls.Count; i++)
        {
            Ball ball = BallSpawner.Instance.balls[i];
            if (ball.idBall < smallestIdBall)
            {
                smallestIdBall = ball.idBall;
                print($"id min: {smallestIdBall}");
            }
        }

        // int sumCoin = 0;
        for (int i = 0; i < BallSpawner.Instance.balls.Count; i++)
        {
            Ball ball = BallSpawner.Instance.balls[i];
            if (ball.idBall == smallestIdBall)
            {
                ball.transform.DOScale(0, 0.25f).SetEase(Ease.InBack).OnComplete(() =>
                {
                    ball.gameObject.SetActive(false);
                    GameController.Instance.HideBall(ball);
                    featureController.isPauseFeature = true;
                    GameController.Instance.isPauseGame = true;
                });
            }
        }
        sumBombSmallest--;
        UpText();
    }

    public void UpText()
    {
        text.text = $"{sumBombSmallest}";
    }
}
