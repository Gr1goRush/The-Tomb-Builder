public class SoundSwitchVIew : SwitchView
{
    public override void Switch()
    {
        base.Switch();
        ContainerSaveerPlayerPrefs.Instance.SaveerData.IsSoundOn = AudioManager.Instance.IsSoundOn() ? "0" : "1";
        ChangeSprite(ContainerSaveerPlayerPrefs.Instance.SaveerData.IsSoundOn);
        AudioManager.Instance.ChangeStateSound();
    }

    protected override void Start()
    {
        base.Start();
        ChangeSprite(ContainerSaveerPlayerPrefs.Instance.SaveerData.IsSoundOn);
    }
}
