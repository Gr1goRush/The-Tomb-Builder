using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;

public class ItemShopView : MonoBehaviour
{
    [SerializeField] private int _price;
    [SerializeField] private TextMeshProUGUI _viewPrice;

    [SerializeField] protected Button _purchase;
    [SerializeField] protected GameObject _purchased;

    public ReactiveCommand OnPurchasedCommand = new();

    protected virtual void Start()
    {
        ManagerUniRx.AddObjectDisposable(OnPurchasedCommand);
        _purchase.onClick.AddListener(() => { OnPurchase(); });
        _viewPrice.text = "Buy " + _price.ToString();
    }

    protected virtual void OnPurchase()
    {
        if (WalletManager.TryPurchase(_price))
        {
            Purchase();
            OnPurchasedCommand.Execute();
        }
        else
            AudioManager.Instance.PlayNotEnoughMoney();
    }

    protected virtual void Purchase() { }

    protected virtual void OnDestroy()
    {
        ManagerUniRx.Dispose(OnPurchasedCommand);
    }
}
