using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] private BlockView _prefabBlock;
    [SerializeField] private BlockView _prefabBlockTop;
    [SerializeField] private Transform _parent;

    public ReactiveCommand<BlockView> OnSpawnBlockComand = new();

    public void SpawnBlock(Block parent, bool isBuildBlockTop)
    {
        var block = Instantiate(isBuildBlockTop ? _prefabBlockTop : _prefabBlock, parent is BlockNull? _parent : parent.Center);
        OnSpawnBlockComand.Execute(block);
    }

    public void SetSpriteBlock(Sprite sprite)
        => _prefabBlock.SetSpriteBlock(sprite);

    public void SetSpriteBlockTop(Sprite sprite)
        => _prefabBlockTop.SetSpriteBlock(sprite);

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(OnSpawnBlockComand);
    }
}
