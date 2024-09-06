using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class TimelineManager : MonoBehaviour
{
    private float _currentTime;
    private bool _isTimerPause;

    [Header("Длина таймера(в сек.)")]
    [SerializeField] private float _longTimer;
    [SerializeField] private TimelineView _timelineView;

    public ReactiveCommand OnTimerEndCommand = new();

    public void SetLongTimer(int value)
        => _longTimer = value;

    public void RestartTimer()
    {
        _currentTime = 0;
    }

    public void UpdateTimer()
    {
        if (_isTimerPause)
            return;

        _currentTime += Time.deltaTime;
        if (_currentTime >= _longTimer)
            OnTimerEndCommand.Execute();

        _timelineView.UpdateTimeline(_currentTime / _longTimer);
    }

    public void SetPauseTimer(bool value)
        => _isTimerPause = value;

    private void Start()
    {
        _currentTime = 0;
        _isTimerPause = false;
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(OnTimerEndCommand);
    }
}
