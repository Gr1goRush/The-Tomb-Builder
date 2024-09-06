public class ContainerSaveerPlayerPrefs
{
    private static ContainerSaveerPlayerPrefs _instance;
    private SaveerDataInPlayerPrefs _saveerData;

    private ContainerSaveerPlayerPrefs()
    {
        _saveerData = new SaveerDataInPlayerPrefs();
    }

    public static ContainerSaveerPlayerPrefs Instance
    {
        get
        {
            if (_instance == null)
                _instance = new ContainerSaveerPlayerPrefs();

            return _instance;
        }
    }
    public SaveerDataInPlayerPrefs SaveerData => _saveerData;
}