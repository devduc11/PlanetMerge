using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class BaseButton : BaseMonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] protected Button button;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (button == null)
        {
            button = GetComponent<Button>();
            ColorBlock colors = button.colors;
            colors.pressedColor = Color.white;
            colors.highlightedColor = Color.white;
            colors.selectedColor = Color.white;
            button.colors = colors;
        }
    }

    protected override void Start()
    {
        base.Start();
        AddOnClickEvent();
    }

    protected virtual void AddOnClickEvent()
    {
        button.onClick.AddListener(() =>
        {
            OnClick();
        });
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (IsAnim())
        {
            transform.DOComplete();
            transform.DOScale(0.9f, 0.1f);
        }
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        if (IsAnim())
        {
            transform.DOComplete();
            transform.DOScale(1, 0.1f);
        }
    }

    private bool IsAnim()
    {
        return button.interactable && IsUseAnim();
    }

    protected virtual bool IsUseAnim()
    {
        return true;
    }

    protected abstract void OnClick();
}

