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
    private Ball ball1, ball2;
    private int count;
    private int countMerge;
    public bool isPauseGame;

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
        UIManager.Instance.ShowUI(UIName.GAME_UI);
    }

    protected override void Start()
    {
        base.Start();
        isPauseGame = true;
        // UIManager.Instance.HideUI(UIName.GAME_UI);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Ball.OnCollisionEnter += OnCollision;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Ball.OnCollisionEnter -= OnCollision;
    }

    private void OnCollision(Ball clone)
    {
        if (count == 0)
        {
            count++;
            ball1 = clone;
        }
        else if (count == 1)
        {
            ball2 = clone;
            count++;
        }

        if (count == 2)
        {
            countMerge++;
            // Debug.Log($"merge {countMerge}");
            Vector3 posMiddle = (ball1.transform.position + ball2.transform.position) / 2;
            print(posMiddle);
            ball1.gameObject.SetActive(false);
            ball2.gameObject.SetActive(false);

            HideBall(ball2);
            ball1.gameObject.SetActive(true);
            ball1.transform.localScale = Vector3.zero;
            ball1.transform.position = posMiddle;
            ball1.transform.rotation = Quaternion.identity;
            ball1.CheckSpriteBallMerge();
            count = 0;
        }
    }

    public void HideBall(Ball ball)
    {
        BallSpawner.Instance.Despawn(ball);
    }


}
