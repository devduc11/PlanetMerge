using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloudController : BaseMonoBehaviour
{
    public static CloudController Instance;
    [SerializeField]
    private Transform ballClampPos1;
    [SerializeField]
    private Transform ballClampPos2;
    [SerializeField]
    private Transform posCloud;
    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        ballClampPos1 = GameObject.Find("BallClampPos1").transform;
        ballClampPos2 = GameObject.Find("BallClampPos2").transform;
        posCloud = GameObject.Find("PosCloud").transform;
    }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        Instance = this;

    }

    public void CheckPosStart(BallController ballController)
    {
        ballController.gameObject.SetActive(false);
        transform.DOMove(posCloud.position, 0.2f)
        .OnComplete(() =>
        {
            ballController.transform.position = transform.position;
            DOVirtual.DelayedCall(0.5f, () =>
            {
                ballController.gameObject.SetActive(true);
                ballController.CheckSizeItemsAndSprites();
            });
        });
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

        float posX = Mathf.Clamp(transform.position.x, ballClampPos1.position.x + 0.5f, ballClampPos2.position.x - 0.5f);
        transform.position = new Vector3(posX, transform.position.y);
    }
}
