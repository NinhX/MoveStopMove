using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObject/Weapon")]
public class WeaponScripableObject : AbstractSkinScripableObject<WeaponType>, IGameObjectScripable
{
    [SerializeField] private GameObject weaponPrefab;

    public GameObject CreateGameObject()
    {
        return Instantiate(weaponPrefab);
    }
}