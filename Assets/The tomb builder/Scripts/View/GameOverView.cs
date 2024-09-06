using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverView : MonoBehaviour
{
    [SerializeField] private Button _restart;
    [SerializeField] private Button _menu;
    [SerializeField] private TextMeshProUGUI _coinsOfGame;
    [SerializeField] private TextMeshProUGUI _coinsAllGame;

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    private void OnRestart()
        => ManagerScenes.Instance.LoadAsyncFromCoroutine("Game");

    private void OnMenu()
        => ManagerScenes.Instance.LoadAsyncFromCoroutine("Menu");

    private void Start()
    {
        AudioManager.Instance.PlayGameOver();
        _restart.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayClickButton();
            OnRestart();
        });
        _menu.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayClickButton();
            OnMenu();
        });
    }

    private void OnEnable()
    {
        _coinsOfGame.text = "Score: " + ContainerSaveerPlayerPrefs.Instance.SaveerData.Coins.ToString();
        _coinsAllGame.text = ContainerSaveerPlayerPrefs.Instance.SaveerData.Money.ToString();
        ContainerSaveerPlayerPrefs.Instance.SaveerData.IsPurchasedMagnet = 0;
    }
}
