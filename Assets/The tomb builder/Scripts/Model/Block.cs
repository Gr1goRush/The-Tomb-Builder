using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Block : IDisposable
{
    private Vector3 _stepIncreaseScale;
    private float _speedIncreaseScale;

    public Block()
    {
        ManagerUniRx.AddObjectDisposable(Position);
    }

    public Block(Vector2 position)
    {
        ManagerUniRx.AddObjectDisposable(Position);
        Position.Value = position;
    }

    public Transform Center { get; set; }
    public ReactiveProperty<Vector2> Position { get; private set; } = new();
    public ReactiveProperty<Vector3> Scale { get; private set; } = new();

    public void SetStepIncreaseScale(Vector3 stepIncreaseScale)
        =>_stepIncreaseScale = stepIncreaseScale;

    public void SetSpeedIncreaseScale(float speedIncreaseScale)
        => _speedIncreaseScale = speedIncreaseScale;

    public void UpdatePosition(Vector2 position)
        => Position.Value = position;

    public void IncreaseScale()
    {
        Scale.Value += _stepIncreaseScale;
    }

    //public bool TryScale()
    //{
    //    if (Scale.Value.x > 0.2f && Scale.Value.y > 0.2f)
    //        return true;

    //    return false;
    //}

    public void Dispose()
    {
        ManagerUniRx.Dispose(Position);
        ManagerUniRx.Dispose(Scale);
    }
}
