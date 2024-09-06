using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataTomb", menuName = "ScriptableObject/DataTomb")]
public class DataTomb : ScriptableObject
{
    [SerializeField] private Vector2 _positionFirstBlock;
    [SerializeField] private float _minDistanceEqualBlock;
    [SerializeField] private float _minScaleBlock;

    public Vector2 PositionFirstBlock => _positionFirstBlock;
    public float MinDistanceEqualBlock => _minDistanceEqualBlock;
    public float MinScaleBlock => _minScaleBlock;
}
