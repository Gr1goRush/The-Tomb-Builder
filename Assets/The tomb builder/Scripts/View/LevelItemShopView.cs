using UnityEngine;
using TMPro;

public class LevelItemShopView : ItemShopView
{
    [SerializeField] private int _numberLevel;
    [SerializeField] private TextMeshProUGUI _titleCloseLevel;
    [SerializeField] private TextMeshProUGUI _viewLevel;
    [SerializeField] private GameObject _close;

    protected override void Start()
    {
        base.Start();
        _viewLevel.text = "Level " + _numberLevel.ToString();
        _titleCloseLevel.text = "You must win " + _numberLevel.ToString() + " level to buy it";
    }

    private void OnEnable()
    {
        if (ContainerSaveerPlayerPrefs.Instance.SaveerData.PurchasedLevels.Contains(_numberLevel.ToString()))
            SetActiveButtons(false, false, true);
        else if (ContainerSaveerPlayerPrefs.Instance.SaveerData.EndLevels.Contains((_numberLevel - 1).ToString()))
            SetActiveButtons(false, true, false);
        else
            SetActiveButtons(true, false, false);
    }

    private void SetActiveButtons(bool isClose, bool isPurchase, bool isPurchased)
    {
        _close.SetActive(isClose);
        _purchase.gameObject.SetActive(isPurchase);
        _purchased.SetActive(isPurchased);
    }

    protected override void Purchase()
    {
        ContainerSaveerPlayerPrefs.Instance.SaveerData.PurchasedLevels += _numberLevel.ToString() + ";";
        SetActiveButtons(false, false, true);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
