using UnityEngine;

[RequireComponent(typeof(BoltHolder))]
[RequireComponent(typeof(CreateObject))]
public class Game : MonoBehaviour
{
    [SerializeField] private SpawnContainer _spawnContainer;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private EndScreen _endScreen;
    [SerializeField] private CellStorage _cellStorage;
    [SerializeField] private ContainerActiveHolder _containerActiveHolder;

    private BoltHolder _boltHolder;
    private CreateObject _createObject;

    private void Awake()
    {
        _createObject = GetComponent<CreateObject>();
        _boltHolder = GetComponent<BoltHolder>();
    }

    private void OnEnable()
    {
        _cellStorage.EndGame += OnGameOver;
        _startScreen.PlayButtonClicked += OnPlayButtenClick;
        _endScreen.RestartButtonClicked += OnRestartButtenClick;
    }

    private void OnDisable()
    {
        _cellStorage.EndGame -= OnGameOver;
        _startScreen.PlayButtonClicked -= OnPlayButtenClick;
        _endScreen.RestartButtonClicked -= OnRestartButtenClick;
    }

    private void Start()
    {
        Time.timeScale = 0f;
        _startScreen.Open();
        _endScreen.Close();
    }

    private void OnPlayButtenClick()
    {
        _startScreen.Close();
        StartGame();
    }

    private void OnRestartButtenClick()
    {
        _endScreen.Close();
        StartGame();
    }

    private void OnGameOver()
    {
        Time.timeScale = 0f;
        _endScreen.Open();
    }

    public void StartGame()
    {
        ObjectCentral createObject = _createObject.Create();
        _boltHolder.Restart(createObject.Bolts);
        _cellStorage.Restart();
        _containerActiveHolder.Restart();
        _spawnContainer.Restart(_boltHolder);
    }
}
