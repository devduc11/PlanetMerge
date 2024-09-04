using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetComponentsInParentAttribute : PropertyAttribute
{
    public Type type;
    public string name;

    public GetComponentsInParentAttribute(Type type, string name = "")
    {
        this.type = type;
        this.name = name;
    }
}
