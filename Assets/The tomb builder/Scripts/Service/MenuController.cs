using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button _openSettings;
    [SerializeField] private Button _openInformation;
    [SerializeField] private Button _openShop;
    [SerializeField] private Button _levels;
    [SerializeField] private Button _startGame;

    [SerializeField] private SettingsView _settingsView;
    [SerializeField] private ShopView _shopView;
    [SerializeField] private LevelsView _levelsView;
    [SerializeField] private InformationView _informationView;

    public void ActivePanelShop()
        => _shopView.SetActive(true);

    private void Start()
    {
        //ContainerSaveerPlayerPrefs.Instance.SaveerData.Coins = 1000;
        _openSettings.onClick.AddListener(() =>
        {
            _settingsView.SetActive(true);
            AudioManager.Instance.PlayClickButton();
        });

        _openInformation.onClick.AddListener(() =>
        {
            _informationView.SetActive(true);
            AudioManager.Instance.PlayClickButton();
        });

        _openShop.onClick.AddListener(() =>
        {
            _shopView.SetActive(true);
            AudioManager.Instance.PlayClickButton();
        });

        _levels.onClick.AddListener(() =>
        {
            _levelsView.SetActive(true);
            AudioManager.Instance.PlayClickButton();
        });

        _startGame.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayClickButton();
            ManagerScenes.Instance.LoadAsyncFromCoroutine("Game");
        });
    }
}
