using UnityEngine;

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

    public void SpawnPlayer()
    {
        if (IsPlayerSpawned) 
        {
            //idk set to some transform instead of null, maybe the egg in the middle incase there will be a delay between death and respawning
            _cameraFollow.cameraTarget = null;
            Destroy(Player);
        }

        Player = Instantiate(_playerPrefab, PlayerSpawnPosition, Quaternion.identity);

        _camera.GetComponent<CameraFollow>().cameraTarget = Player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
