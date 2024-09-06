using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private Button _pause;
    [SerializeField] private PanelPauseView _panelPauseView;

    public ReactiveCommand ActivePauseComand = new();

    public void SubscribeOnContinueCommand(Action<Unit> action)
        => _panelPauseView.ContinueCommand.Subscribe(action);

    private void Start()
    {
        _pause.onClick.AddListener(() =>
        {
            ActivePauseComand.Execute();
            _panelPauseView.SetActive(true);
        });
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(ActivePauseComand);
    }
}
