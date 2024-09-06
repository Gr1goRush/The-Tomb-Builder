using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class HealthManager : MonoBehaviour
{
    private int _currentHealth;
    private List<HealthIconView> _currentHealthIcon;

    [SerializeField] private int _countHealth;
    [SerializeField] private Sprite _icon;
    [SerializeField] private HealthIconView _prefabHhealthIconView;
    [SerializeField] private GameObject _healthLine;

    public ReactiveCommand GameOverCommand = new();

    public void Damage()
    {
        if (_currentHealth != 0)
        { 
            _currentHealth -= 1;
            _currentHealthIcon[0].DestroyHealth();
            _currentHealthIcon.Remove(_currentHealthIcon[0]);
        }
        else
            GameOverCommand.Execute();
    }

    private void Start()
    {
        _currentHealthIcon = new List<HealthIconView>();
        _currentHealth = _countHealth + ContainerSaveerPlayerPrefs.Instance.SaveerData.AdditionallyHealth;
        ContainerSaveerPlayerPrefs.Instance.SaveerData.AdditionallyHealth = 0;

        for (int i = 0; i < _currentHealth; i++)
        {
            var healthIcon = Instantiate(_prefabHhealthIconView, _healthLine.transform);
            healthIcon.SetIcon(_icon);
            _currentHealthIcon.Add(healthIcon);
        }
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(GameOverCommand);
    }
}
