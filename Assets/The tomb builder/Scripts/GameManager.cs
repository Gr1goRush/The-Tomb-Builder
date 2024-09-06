using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SystemInput _systemInput;
    [SerializeField] private SpawnerController _spawnerController;
    [SerializeField] private DataTomb _dataTomb;
    [SerializeField] private TimelineManager _timelineManager;
    [SerializeField] private HealthManager _healthManager;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private FollowTomb _followTomb;
    [SerializeField] private PauseManager _pauseManager;
    [SerializeField] private GameOverView _gameOverView;
    [SerializeField] private PanelWin _panelWin;
    [SerializeField] private LevelManager _levelManager;

    private Tomb _tomb;

    private TombController _tombController;

    private void Start()
    {
        AudioManager.Instance.PlayMusicGame();

        _tomb = new Tomb(_dataTomb.PositionFirstBlock, _dataTomb.MinDistanceEqualBlock, _dataTomb.MinScaleBlock);
        _tombController = new TombController(_tomb);

        Subscriber();
        AddObjectsDisposable();

        _levelManager.LoadingLevel();
        _spawnerController.SpawnBlock(_tomb.LastBlock, _tombController.TryBuildBlockTop());
    }

    private void Subscriber()
    {
        _levelManager.LevelLoadingCommand.Subscribe(level => { _timelineManager.SetLongTimer(level.TimeBuildBlock); });
        _levelManager.LevelLoadingCommand.Subscribe(level => { _spawnerController.SetSpriteBlock(level.TypeBlock); });
        _levelManager.LevelLoadingCommand.Subscribe(level => { _spawnerController.SetSpriteBlockTop(level.TypeBlockTop); });
        _levelManager.LevelLoadingCommand.Subscribe(level => { _tombController.SetCountBlocksGame(level.CountBlocksGame); });

        _tombController.OnTombDestroyCommand.Subscribe(_ => { StartCoroutine(OnTombDestroy()); });
        _tombController.OnWinGame.Subscribe(_ => { StartCoroutine(_panelWin.SetActive(true)); });
        _tombController.OnWinGame.Subscribe(_ => { _timelineManager.SetPauseTimer(true); });
        _tombController.OnWinGame.Subscribe(_ => { _systemInput.OnSetActivePause(true); });
        _tombController.OnWinGame.Subscribe(_ => { ManagerUniRx.Dispose(_healthManager.GameOverCommand); });
        _tombController.OnBlocksEqualCommand.Subscribe(_ => { _scoreManager.AddScore(); });
        _tombController.OnSizeLimitExceededCommand.Subscribe(_ => { _timelineManager.RestartTimer(); });
        _tombController.OnSizeLimitExceededCommand.Subscribe(_ => { _tombController.OnEndTimer(); });
        _tombController.OnSizeLimitExceededCommand.Subscribe(_ => { _healthManager.Damage(); });

        _healthManager.GameOverCommand.Subscribe(_ => { _gameOverView.SetActive(true); });
        _healthManager.GameOverCommand.Subscribe(_ => { _timelineManager.SetPauseTimer(true); });
        _healthManager.GameOverCommand.Subscribe(_ => { _systemInput.OnSetActivePause(true); });

        _pauseManager.SubscribeOnContinueCommand(_ => { _systemInput.OnSetActivePause(false); });
        _pauseManager.SubscribeOnContinueCommand(_ => { _timelineManager.SetPauseTimer(false); });
        _pauseManager.ActivePauseComand.Subscribe(_ => { _timelineManager.SetPauseTimer(true); });
        _pauseManager.ActivePauseComand.Subscribe(_ => { _systemInput.OnSetActivePause(true); });

        _timelineManager.OnTimerEndCommand.Subscribe(_ => { _timelineManager.RestartTimer(); });
        _timelineManager.OnTimerEndCommand.Subscribe(_ => { _tombController.OnEndTimer(); });
        _timelineManager.OnTimerEndCommand.Subscribe(_ => { _healthManager.Damage(); });

        _systemInput.OnMouseDownCommand.Subscribe( _ => { _spawnerController.SpawnBlock(_tomb.LastBlock, _tombController.TryBuildBlockTop()); } );
        _systemInput.OnMouseDownCommand.Subscribe( _ => { if(_tomb.LastBlock is Block) _followTomb.SetTarget(_tomb.LastBlock.Position.Value); } );
        _systemInput.OnMouseDownCommand.Subscribe( _ => { _tomb.SetIsIncreaseScaleLastBlock(true); } );
        _systemInput.OnMouseUpCommand.Subscribe( _ => { _tomb.SetIsIncreaseScaleLastBlock(false); } );
        _systemInput.OnMouseUpCommand.Subscribe( _ => { _timelineManager.RestartTimer(); } );
        _systemInput.OnMouseUpCommand.Subscribe( _ => { if(_tombController.IsBlockBuild) _scoreManager.AddMoney(); } );
        _systemInput.OnMouseUpCommand.Subscribe( _ => { _tombController.CheckingFinishLevel(); } );
        _systemInput.OnMouseUpCommand.Subscribe( _ => { _tombController.ActionAfterBuildBlock(); } );

        _spawnerController.OnSpawnBlockComand.Subscribe((block) => { _tombController.AddBlockToTomb(block); });
    }

    private IEnumerator OnTombDestroy()
    {
        if (_panelWin.gameObject.activeSelf == false)
        {
            AudioManager.Instance.PlayDropTomb();
            yield return new WaitForSeconds(3);
            _healthManager.GameOverCommand.Execute();
        }
    }

    private void AddObjectsDisposable()
    {
        ManagerUniRx.AddObjectDisposable(_levelManager.LevelLoadingCommand);
        ManagerUniRx.AddObjectDisposable(_pauseManager.ActivePauseComand);
        ManagerUniRx.AddObjectDisposable(_systemInput.OnMouseDownCommand);
        ManagerUniRx.AddObjectDisposable(_systemInput.OnMouseUpCommand);
        ManagerUniRx.AddObjectDisposable(_spawnerController.OnSpawnBlockComand);
        ManagerUniRx.AddObjectDisposable(_timelineManager.OnTimerEndCommand);
        ManagerUniRx.AddObjectDisposable(_healthManager.GameOverCommand);
        ManagerUniRx.AddObjectDisposable(_tombController.OnTombDestroyCommand);
        ManagerUniRx.AddObjectDisposable(_tombController.OnBlocksEqualCommand);
        ManagerUniRx.AddObjectDisposable(_tombController.OnSizeLimitExceededCommand);
        ManagerUniRx.AddObjectDisposable(_tombController.OnWinGame);
    }

    private void FixedUpdate()
    {
        _tombController.BaseUpdate();
        _timelineManager.UpdateTimer();
    }

    private void OnDestroy()
    {
        _tombController.OnDestroy();
    }
}
