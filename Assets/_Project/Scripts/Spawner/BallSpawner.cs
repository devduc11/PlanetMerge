using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : Spawner<Ball>
{
    private static BallSpawner instance;
    public static BallSpawner Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if (instance == null)
        {
            instance = this;
        }
    }

    protected override string GetPrefabPath()
    {
        return "Assets/_Project/Prefabs/Ball.prefab";
    }
}
