using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAssetAtPathAttribute : PropertyAttribute
{
    public Type type;
    public string path;

    public LoadAssetAtPathAttribute(Type type, string path = "")
    {
        this.type = type;
        this.path = path;
    }
}
