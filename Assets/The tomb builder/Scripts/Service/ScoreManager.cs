using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int _countMoneyOfOneGame;

    [SerializeField] private List<TextMeshProUGUI> _viewsScore;
    [SerializeField] private TextMeshProUGUI _viewMoney;
    [SerializeField] private int _increaseScore;

    public void AddScore()
    {
        AudioManager.Instance.PlayGetScore();
        ContainerSaveerPlayerPrefs.Instance.SaveerData.Coins += _increaseScore;
        UpdateViewScore();
    }

    public void AddMoney()
    {
        AudioManager.Instance.PlayGetMoney();
        _countMoneyOfOneGame += _increaseScore;
        ContainerSaveerPlayerPrefs.Instance.SaveerData.Money += _increaseScore;
        _viewMoney.text = _countMoneyOfOneGame.ToString();
    }

    private void UpdateViewScore()
    {
        foreach (var viewScore in _viewsScore)
            viewScore.text = "Score: " + ContainerSaveerPlayerPrefs.Instance.SaveerData.Coins.ToString();
    }

    private void Start()
    {
        ContainerSaveerPlayerPrefs.Instance.SaveerData.Coins = 0;
        _countMoneyOfOneGame = 0;
        UpdateViewScore();
    }
}
