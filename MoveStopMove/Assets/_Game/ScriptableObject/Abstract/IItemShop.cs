using System;

public interface IItemShop<T>
{
    public T GetSkinType();
    public int GetPrice();
}
