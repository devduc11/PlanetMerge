using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUIAnim : BaseUI
{
    [SerializeField, GetComponent()] private Animator anim;

    public override void SetActive(bool active)
    {
        if (active)
        {
            Visible();
        }
        else if (anim != null)
        {
            anim.SetBool("Hide", true);
        }
        else
        {
            Invisible();
        }
    }
}
