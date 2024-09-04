using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bool", menuName = "ScriptableObject/Bool", order = 1)]
public class BooleanScripableObject : ScriptableObject
{
    [SerializeField]
    private bool value;

    public bool Value { get => value; set => this.value = value; }
}
