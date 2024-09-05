using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BallController : BaseMonoBehaviour
{
    [SerializeField, GetComponent()]
    private SpriteRenderer spriteRenderer;
    [SerializeField, LoadAssetAtPath(typeof(SizeBall), "Assets/_Project/ScriptableObject/SizeBall.asset")]
    private SizeBall sizeBall;
    [SerializeField, LoadAssetAtPath(typeof(SpriteBall), "Assets/_Project/ScriptableObject/SpriteBall.asset")]
    private SpriteBall spriteBall;
    [SerializeField, LoadAssetAtPath(typeof(BallPercentageList), "Assets/_Project/ScriptableObject/BallPercentageList.asset")]
    private BallPercentageList ballPercentageList;

    [SerializeField]
    private Transform ballClampPos1;
    [SerializeField]
    private Transform ballClampPos2;
    [SerializeField]
    public int idBall;
    [SerializeField]
    public int idBallNext;
    [SerializeField]
    private int minBall;
    [SerializeField]
    private int maxBall;
    [SerializeField]
    public int idShop;
    [SerializeField]
    private int idPercentages;
    [SerializeField]
    private bool isCheckMove;
    public static event Action<BallController> OnNextBallImage;
    [SerializeField]
    private LineController lineController;
    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadLineController();
        ballClampPos1 = GameObject.Find("BallClampPos1").transform;
        ballClampPos2 = GameObject.Find("BallClampPos2").transform;
    }

    private void LoadLineController()
    {
        lineController = FindObjectOfType<LineController>();
    }
    #endregion

    protected override void Start()
    {
        base.Start();
        CheckSizeItemsAndSprites();
        lineController.CheckEnabledLine(false);
    }

    protected override void Update()
    {
        if (Input.GetMouseButton(0) && EventSystem.current.currentSelectedGameObject == null && GameController.Instance.isPauseGame)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 1f;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.position = new Vector3(worldPos.x, transform.position.y, transform.position.z);
            lineController.CheckEnabledLine(true);
        }

        if (Input.GetMouseButtonUp(0) && EventSystem.current.currentSelectedGameObject == null && GameController.Instance.isPauseGame)
        {
            lineController.CheckEnabledLine(false);
            GameController.Instance.isPauseGame = false;
            CloudController.Instance.CheckPosStart(this);
            CheckBallSpawner();
        }

        float posX = Mathf.Clamp(transform.position.x, ballClampPos1.position.x + sizeBall.ListSize[idBall], ballClampPos2.position.x - sizeBall.ListSize[idBall]);
        transform.position = new Vector3(posX, transform.position.y);
    }

    public void CheckBallSpawner()
    {
        Ball ball = BallSpawner.Instance.Spawn(transform.position, true);
        ball.CheckSpriteBallSpawner(idBall, idShop);
    }

    public void CheckSizeItemsAndSprites()
    {
        if (BallSpawner.Instance.transform.childCount == 0)
        {
            idBall = 0;
        }
        else
        {
            idBall = idBallNext;
        }
        transform.localScale = Vector3.zero;
        CheckSprite(idBall);
        for (int i = 0; i < sizeBall.ListSize.Count; i++)
        {
            if (idBall == i)
            {
                transform.DOScale(sizeBall.ListSize[idBall], 0.2f)
                .OnComplete(() =>
                {
                    GameController.Instance.isPauseGame = true;
                });
            }
        }
        CheckBallNext();
    }


    public int CheckIdBall()
    {
        int id = 0;
        float percent = UnityEngine.Random.Range(0, 100f);
        // print($"percent: {percent}");
        int NumberOfFruits = ballPercentageList.Percentages[idPercentages].NumberOfFruits;
        // if (maxItems > NumberOfFruits)
        // {
        //     idPercentages++;
        //     NumberOfFruits = ballPercentageList.Percentages[idPercentages].NumberOfFruits;
        // }
        // print($"NumberOfFruits: {NumberOfFruits}");

        float cumulativePercent = 0f;
        for (int i = 0; i < ballPercentageList.Percentages[idPercentages].Percentages.Count; i++)
        {
            cumulativePercent += ballPercentageList.Percentages[idPercentages].Percentages[i];
            if (percent <= cumulativePercent)
            {
                // print($"qua {i + 1}");
                // print($"qua {i}");
                // CheckSprite(i, 0);
                id = i;
                break;
            }
        }
        return id;
    }

    public void CheckBallNext()
    {
        idBallNext = CheckIdBall();
        // idBallNext = UnityEngine.Random.Range(0, 6);
        OnNextBallImage?.Invoke(this);
    }

    public void CheckSprite(int idBall)
    {
        spriteRenderer.sprite = spriteBall.Sprites[idShop].Sprites[idBall];
    }
}
