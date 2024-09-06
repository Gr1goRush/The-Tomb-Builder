public abstract class SaveerData
{
    public abstract void Save<T>(string nameParameter, T value);
    public abstract T Load<T>(string nameParameter, T defaultValue);
}
