using UnityEngine;

public class PantScroll : ScrollView
{
    [SerializeField] private CharacterScripableObject characterScripable;

    private AbstractListSkinScipableObject<PantType> listPantScripable;
    private PantType pantType;

    private void Awake()
    {
        listPantScripable = characterScripable.PantScipable;
        AddContentScroll(listPantScripable.DictSkin, OnSetPant);
    }

    public override void OnInit(Skin demo)
    {
        base.OnInit(demo);
        pantType = PlayerInventory.GetItemSelected<PantType>();
        OnSetPant(pantType);
    }

    private void OnSetPant(PantType pantType)
    {
        AbstractSkinScripableObject<PantType> pantScripable = listPantScripable.GetScripable(pantType);
        if (PlayerInventory.CheckItemExis(pantType))
        {
            this.pantType = pantType;
            PlayerInventory.SetItemSelected(pantType);
            SetPant(pantScripable);
        }
        else
        {
            OpenPopup(pantScripable);
        }
    }

    private void SetPant(AbstractSkinScripableObject<PantType> pantScripable)
    {
        IMaterialScripable pant = (IMaterialScripable)pantScripable;
        Material pantMaterial = pant.GetMaterial();
        demo.SetPant(pantMaterial);

        if (pantMaterial != null)
        {
            SelectContent(pantType);
        }
    }
}
