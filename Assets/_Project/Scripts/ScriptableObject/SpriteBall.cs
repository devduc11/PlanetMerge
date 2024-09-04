using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SpriteBall", menuName = "ScriptableObject/New SpriteBall", order = 1)]
public class SpriteBall : ScriptableObject
{
    public List<SpriteItemsBall> Sprites;
}

[System.Serializable]
public class SpriteItemsBall
{
    public List<Sprite> Sprites;
}
