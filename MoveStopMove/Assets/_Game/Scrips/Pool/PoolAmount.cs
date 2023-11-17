using UnityEngine;

[System.Serializable]
public struct PoolAmount
{
    [SerializeField] private PoolType type;
    [SerializeField] private AbstractGameUnit gameUnitPrefab;
    [SerializeField] private int minAmount;

    public PoolType Type => type;
    public AbstractGameUnit GameUnitPrefab => gameUnitPrefab;
    public int MinAmount => minAmount;
}
