using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloudController : BaseMonoBehaviour
{
    [SerializeField]
    private Transform ballClampPos1;
    [SerializeField]
    private Transform ballClampPos2;
    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
    }
    #endregion

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
