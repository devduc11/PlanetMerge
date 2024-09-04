using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectRatio : BaseMonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float targetAspect = 0.5625f;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCamera();
    }

    private void LoadCamera()
    {
        cam = GetComponent<Camera>();
    }

    protected override void Awake()
    {
        base.Awake();
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        cam.orthographicSize /= scaleHeight;
    }
}