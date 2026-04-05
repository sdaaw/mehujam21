using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private GameObject _playerPrefab;

    [HideInInspector]
    public GameObject Player;

    public Vector2 PlayerSpawnPosition;

    public bool IsPlayerSpawned;

    private Camera _camera;
    private CameraFollow _cameraFollow;

    public static GameManager Instance;

    [SerializeField]
    private GameObject _eggPrefab;

    [HideInInspector]
    public GameObject BigEgg;

    public List<GameObject> EnemiesAlive = new();

    [SerializeField]
    private TMP_Text _gameOverText;

    [SerializeField]
    private TMP_Text _eggHatchText;

    [SerializeField]
    private TMP_Text _eggHealthText;
    public bool IsGameOver;

    private float _timer;

    public int TotalKills;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        _camera = Camera.main;
        _cameraFollow = _camera.GetComponent<CameraFollow>();
        SpawnPlayer();


        //cache into memory to avoid initial lagspike, idk what else to come up with rn but it started to trigger me aaaaaaa
        DamageNumberSpawner.Instance?.Spawn(1f, new Vector2(-500, -500));
    }

    public void UpdateEggHatchText(string text)
    {
        _eggHatchText.text = text;
    }

    public void UpdateEggHealthText(string text)
    {
        _eggHealthText.text = text;
    }

    public void SpawnPlayer()
    {
        if (IsPlayerSpawned) 
        {
            //idk set to some transform instead of null, maybe the egg in the middle incase there will be a delay between death and respawning
            _cameraFollow.cameraTarget = null;
            Destroy(Player);
        }

        Player = Instantiate(_playerPrefab, PlayerSpawnPosition, Quaternion.identity);
        BigEgg = Instantiate(_eggPrefab, new Vector3(0, 5, 0), Quaternion.identity);

        _camera.GetComponent<CameraFollow>().cameraTarget = Player.transform;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if(_timer > 40)
        {
            EnemySpawnManager.Instance.difficulty = 1.2f;
        }
        if (_timer > 70)
        {
            EnemySpawnManager.Instance.difficulty = 1.6f;
            EnemySpawnManager.Instance.spawnInterval = 0.5f;
        }
        if (_timer > 100)
        {
            EnemySpawnManager.Instance.difficulty = 3f;
            EnemySpawnManager.Instance.spawnInterval = 0.3f;
        }
        if (_timer > 140)
        {
            EnemySpawnManager.Instance.difficulty = 3.4f;
            EnemySpawnManager.Instance.spawnInterval = 0.12f;
        }
        if (_timer > 150)
        {
            EnemySpawnManager.Instance.difficulty = 4f;
            EnemySpawnManager.Instance.spawnInterval = 0.1f;
        }
        if (_timer > 160)
        {
            EnemySpawnManager.Instance.difficulty = 4f;
            EnemySpawnManager.Instance.spawnInterval = 0.08f;
        }
        if (_timer > 180)
        {
            EnemySpawnManager.Instance.difficulty = 4f;
            EnemySpawnManager.Instance.spawnInterval = 0.06f;
        }
    }

    public void GameOver()
    {
        _gameOverText.text = "The Egg has been sogged. >.> \n You killed " + TotalKills + " slimes. \n Press R to try again.";
    }
}
