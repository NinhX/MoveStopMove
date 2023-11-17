using UnityEngine;

public class WeaponScroll : ScrollView
{
    [SerializeField] private CharacterScripableObject characterScripable;

    private AbstractListSkinScipableObject<WeaponType> listWeaponScripable;
    private WeaponType weaponType;

    private void Awake()
    {
        listWeaponScripable = characterScripable.WeaponScripable;
        AddContentScroll(listWeaponScripable.DictSkin, OnSetWeapon);
    }

    public override void OnInit(Skin demo)
    {
        base.OnInit(demo);
        weaponType = PlayerInventory.GetItemSelected<WeaponType>();
        OnSetWeapon(weaponType);
    }

    public void NextWeapon()
    {
        OnSetWeapon(listWeaponScripable.Next(weaponType));
    }

    public void PrevWeapon()
    {
        OnSetWeapon(listWeaponScripable.Prev(weaponType));
    }

    private void OnSetWeapon(WeaponType weaponType)
    {
        AbstractSkinScripableObject<WeaponType> weaponScripable = listWeaponScripable.GetScripable(weaponType);
        if (PlayerInventory.CheckItemExis(weaponType))
        {
            this.weaponType = weaponType;
            PlayerInventory.SetItemSelected(weaponType);
            SetWeapon(weaponScripable);
        }
        else
        {
            OpenPopup(weaponScripable);
        }
    }

    private void SetWeapon(AbstractSkinScripableObject<WeaponType> weaponScripable)
    {
        IGameObjectScripable skin = (IGameObjectScripable)weaponScripable;
        GameObject weapon = skin.CreateGameObject();
        demo.SetWeapon(weapon);

        if (weapon != null)
        {
            SelectContent(weaponType);
            SetTextName(weaponScripable.SkinName);
        }
    }
}
