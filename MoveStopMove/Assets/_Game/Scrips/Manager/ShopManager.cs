using System;

public class ShopManager : Singleton<ShopManager>
{
    public bool BuyItem<T>(IItemShop<T> itemShop) where T : Enum
    {
        bool result = false;
        if (!PlayerInventory.CheckItemExis(itemShop.GetSkinType()))
        {
            int coinPlayer = PlayerInventory.GetItem(ItemType.Coin).number;
            int priceItem = itemShop.GetPrice();

            if (coinPlayer >= priceItem)
            {
                coinPlayer -= priceItem;
                PlayerInventory.AddOrUpdateItem(ItemType.Coin, coinPlayer);
                PlayerInventory.AddItem(itemShop.GetSkinType());
                UIManager.Instance.ReloadUI<UIShop>();
                result = true;
            }
        }
        return result;
    }
}
