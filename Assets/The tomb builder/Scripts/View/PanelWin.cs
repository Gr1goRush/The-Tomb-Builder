using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelWin : MonoBehaviour
{
    private float _startFieldOfViewCamera = 3;
    private float _finishFieldOfViewCamera = 13;
    private float _stepFieldUp = 0.01f;
    private float _delayStepFieldUp = 0.001f;
    private float _delayActivePanelWin = 3;
    private Vector3 _maxScaleBackground = new Vector3(3, 3, 0);
    [Header("UI")]
    [SerializeField] private Image _background;
    [SerializeField] private Button _shop;
    [SerializeField] private Button _menu;
    [SerializeField] private TextMeshProUGUI _coinsOfGame;
    [SerializeField] private TextMeshProUGUI _coinsAllGame;

    public IEnumerator SetActive(bool value)
    {
        Camera.main.orthographic = false;
        Camera.main.fieldOfView = _startFieldOfViewCamera;
        while (Camera.main.fieldOfView < _finishFieldOfViewCamera)
        {
            Camera.main.fieldOfView += _stepFieldUp;
            var scaleBackground = _background.gameObject.transform.localScale;
            if (scaleBackground.x < _maxScaleBackground.x)
                _background.gameObject.transform.localScale += new Vector3(_stepFieldUp, _stepFieldUp, 0);

            yield return new WaitForSeconds(_delayStepFieldUp);
        }

        yield return new WaitForSeconds(_delayActivePanelWin);

        gameObject.SetActive(value);
    }

    private void OnShop()
        => ManagerScenes.Instance.LoadAsyncFromCoroutine("Menu", () =>
        {
            var menuController = Object.FindObjectOfType<MenuController>();
            menuController.ActivePanelShop();
        });

    private void OnMenu()
        => ManagerScenes.Instance.LoadAsyncFromCoroutine("Menu");

    private void Start()
    {
        AudioManager.Instance.PlayWinner();
        _shop.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayClickButton();
            OnShop();
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
