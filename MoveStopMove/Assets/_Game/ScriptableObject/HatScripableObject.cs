using UnityEngine;

[CreateAssetMenu(fileName = "Hat", menuName = "ScriptableObject/Hat")]
public class HatScripableObject : AbstractSkinScripableObject<HatType>, IGameObjectScripable
{
    [SerializeField] private GameObject hatPrefab;

    public GameObject CreateGameObject()
    {
        if (hatPrefab != null)
        {
            return Instantiate(hatPrefab);
        }
        return new GameObject();
    }
}