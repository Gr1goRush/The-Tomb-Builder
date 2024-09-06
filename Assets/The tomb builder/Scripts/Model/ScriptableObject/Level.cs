using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObject/Level")]
public class Level : ScriptableObject
{
    [Header("Номер уровня")]
    [SerializeField] private int _numberLevel;
    [Header("Длительность уровня(кол-во блоков)")]
    [SerializeField] private int _countBlocksGame;
    [Header("Время на поставноку одного блока(в мин.)")]
    [SerializeField] private int _timeBuildBlock;
    [SerializeField] private Sprite _background;
    [SerializeField] private Sprite _typeBlock;
    [SerializeField] private Sprite _typeBlockTop;

    public int NumberLevel => _numberLevel;
    public int CountBlocksGame => _countBlocksGame;
    public int TimeBuildBlock => _timeBuildBlock;
    public Sprite Background => _background;
    public Sprite TypeBlock => _typeBlock;
    public Sprite TypeBlockTop => _typeBlockTop;
}
