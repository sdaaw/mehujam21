using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _enemyPrefabs;

    [SerializeField]
    private float _spawnRadius = 10f;

    public float spawnInterval = 1f;



    public Transform center; //the egg

    private float _timer;

    void Start()
    {
        if (center == null)
            center = transform;
    }

    void Update()
    {
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
        GameObject prefab = _enemyPrefabs[Random.Range(0, _enemyPrefabs.Length)];
        GameObject enemy = Instantiate(prefab, spawnPoint, Quaternion.identity);
        enemy.GetComponent<Entity>().SetTarget(center);

    }

    private Vector2 GetRandomPointOnCircle()
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        return (Vector2)center.position + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * _spawnRadius;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 c = center != null ? center.position : transform.position;
        Gizmos.DrawWireSphere(c, _spawnRadius);
    }
}
