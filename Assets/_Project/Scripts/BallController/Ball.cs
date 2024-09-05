using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Ball : BaseMonoBehaviour
{
    [SerializeField, GetComponent()]
    private Rigidbody2D rb;
    [SerializeField, GetComponent()]
    private SpriteRenderer spriteRenderer;
    [SerializeField, LoadAssetAtPath(typeof(SizeBall), "Assets/_Project/ScriptableObject/SizeBall.asset")]
    private SizeBall sizeBall;
    [SerializeField, LoadAssetAtPath(typeof(SpriteBall), "Assets/_Project/ScriptableObject/SpriteBall.asset")]
    private SpriteBall spriteBall;
    [SerializeField]
    public int idBall = 0;
    [SerializeField]
    private int idShop = 0;
    [SerializeField]
    private bool isDrop;
    [SerializeField]
    public bool isBomb;
    [SerializeField]
    private bool isUpgrade;
    [SerializeField]
    private int coin;
    public static event Action<Ball> OnCollisionEnter;

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
    }
    #endregion

    public void CheckSpriteBallSpawner(int idBall, int idShop)
    {
        this.idBall = idBall;
        this.idShop = idShop;
        spriteRenderer.sprite = spriteBall.Sprites[this.idShop].Sprites[this.idBall];
        transform.localScale = new Vector3(sizeBall.ListSize[this.idBall], sizeBall.ListSize[this.idBall]);
    }

    public void CheckSpriteBallMerge()
    {
        idBall++;
        spriteRenderer.sprite = spriteBall.Sprites[idShop].Sprites[idBall];
        transform.DOScale(sizeBall.ListSize[idBall], 0.25f)
        .SetDelay(0.08f)
        .OnComplete(() =>
        {
        });
        // transform.localScale = new Vector3(sizeBall.ListSize[idBall], sizeBall.ListSize[idBall]);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        isDrop = false;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isDrop = true;
            if (idBall == other.transform.GetComponent<Ball>().idBall)
            {
                // print("okoko");
                OnCollisionEnter?.Invoke(this);
            }
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            isDrop = true;
        }
    }

    private void OnMouseDown()
    {
        if (isBomb)
        {
            print("isBomb");
            // BombGameButton.Instance.CheckBomb();
            // Feature.Instance.CheckBomb(this);
        };
        if (isUpgrade)
        {
            print("Upgrade");
            // Feature.Instance.CheckUpgrade(this);
        };
    }


}
