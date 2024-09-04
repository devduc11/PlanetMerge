using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Spawner<T> : BaseMonoBehaviour where T : Component
{
    [SerializeField] protected T prefab;
    [SerializeField] private int spawnedCount = 0;
    [SerializeField] protected List<T> poolObjs = new();

    protected int SpawnedCount { get => spawnedCount; private set => spawnedCount = value; }

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (prefab == null)
        {
            prefab = LoadAssetAtPath<T>(GetPrefabPath());
        }
    }
    #endregion

    public virtual T Spawn(Vector3 spawnPos, bool show = false)
    {
        return Spawn(spawnPos, Quaternion.identity, show);
    }

    public virtual T Spawn(Vector3 spawnPos, Quaternion rotation, bool show = false)
    {
        T newPrefab = GetObjectFromPool(show);
        newPrefab.transform.SetPositionAndRotation(spawnPos, rotation);
        spawnedCount++;

        return newPrefab.GetComponent<T>();
    }

    protected virtual T GetObjectFromPool(bool show)
    {
        if (poolObjs.Count > 0)
        {
            if (!poolObjs[0].gameObject.activeInHierarchy)
            {
                T t = poolObjs[0];
                t.gameObject.SetActive(show);
                poolObjs.Remove(poolObjs[0]);
                return t;
            }
        }

        T newPrefab = Instantiate(prefab, transform);
        newPrefab.gameObject.SetActive(show);
        newPrefab.name = prefab.name;
        return newPrefab;
    }

    public virtual void Despawn(T obj)
    {
        if (poolObjs.Contains(obj)) return;

        poolObjs.Add(obj);
        obj.gameObject.SetActive(false);
        spawnedCount--;
    }

    public virtual void DespawnAll()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.gameObject.activeInHierarchy)
            {
                Despawn(child.GetComponent<T>());
            }
        }
    }

    protected abstract string GetPrefabPath();
}
