using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class TombController
{
    private Tomb _tomb;
    private BlockView _lastBlockView;
    private bool _isBlockBuild;
    private int _countBlocksGame;

    public ReactiveCommand OnTombDestroyCommand = new ();
    public ReactiveCommand OnWinGame = new();
    public ReactiveCommand OnBlocksEqualCommand = new();
    public ReactiveCommand OnSizeLimitExceededCommand = new();

    public TombController(Tomb tomb)
    {
        _tomb = tomb;
    }

    public void SetCountBlocksGame(int value)
        => _countBlocksGame = value;

    public bool IsBlockBuild => _isBlockBuild;

    public void AddBlockToTomb(BlockView blockView)
    {
        _isBlockBuild = true;
        _lastBlockView = blockView;
        blockView.OnBlockDownCommand.Subscribe(_ => { OnTombDestroyCommand.Execute(); });
        blockView.OnSizeLimitExceededCommand.Subscribe(_ => { OnSizeLimitExceededCommand.Execute(); });
        OnWinGame.Subscribe(_ => { blockView.SetStaticBodyTypeRigidbody(); });

        var block = new Block();
        block.Center = blockView.Center;
        block.UpdatePosition(blockView.transform.position);
        block.SetSpeedIncreaseScale(blockView.SpeedIncreaseScale);
        block.SetStepIncreaseScale(blockView.StepIncreaseScale);
        block.Position.Subscribe((value) => { blockView.UpdatePosition(value); });
        block.Scale.Subscribe((value) => { blockView.UpdateScale(value); });

        _tomb.BuildTomb(block);
        AudioManager.Instance.PlayBuildBlock();
    }

    public void ActionAfterBuildBlock()
    {
        if (_tomb.TryBlocksEqual())
            OnBlocksEqualCommand.Execute();

        _lastBlockView.NotCollisionWithWall();

        if (_tomb.TryLastBlockScale() == false)
        {
            OnSizeLimitExceededCommand.Execute();
            _tomb.DeleteLastBlock();
            _tomb.SetIsIncreaseScaleLastBlock(false);
            _lastBlockView.StartCoroutineDropBlock();
        }
    }

    public bool TryBuildBlockTop()
        => _tomb.CountBlocks == _countBlocksGame - 1;

    public void CheckingFinishLevel()
    {
        if (_countBlocksGame == _tomb.CountBlocks)
        {
            OnWinGame.Execute();
            _tomb.OnEndTombBuilded();

            if(ContainerSaveerPlayerPrefs.Instance.SaveerData.EndLevels.Contains(ContainerSaveerPlayerPrefs.Instance.SaveerData.Level.ToString()) == false)
                ContainerSaveerPlayerPrefs.Instance.SaveerData.EndLevels += ContainerSaveerPlayerPrefs.Instance.SaveerData.Level + ";";
        }
    }

    public void OnEndTimer()
    {
        if (_tomb.IsIncreaseScaleLastBlock)
        {
            _isBlockBuild = false;
            _tomb.DeleteLastBlock();
            _tomb.SetIsIncreaseScaleLastBlock(false);
            _lastBlockView.StartCoroutineDropBlock();
        }
    }

    public void BaseUpdate()
    {
        _tomb.IncreaseScaleLastBlock();
    }

    public void OnDestroy()
    {
        ManagerUniRx.Dispose(OnTombDestroyCommand);
        ManagerUniRx.Dispose(OnWinGame);
        ManagerUniRx.Dispose(OnBlocksEqualCommand);
        ManagerUniRx.Dispose(OnSizeLimitExceededCommand);
    }
}
