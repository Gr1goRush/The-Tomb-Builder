using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Tomb
{
    private List<Block> _blocks;
    private Vector2 _positionFirstBlock;
    private bool _isIncreaseScaleLastBlock;
    private bool _isTombBuildEnd;
    private float _minDistanceEqualBlock;
    private float _minScaleBlock;

    public Tomb(Vector2 positionFirstBlock, float minDistanceEqualBlock, float minScaleBlock)
    {
        _blocks = new List<Block>();
        _positionFirstBlock = positionFirstBlock;
        _isIncreaseScaleLastBlock = false;
        _isTombBuildEnd = false;
        _minDistanceEqualBlock = minDistanceEqualBlock;
        _minScaleBlock = minScaleBlock;
    }

    public int CountBlocks => _blocks.Count;
    public Block LastBlock => _blocks.Count != 0 ? _blocks[_blocks.Count - 1] : new BlockNull();
    public bool IsIncreaseScaleLastBlock => _isIncreaseScaleLastBlock;

    public void OnEndTombBuilded()
        => _isTombBuildEnd = true;

    public void BuildTomb(Block block)
    {
        if (_blocks.Count == 0)
            BuildFirstBlock(block);

        _blocks.Add(block);
    }

    public bool TryBlocksEqual()
    {
        if (LastBlock is Block && _blocks.Count >= 2)
        {
            var deltaScale = Mathf.Abs(LastBlock.Scale.Value.x - _blocks[_blocks.Count - 2].Scale.Value.x);
            if (deltaScale <= _minDistanceEqualBlock)
                return true;
        }

        return false;
    }

    public bool TryLastBlockScale()
    {
        if (LastBlock.Scale.Value.x >= _minScaleBlock)
            return true;

        return false;
    }

    public void DeleteLastBlock()
        => _blocks.Remove(LastBlock);

    public async void SetIsIncreaseScaleLastBlock(bool value)
    {
        if (value == false && ContainerSaveerPlayerPrefs.Instance.SaveerData.IsPurchasedMagnet == 1)
        {
            AudioManager.Instance.PlayMagnit();
            await Task.Run(() =>
            {
                while (LastBlock != null && Vector3.Distance(LastBlock.Scale.Value, new Vector3(1, 1, 0)) > 0.05)
                    continue;
            });
        }

        _isIncreaseScaleLastBlock = value;
    }

    public void IncreaseScaleLastBlock()
    {
        if(_isTombBuildEnd == false && _isIncreaseScaleLastBlock)
            LastBlock.IncreaseScale();
    }

    private void BuildFirstBlock(Block block)
    {
        block.UpdatePosition(_positionFirstBlock);
        block.Scale.Value = new Vector3(1, 1, 0);
    }
}
