using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetComponentInChildrenAttribute : PropertyAttribute
{
    public string name;

    public GetComponentInChildrenAttribute(string name)
    {
        this.name = name;
    }
}
