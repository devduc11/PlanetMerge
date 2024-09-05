using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UpgradeGameButton : BaseButton
{
    public static UpgradeGameButton Instance;
    [SerializeField]
    private FeatureController featureController;
    [SerializeField]
    private int sumUpgrade;
    [SerializeField, GetComponentInChildren("Text")]
    protected TextMeshProUGUI text;
    [SerializeField]
    private bool isMinusUpgrade;

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
        CheckBtn();
    }

    public void CheckBtn()
    {
        if (BallSpawner.Instance.transform.childCount == 0 || sumUpgrade <= 0 || BallSpawner.Instance.balls.Count == 0 || !featureController.isPauseFeature) return;
        for (int i = 0; i < BallSpawner.Instance.balls.Count; i++)
        {
            Ball ball = BallSpawner.Instance.balls[i];
            ball.isUpgrade = true;
        }

        isMinusUpgrade = true;
        featureController.isPauseFeature = false;
        GameController.Instance.isPauseGame = false;
        if (isMinusUpgrade)
        {
            sumUpgrade--;
            UpText();
            isMinusUpgrade = false;
        }
    }

    public void UpText()
    {
        text.text = $"{sumUpgrade}";
    }

    public void CheckUpgrade(Ball ball)
    {
        ball.CheckSpriteBallMerge(() =>
        {
            featureController.isPauseFeature = true;
            GameController.Instance.isPauseGame = true;
            for (int i = 0; i < BallSpawner.Instance.transform.childCount; i++)
            {
                Ball ball = BallSpawner.Instance.transform.GetChild(i).GetComponent<Ball>();
                ball.isUpgrade = false;
            }
        });
    }
}
