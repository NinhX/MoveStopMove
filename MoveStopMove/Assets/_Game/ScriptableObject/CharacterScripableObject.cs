using UnityEngine;
[CreateAssetMenu(fileName = "CharaterScriptableObject", menuName = "ScriptableObject/CharaterScriptableObject", order = 1)]
public class CharacterScripableObject : ScriptableObject
{
    [SerializeField] private Skin modelPrefab;
    [SerializeField] private AbstractListSkinScipableObject<SkinType> skinScipable;
    [SerializeField] private AbstractListSkinScipableObject<PantType> pantScipable;
    [SerializeField] private AbstractListSkinScipableObject<HatType> hatScipable;
    [SerializeField] private AbstractListSkinScipableObject<WeaponType> weaponScipable;
    [SerializeField] private float speed;

    public float Speed => speed;
    public AbstractListSkinScipableObject<SkinType> SkinScipable => skinScipable;
    public AbstractListSkinScipableObject<PantType> PantScipable => pantScipable;
    public AbstractListSkinScipableObject<HatType> HatScripable => hatScipable;
    public AbstractListSkinScipableObject<WeaponType> WeaponScripable => weaponScipable;

    public Skin ModelPrefab
    {
        get => modelPrefab;
        set => modelPrefab = value;
    }
}
