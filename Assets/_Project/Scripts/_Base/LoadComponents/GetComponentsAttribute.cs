using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetComponentsAttribute : PropertyAttribute
{
    public Type type;

    public GetComponentsAttribute(Type type)
    {
        this.type = type;
    }
}