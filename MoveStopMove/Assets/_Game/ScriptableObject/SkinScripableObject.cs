using UnityEngine;

[CreateAssetMenu(fileName = "Skin", menuName = "ScriptableObject/Skin")]
public class SkinScripableObject : AbstractSkinScripableObject<SkinType>, IMaterialScripable
{
    [SerializeField] private Material skinMaterial;

    public Material GetMaterial()
    {
        return skinMaterial;
    }
}