using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using TMPro;

public class BombGameButton : BaseButton
{
    public static BombGameButton Instance;
    [SerializeField, GetComponentInChildren("Text")]
    protected TextMeshProUGUI text;
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

    protected override void Start()
    {
        base.Start();
        UpText();
    }

    protected override void OnClick()
    {
        CheckBnt();
    }

    public void CheckBnt()
    {
        if (BallSpawner.Instance.transform.childCount == 0 || sumBomb <= 0 || BallSpawner.Instance.balls.Count == 0 || !featureController.isPauseFeature) return;
        // print("CheckBomb");
        for (int i = 0; i < BallSpawner.Instance.balls.Count; i++)
        {
            Ball ball = BallSpawner.Instance.balls[i];
            ball.isBomb = true;
        }

        isMinusBomb = true;
        featureController.isPauseFeature = false;
        GameController.Instance.isPauseGame = false;
        if (isMinusBomb)
        {
            sumBomb--;
            isMinusBomb = false;
            UpText();
        }
    }

    public void UpText()
    {
        text.text = $"{sumBomb}";
    }

    public void CheckBomb(Ball ball)
    {
        ball.transform.DOScale(0, 0.25f).SetEase(Ease.InBack).OnComplete(() =>
        {
            ball.gameObject.SetActive(false);
            GameController.Instance.HideBall(ball);
            featureController.isPauseFeature = true;
            GameController.Instance.isPauseGame = true;
            for (int i = 0; i < BallSpawner.Instance.transform.childCount; i++)
            {
                Ball ball = BallSpawner.Instance.transform.GetChild(i).GetComponent<Ball>();
                ball.isBomb = false;
            }
        });
    }
}
