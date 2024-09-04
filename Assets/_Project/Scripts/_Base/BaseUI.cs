using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIName
{
    HOME_UI, GAME_UI
}

public abstract class BaseUI : BaseMonoBehaviour
{
    protected abstract UIName GetName();

    public UIName GetUIName()
    {
        return GetName();
    }

    public virtual void SetActive(bool active)
    {
        if (active)
        {
            Visible();
        }
        else
        {
            Invisible();
        }
    }

    protected virtual void Visible()
    {
        gameObject.SetActive(true);
    }

    protected virtual void Invisible()
    {
        gameObject.SetActive(false);
    }
}
