using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : BaseMonoBehaviour
{
    [SerializeField, GetComponent()]
    private LineRenderer lr;
    // [SerializeField]
    // private List<Transform> points;
    [SerializeField]
    private BallController ballController;

    [SerializeField, FindChildren("Bot")]
    private Transform botTransform;

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBallController();
    }

    private void LoadBallController()
    {
        ballController = FindObjectOfType<BallController>();
    }
    #endregion

    protected override void Update()
    {
        if (lr.enabled)
        {
            lr.SetPosition(0, ballController.transform.position);
            lr.SetPosition(1, new Vector3(ballController.transform.position.x, botTransform.position.y, 0));
        }
    }

    public void CheckEnabledLine(bool isEnabled)
    {
        lr.enabled = isEnabled;
    }
}
