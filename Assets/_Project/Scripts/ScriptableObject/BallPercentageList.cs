using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BallPercentageList", menuName = "ScriptableObject/New BallPercentageList", order = 1)]
public class BallPercentageList : ScriptableObject
{
    public List<Percentage> Percentages;
}

[System.Serializable]
public class Percentage
{
    public int NumberOfFruits;
    public List<float> Percentages;
}
