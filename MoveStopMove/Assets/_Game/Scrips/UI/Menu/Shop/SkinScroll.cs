using UnityEngine;

public class SkinScroll : ScrollView
{
    [SerializeField] private CharacterScripableObject characterScripable;

    private AbstractListSkinScipableObject<SkinType> listSkinScripable;
    private SkinType skinType;

    private void Awake()
    {
        listSkinScripable = characterScripable.SkinScipable;
        AddContentScroll(listSkinScripable.DictSkin, OnSetSkin);
    }

    public override void OnInit(Skin demo)
    {
        base.OnInit(demo);
        skinType = PlayerInventory.GetItemSelected<SkinType>();
        OnSetSkin(skinType);
    }

    private void OnSetSkin(SkinType skinType)
    {
        AbstractSkinScripableObject<SkinType> skinScripable = listSkinScripable.GetScripable(skinType);
        if (PlayerInventory.CheckItemExis(skinType))
        {
            this.skinType = skinType;
            PlayerInventory.SetItemSelected(skinType);
            SetSkin(skinScripable);
        }
        else
        {
            OpenPopup(skinScripable);
        }
    }

    private void SetSkin(AbstractSkinScripableObject<SkinType> pantScripable)
    {
        IMaterialScripable skin = (IMaterialScripable)pantScripable;
        Material skinMaterial = skin.GetMaterial();
        demo.SetSkin(skinMaterial);

        if (skinMaterial != null)
        {
            SelectContent(skinType);
        }
    }
}
