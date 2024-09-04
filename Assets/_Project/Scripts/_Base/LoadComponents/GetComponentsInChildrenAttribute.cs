using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetComponentsInChildrenAttribute : PropertyAttribute
{
    public Type type;
    public string name;

    public GetComponentsInChildrenAttribute(Type type, string name = "")
    {
        this.type = type;
        this.name = name;
    }
}
