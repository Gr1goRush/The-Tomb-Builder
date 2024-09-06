public class VibrationSwitchView : SwitchView
{
    public override void Switch()
    {
        base.Switch();
        ContainerSaveerPlayerPrefs.Instance.SaveerData.IsVibrationOn = ContainerSaveerPlayerPrefs.Instance.SaveerData.IsVibrationOn == "1" ? "0" : "1";
        ChangeSprite(ContainerSaveerPlayerPrefs.Instance.SaveerData.IsVibrationOn);
    }

    protected override void Start()
    {
        base.Start();
        ChangeSprite(ContainerSaveerPlayerPrefs.Instance.SaveerData.IsVibrationOn);
    }
}
