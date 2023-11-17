using System.Collections.Generic;
using UnityEngine;

public class Skin : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private List<Renderer> skins;

    [SerializeField] private Renderer skin;
    [SerializeField] private Renderer pant;
    [SerializeField] private Transform hairLocale;
    [SerializeField] private Transform weaponLocale;

    public Animator Anim => anim;
    private GameObject hat;
    private GameObject weapon;
    private List<GameObject> items = new();

    public void DisableSkin()
    {
        foreach (var skin in skins)
        {
            skin.enabled = false;
        }
        foreach (var item in items)
        {
            item.SetActive(false);
        }
    }

    public void EnableSkin()
    {
        foreach (var skin in skins)
        {
            skin.enabled = true;
        }
        foreach (var item in items)
        {
            item.SetActive(true);
        }
    }

    public void SetPant(Material pantMaterial)
    {
        pant.material = pantMaterial;
    }

    public void SetSkin(Material skinMaterial)
    {
        skin.material = skinMaterial;
    }

    public void SetHat(GameObject newHat)
    {
        Destroy(hat);
        items.Remove(hat);
        if (newHat != null)
        {
            hat = newHat;
            hat.transform.SetParent(hairLocale);
            hat.transform.localPosition = Vector3.zero;
            hat.transform.localScale = Vector3.one;
            hat.transform.localRotation = Quaternion.identity;
            items.Add(hat);
        }
    }

    public void SetWeapon(GameObject newWeapon)
    {
        Destroy(weapon);
        items.Remove(weapon);
        if (newWeapon != null)
        {
            weapon = newWeapon;
            weapon.transform.SetParent(weaponLocale);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;
            items.Add(weapon);
        }
    }
}
