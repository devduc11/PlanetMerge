using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BoxController : BaseMonoBehaviour
{
    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
    }
    #endregion


    protected override void OnEnable()
    {
        base.OnEnable();
        ShakeGameButton.OnBox += CheckBoxShake;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        ShakeGameButton.OnBox -= CheckBoxShake;
    }

    private void CheckBoxShake(ShakeGameButton shakeGameButton)
    {
        print("box");
        transform.DOShakePosition(1f, 1, 10, 90)
        .OnComplete(() =>
        {
            shakeGameButton.CheckShake();
        });
    }
}
