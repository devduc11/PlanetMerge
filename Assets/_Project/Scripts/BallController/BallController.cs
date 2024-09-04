using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BallController : BaseMonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private SizeBall sizeBall;
    [SerializeField]
    private SpriteBall spriteBall;
    [SerializeField]
    private Transform ballClampPos1;
    [SerializeField]
    private Transform ballClampPos2;
    [SerializeField]
    private int idBall;
    [SerializeField]
    private int idShop;
    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
    }
    #endregion

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (Input.GetMouseButton(0) && EventSystem.current.currentSelectedGameObject == null && GameController.Instance.isPauseGame)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 1f;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.position = new Vector3(worldPos.x, transform.position.y, transform.position.z);
        }

        if (Input.GetMouseButtonUp(0) && EventSystem.current.currentSelectedGameObject == null && GameController.Instance.isPauseGame)
        {
            // isCheckMove = false;
            // cloud.CheckPosStart();
            // gameObject.SetActive(false);
            // GameScene.Instance.ShowItemsClone(this);
            // GameScene.Instance.lineController.lr.enabled = false;
            // Invoke("CheckSizeItemsAndSprites", 1);
            BallSpawner.Instance.Spawn(transform.position, true);
        }

        float posX = Mathf.Clamp(transform.position.x, ballClampPos1.position.x + sizeBall.ListSize[idBall], ballClampPos2.position.x - sizeBall.ListSize[idBall]);
        transform.position = new Vector3(posX, transform.position.y);

    }

    public void CheckSprite(int idBall, int status)
    {
        if (status == 0)
        {
            spriteRenderer.sprite = spriteBall.Sprites[idShop].Sprites[idBall];
        }
        else if (status == 1)
        {
            // GameUi.Instance.GetImageNextBall();
            // imageNextBall.sprite = spriteBall.Sprites[idShop].Sprites[idBall];
            // imageNextBall.SetNativeSize();
        }
    }
}
