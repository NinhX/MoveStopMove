using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pool
{
    private AbstractGameUnit gameUnitPrefab;
    private Dictionary<int, AbstractGameUnit> m_active = new();
    private Queue<AbstractGameUnit> m_inactive = new();

    public Pool(PoolAmount poolAmount)
    {
        gameUnitPrefab = poolAmount.GameUnitPrefab;
        AddPool(poolAmount.MinAmount);
    }

    public AbstractGameUnit Spawn()
    {
        return Spawn(Vector3.zero, Quaternion.identity);
    }

    public AbstractGameUnit Spawn(Vector3 positon, Quaternion quaternion)
    {
        AbstractGameUnit gameUnit;

        if (m_inactive.Count == 0)
        {
            gameUnit = GameObject.Instantiate(gameUnitPrefab, positon, quaternion);
        }
        else
        {
            gameUnit = m_inactive.Dequeue();
            gameUnit.Tf.SetPositionAndRotation(positon, quaternion);
        }
        gameUnit.gameObject.SetActive(true);
        m_active.Add(gameUnit.ID, gameUnit);
        return gameUnit;
    }

    public void DesSpawn(AbstractGameUnit unit)
    {
        unit.gameObject.SetActive(false);
        if (m_active.Remove(unit.ID))
        {
            m_inactive.Enqueue(unit);
        }
    }

    public void Collect()
    {
        int[] keys = m_active.Keys.ToArray();
        for (int i = 0; i < keys.Length; i++)
        {
            DesSpawn(m_active[keys[i]]);
        }
    }

    private void AddPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            AbstractGameUnit gameUnit = GameObject.Instantiate(gameUnitPrefab);
            gameUnit.gameObject.SetActive(false);
            m_inactive.Enqueue(gameUnit);
        }
    }
}
