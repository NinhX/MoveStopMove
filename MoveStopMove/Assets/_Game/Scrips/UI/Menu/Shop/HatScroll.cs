using UnityEngine;
using UnityEngine.Rendering;

public class HatScroll : ScrollView
{
    [SerializeField] private CharacterScripableObject characterScripable;

    private AbstractListSkinScipableObject<HatType> listHatScripable;
    private HatType hatType;

    public override void OnInit(Skin demo)
    {
        base.OnInit(demo);

        if (listHatScripable == null)
        {
            listHatScripable = characterScripable.HatScripable;
            AddContentScroll(listHatScripable.DictSkin, OnSetHat);
        }

        PlayerInventory.TryGetItemSelected(out HatType hatType);
        OnSetHat(hatType);
    }

    private void OnSetHat(HatType hatType)
    {
        AbstractSkinScripableObject<HatType> hatScripable = listHatScripable.GetScripable(hatType);
        if (PlayerInventory.CheckItemExis(hatType))
        {
            this.hatType = hatType;
            PlayerInventory.SetItemSelected(hatType);
            SetHat(hatScripable);
        }
        else
        {
            OpenPopup(hatScripable);
        }
    }

    private void SetHat(AbstractSkinScripableObject<HatType> hatScripable)
    {
        IGameObjectScripable skin = (IGameObjectScripable)hatScripable;
        GameObject hat = skin.CreateGameObject();
        demo.SetHat(hat);

        if (hat != null)
        {
            SelectContent(hatType);
        }
    }
}
