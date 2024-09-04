using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Float", menuName = "ScriptableObject/Float", order = 1)]
public class FloatScriptableObject : ScriptableObject
{
    [SerializeField]
    private float value;

    public float Value { get => value; set => this.value = value; }
}
