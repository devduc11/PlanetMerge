using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : Spawner<Ball>
{
    private static BallSpawner instance;
    public static BallSpawner Instance => instance;

    public List<Ball> Balls { get => balls; private set => balls = value; }

    [SerializeField]
    public List<Ball> balls = new List<Ball>();

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

    public override Ball Spawn(Vector3 spawnPos, Quaternion rotation, bool show = false)
    {
        Ball ball = base.Spawn(spawnPos, rotation, show);
        balls.Add(ball);
        return ball;
    }

    public override void Despawn(Ball obj)
    {
        balls.Remove(obj);
        base.Despawn(obj);
    }

}
