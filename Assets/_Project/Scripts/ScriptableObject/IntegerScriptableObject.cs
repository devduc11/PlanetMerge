using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Integer", menuName = "ScriptableObject/Integer", order = 1)]
public class IntegerScriptableObject : ScriptableObject
{
    [SerializeField]
    private int value;

    public int Value { get => value; set => this.value = value; }
}
