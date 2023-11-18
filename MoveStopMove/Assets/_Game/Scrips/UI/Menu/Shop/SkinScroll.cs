using UnityEngine;

public class SkinScroll : ScrollView
{
    [SerializeField] private CharacterScripableObject characterScripable;

    private AbstractListSkinScipableObject<SkinType> listSkinScripable;
    private SkinType skinType;

    public override void OnInit(Skin demo)
    {
        base.OnInit(demo);

        if (listSkinScripable == null)
        {
            listSkinScripable = characterScripable.SkinScipable;
            AddContentScroll(listSkinScripable.DictSkin, OnSetSkin);
        }

        PlayerInventory.TryGetItemSelected(out SkinType skinType);
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
