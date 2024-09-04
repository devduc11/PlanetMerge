using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetComponentInParentAttribute : PropertyAttribute
{
    public string name;

    public GetComponentInParentAttribute(string name)
    {
        this.name = name;
    }
}
