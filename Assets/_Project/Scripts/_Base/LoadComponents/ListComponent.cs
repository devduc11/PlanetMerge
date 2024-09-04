using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ListComponent<T> where T : Object
{
    public List<T> Components;
}
