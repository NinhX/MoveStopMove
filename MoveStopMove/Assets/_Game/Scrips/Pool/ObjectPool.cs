using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    [SerializeField] private PoolAmount[] poolAmounts;

    private static Dictionary<PoolType, Pool> dictPool = new();

    void Awake()
    {
        PreLoad();
    }

    public T Spawn<T>(PoolType tag) where T : AbstractGameUnit
    {
        return Spawn<T>(tag, Vector3.zero, Quaternion.identity);
    }

    public T Spawn<T>(PoolType tag, Quaternion quaternion) where T : AbstractGameUnit
    {
        return Spawn<T>(tag, Vector3.zero, quaternion);
    }

    public T Spawn<T>(PoolType tag, Vector3 positon) where T : AbstractGameUnit
    {
        return Spawn<T>(tag, positon, Quaternion.identity);
    }

    public T Spawn<T>(PoolType tag, Vector3 positon, Quaternion quaternion) where T : AbstractGameUnit
    {
        return dictPool[tag].Spawn(positon, quaternion) as T;
    }

    public void DesSpawn(AbstractGameUnit gameUnit)
    {
        if (dictPool.ContainsKey(gameUnit.PoolType))
        {
            dictPool[gameUnit.PoolType].DesSpawn(gameUnit);
        }
    }

    public void Collect(PoolType poolType)
    {
        dictPool[poolType].Collect();
    }

    public void CollectAll()
    {
        for (int i = 0; i < dictPool.Count; i++)
        {
            dictPool.ElementAt(i).Value.Collect();
        }
    }

    private void PreLoad()
    {
        dictPool.Clear();
        for (int i = 0; i < poolAmounts.Length; i++)
        {
            PoolAmount poolAmount = poolAmounts[i];

#if UNITY_EDITOR
            if (dictPool.ContainsKey(poolAmount.Type))
            {
                Debug.Log("PoolType duplicate.");
                return;
            }
#endif
            Pool pool = new(poolAmount);
            dictPool.Add(poolAmount.Type, pool);
        }
    }
}
