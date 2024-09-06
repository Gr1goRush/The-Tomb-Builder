using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WalletManager
{
    public static bool TryPurchase(int price)
    {
        var isMach = ContainerSaveerPlayerPrefs.Instance.SaveerData.Money >= price;

        if (isMach)
            ContainerSaveerPlayerPrefs.Instance.SaveerData.Money -= price;

        return isMach;
    }
}
