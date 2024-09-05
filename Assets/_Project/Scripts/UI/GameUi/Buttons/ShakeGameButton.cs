using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShakeGameButton : BaseButton
{
    [SerializeField]
    private FeatureController featureController;
    [SerializeField]
    private int sumShake;
    [SerializeField, GetComponentInChildren("Text")]
    protected TextMeshProUGUI text;
    public static event Action<ShakeGameButton> OnBox;

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
        CheckBnt();
    }

    public void CheckBnt()
    {
        if (BallSpawner.Instance.transform.childCount == 0 || sumShake <= 0 || BallSpawner.Instance.balls.Count == 0 || !featureController.isPauseFeature) return;

        GameController.Instance.isPauseGame = false;
        featureController.isPauseFeature = false;
        for (int i = 0; i < BallSpawner.Instance.balls.Count; i++)
        {
            Ball ball = BallSpawner.Instance.balls[i];
            ball.CheckForce();
        }
        sumShake--;
        UpText();
        OnBox?.Invoke(this);
    }

    public void UpText()
    {
        text.text = $"{sumShake}";
    }

    public void CheckShake()
    {
        GameController.Instance.isPauseGame = true;
        featureController.isPauseFeature = true;
    }
}
