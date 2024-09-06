using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.EventSystems;

public class SystemInput : MonoBehaviour
{
    private bool _isPauseGame;

    public ReactiveCommand OnMouseDownCommand = new ();
    public ReactiveCommand OnMouseUpCommand = new ();

    public void OnSetActivePause(bool value)
        => _isPauseGame = value;

    private void Start()
    {
        _isPauseGame = false;
    }

    private void OnMouseDown()
    {
        if (_isPauseGame == false)
            OnMouseDownCommand.Execute();
    }

    private void OnMouseUp()
    {
        if (_isPauseGame == false)
            OnMouseUpCommand.Execute();
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(OnMouseDownCommand);
        ManagerUniRx.Dispose(OnMouseUpCommand);
    }
}
