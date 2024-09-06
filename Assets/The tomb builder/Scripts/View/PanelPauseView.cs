using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class PanelPauseView : MonoBehaviour
{
    [SerializeField] private Button _continue;
    [SerializeField] private Button _restart;
    [SerializeField] private Button _menu;

    public ReactiveCommand ContinueCommand = new ();

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    private void OnContinue()
        => SetActive(false);

    private void OnRestart()
        => ManagerScenes.Instance.LoadAsyncFromCoroutine("Game");

    private void OnMenu()
        => ManagerScenes.Instance.LoadAsyncFromCoroutine("Menu");

    private void Start()
    {
        ManagerUniRx.AddObjectDisposable(ContinueCommand);

        _continue.onClick.AddListener(() =>
        {
            ContinueCommand.Execute();
            AudioManager.Instance.PlayClickButton();
            OnContinue();
        });
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

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(ContinueCommand);
    }
}
