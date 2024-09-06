public class HealthItemShopView : ItemShopView
{
    protected override void Start()
    {
        base.Start();
    }

    private void OnEnable()
    {
        if (ContainerSaveerPlayerPrefs.Instance.SaveerData.AdditionallyHealth == 1)
            SetActiveButtons(false, true);
        else
            SetActiveButtons(true, false);
    }

    private void SetActiveButtons(bool isPurchase, bool isPurchased)
    {
        _purchase.gameObject.SetActive(isPurchase);
        _purchased.SetActive(isPurchased);
    }

    protected override void Purchase()
    {
        ContainerSaveerPlayerPrefs.Instance.SaveerData.AdditionallyHealth = 1;
        SetActiveButtons(false, true);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
