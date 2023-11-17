using UnityEngine;

[CreateAssetMenu(fileName = "Pant", menuName = "ScriptableObject/Pant")]
public class PantScripableObject : AbstractSkinScripableObject<PantType>, IMaterialScripable
{
    [SerializeField] private Material pantMaterial;

    public Material GetMaterial()
    {
        return pantMaterial;
    }
}