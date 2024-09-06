using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;

public class ShopView : MonoBehaviour
{
    [SerializeField] private Button _close;
    [SerializeField] private TextMeshProUGUI _coins;
    [SerializeField] private List<ItemShopView> _items;

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    public void UpdateViewCoins()
        => _coins.text = ContainerSaveerPlayerPrefs.Instance.SaveerData.Money.ToString();

    private void Start()
    {
        _close.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayClickButton();
            SetActive(false);
        });
        UpdateViewCoins();

        foreach (var item in _items)
            item.OnPurchasedCommand.Subscribe(_ => { UpdateViewCoins(); });
    }
}
