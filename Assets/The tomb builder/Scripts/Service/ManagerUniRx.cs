using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public static class ManagerUniRx
{
    private static List<IDisposable> _objectsForDispose = new List<IDisposable>();

    public static void AddObjectDisposable(IDisposable disposable) =>
        _objectsForDispose.Add(disposable);

    public static void Dispose(IDisposable disposable)
    {
        _objectsForDispose.Remove(disposable);
        disposable.Dispose();
    }
}