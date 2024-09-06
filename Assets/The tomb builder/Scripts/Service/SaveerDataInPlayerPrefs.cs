using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveerDataInPlayerPrefs : SaveerData
{
    private readonly string KEY_NUMBER_LEVEL = "Level";
    private readonly string KEY_COUNT_COIN = "Coins";
    private readonly string KEY_COUNT_MONEY = "Money";
    private readonly string KEY_PURCHASED_LEVELS = "PurchasedLevels";
    private readonly string KEY_IS_MUSIC_ON = "IsMusicOn";
    private readonly string KEY_IS_SOUND_ON = "IsSoundOn";
    private readonly string KEY_IS_VIBRATION_ON = "IsVibrationOn";
    private readonly string KEY_ADDITIONALLY_COUNT_HELTH = "AdditionallyHealth";
    private readonly string KEY_END_LEVELS = "EndLevels";
    private readonly string KEY_MAGNET_PURCHASED = "IsPurchasedMagnet";

    public int Level { get { return Load<int>(KEY_NUMBER_LEVEL, 1); } set { Save<int>(KEY_NUMBER_LEVEL, value); } }
    public int Coins { get { return Load<int>(KEY_COUNT_COIN, 0); } set { Save<int>(KEY_COUNT_COIN, value); } }
    public int Money { get { return Load<int>(KEY_COUNT_MONEY, 0); } set { Save<int>(KEY_COUNT_MONEY, value); } }
    public string PurchasedLevels { get { return Load<string>(KEY_PURCHASED_LEVELS, "1"); } set { Save<string>(KEY_PURCHASED_LEVELS, value); } }
    public string IsMusicOn { get { return Load<string>(KEY_IS_MUSIC_ON, "1"); } set { Save<string>(KEY_IS_MUSIC_ON, value); } }
    public string IsSoundOn { get { return Load<string>(KEY_IS_SOUND_ON, "1"); } set { Save<string>(KEY_IS_SOUND_ON, value); } }
    public string IsVibrationOn { get { return Load<string>(KEY_IS_VIBRATION_ON, "1"); } set { Save<string>(KEY_IS_VIBRATION_ON, value); } }
    public int AdditionallyHealth { get { return Load<int>(KEY_ADDITIONALLY_COUNT_HELTH, 0); } set { Save<int>(KEY_ADDITIONALLY_COUNT_HELTH, value); } }
    public string EndLevels { get { return Load<string>(KEY_END_LEVELS, "0"); } set { Save<string>(KEY_END_LEVELS, value); } }
    public int IsPurchasedMagnet { get { return Load<int>(KEY_MAGNET_PURCHASED, 0); } set { Save<int>(KEY_MAGNET_PURCHASED, value); } }

    public override T Load<T>(string nameParameter, T defaultValue)
    {
        if (PlayerPrefs.HasKey(nameParameter) == false)
            return defaultValue;

        Type inType = typeof(T);

        if (inType == typeof(int))
            return (T)(object)PlayerPrefs.GetInt(nameParameter);
        else if (inType == typeof(float))
            return (T)(object)PlayerPrefs.GetFloat(nameParameter);
        else
            return (T)(object)PlayerPrefs.GetString(nameParameter);
    }

    public override void Save<T>(string nameParameter, T value)
    {
        Type inType = typeof(T);

        if (inType == typeof(int))
            PlayerPrefs.SetInt(nameParameter, int.Parse(value.ToString()));
        else if (inType == typeof(float))
            PlayerPrefs.SetFloat(nameParameter, float.Parse(value.ToString()));
        else if (inType == typeof(string))
            PlayerPrefs.SetString(nameParameter, value.ToString());
    }
}
