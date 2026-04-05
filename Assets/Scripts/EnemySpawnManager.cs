using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{

    public static EnemySpawnManager Instance;
    [SerializeField]
    private GameObject[] _enemyPrefabs;

    [SerializeField]
    private float _spawnRadius = 10f;

    public float spawnInterval = 1f;

    public float difficulty = 1;

    public Transform center; //the egg

    private float _timer;

    void Start()
    {
        Instance = this;
        if (center == null)
            center = transform;
    }

    void Update()
    {
        if (GameManager.Instance.IsGameOver) return;
        _timer += Time.deltaTime;
        if (_timer >= spawnInterval)
        {
            _timer = 0f;
            TrySpawn();
        }
    }

    private void TrySpawn()
    {
        if (_enemyPrefabs.Length == 0) return;

        Vector2 spawnPoint = GetRandomPointOnCircle();
        Entity ent;
        GameObject prefab = _enemyPrefabs[Random.Range(0, _enemyPrefabs.Length)];
        GameObject enemy = Instantiate(prefab, spawnPoint, Quaternion.identity);
        enemy.transform.localScale = new Vector2(enemy.transform.localScale.x + Random.Range(-0.2f, 0.3f), enemy.transform.localScale.y + Random.Range(-0.2f, 0.3f));
        ent = enemy.GetComponent<Entity>();
        ent.maxHealth = ent.maxHealth * difficulty;
        ent.moveSpeed = ent.moveSpeed * difficulty;
        ent.damage = ent.damage * difficulty + Random.Range(-3, 3);
        ent.SetTarget(GameManager.Instance.BigEgg.transform);
        GameManager.Instance.EnemiesAlive.Add(enemy);
    }

    private Vector2 GetRandomPointOnCircle()
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        return (Vector2)center.position + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * _spawnRadius;
    }
}
