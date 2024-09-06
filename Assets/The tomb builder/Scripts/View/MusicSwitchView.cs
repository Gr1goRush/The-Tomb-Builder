public class MusicSwitchView : SwitchView
{
    public override void Switch()
    {
        base.Switch();
        ContainerSaveerPlayerPrefs.Instance.SaveerData.IsMusicOn = AudioManager.Instance.IsAudioOn() ? "0" : "1";
        ChangeSprite(ContainerSaveerPlayerPrefs.Instance.SaveerData.IsMusicOn);
        AudioManager.Instance.ChangeStateMusic();
    }

    protected override void Start()
    {
        base.Start();
        ChangeSprite(ContainerSaveerPlayerPrefs.Instance.SaveerData.IsMusicOn);
    }
}
