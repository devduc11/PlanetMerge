using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindChildrenAttribute : PropertyAttribute
{
    public string name;

    public FindChildrenAttribute(string name)
    {
        this.name = name;
    }
}
