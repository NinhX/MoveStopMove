using System;
using UnityEngine;

public abstract class AbstractGameUnit : MonoBehaviour
{
    [SerializeField] private PoolType poolType;

    private static int indexer = 0;

    public readonly int ID = indexer++;
    public Transform Tf => tf ??= transform;
    public PoolType PoolType => poolType;

    private Transform tf;
    private ObjectPool pool => ObjectPool.Instance;

    public void OnDesPawn(float timeDelay)
    {
        CancelInvoke(nameof(OnDesPawn));
        Invoke(nameof(OnDesPawn), timeDelay);
    }

    public void OnDesPawn()
    {
        pool.DesSpawn(this);
    }
}
