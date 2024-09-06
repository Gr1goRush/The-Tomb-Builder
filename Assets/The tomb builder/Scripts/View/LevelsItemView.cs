using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsItemView : MonoBehaviour
{
    [SerializeField] private int _numberLevel;
    [SerializeField] private Button _play;
    [SerializeField] private Image _closeLevel;

    private void Start()
    {
        _play.onClick.AddListener(() => { OnPlayLevel(); });
    }

    private void OnEnable()
    {
        if (_numberLevel == 1)
            return;

        if (ContainerSaveerPlayerPrefs.Instance.SaveerData.PurchasedLevels.Contains(_numberLevel.ToString()))
        {
            _play.gameObject.SetActive(true);
            _closeLevel.gameObject.SetActive(false);
        }
        else
        {
            _play.gameObject.SetActive(false);
            _closeLevel.gameObject.SetActive(true);
        }
    }

    private void OnPlayLevel()
    {
        ContainerSaveerPlayerPrefs.Instance.SaveerData.Level = _numberLevel;
        ManagerScenes.Instance.LoadAsyncFromCoroutine("Game");
    }
}
