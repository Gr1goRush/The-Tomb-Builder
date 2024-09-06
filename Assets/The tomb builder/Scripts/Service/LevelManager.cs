using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class LevelManager : MonoBehaviour
{
    [SerializeField] List<Level> _levels;
    [SerializeField] private Image _background;

    public ReactiveCommand<Level> LevelLoadingCommand = new();

    public void LoadingLevel()
    {
        var level = _levels.Find(level => level.NumberLevel == ContainerSaveerPlayerPrefs.Instance.SaveerData.Level);
        if (level != null)
        {
            _background.sprite = level.Background;
            LevelLoadingCommand.Execute(level);
        }
        else
            new Exception("Не найден уровень!");
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(LevelLoadingCommand);
    }
}
