using UnityEngine;
using UnityEngine.UI;

public class PopupSetting : AbstractPopup
{
    [SerializeField] private Slider sliderVolume;
    [SerializeField] private Button buttonClose;
    [SerializeField] private InputField fakeCoin;

    protected override void OnBeginOpen()
    {
        sliderVolume.value = PlayerPrefs.GetFloat(Constant.VOLUME_PLAYER_PREF, 1);
        fakeCoin.text = PlayerInventory.GetItem(ItemType.Coin).number.ToString();
    }

    protected override void OnOpenPopup()
    {
        buttonClose.onClick.AddListener(Close);
        sliderVolume.onValueChanged.AddListener(ChangeVolume);
        fakeCoin.onValueChanged.AddListener(FakeCoin);
    }

    protected override void OnClosePopup()
    {
        buttonClose.onClick.RemoveAllListeners();
        sliderVolume.onValueChanged.RemoveAllListeners();
        fakeCoin.onValueChanged.RemoveAllListeners();
    }

    private void ChangeVolume(float vol)
    {
        AudioManager.Instance.SaveVolume(vol);
    }

    private void FakeCoin(string coin)
    {
        if (int.TryParse(coin, out int coinInt))
        {
            PlayerInventory.UpdateItem(ItemType.Coin, coinInt);
            UIManager.Instance.ReloadUI<UIShop>();
        }
    }
}
