using UnityEngine;

public class SpinningAttack : MonoBehaviour
{

    public GameObject orbPrefab;
    public int orbCount = 1;
    public float orbitRadius = 2f;
    public float rotationSpeed = 120f; // degrees per second

    public float damage = 20f;
    public float orbScale = 1f;

    private float _currAngle;
    private GameObject[] _orbs;

    void Start()
    {
        SpawnOrbs();
    }

    void Update()
    {
        _currAngle += rotationSpeed * Time.deltaTime;
        PositionOrbs();
    }

    private void SpawnOrbs()
    {
        if (_orbs != null)
        {
            foreach (var o in _orbs)
            {
                if (o != null) Destroy(o);
            }
        }

        _orbs = new GameObject[orbCount];

        for (int i = 0; i < orbCount; i++)
        {
            _orbs[i] = Instantiate(orbPrefab, transform.position, Quaternion.identity);
            _orbs[i].transform.localScale = Vector3.one * orbScale;

            var hitbox = _orbs[i].GetComponent<OrbHitbox>();
            if (hitbox != null) hitbox.Init(damage, gameObject);
        }
    }

    private void PositionOrbs()
    {
        float angleStep = 360f / orbCount;

        for (int i = 0; i < orbCount; i++)
        {
            if (_orbs[i] == null) continue;

            float angle = (_currAngle + angleStep * i) * Mathf.Deg2Rad;
            Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * orbitRadius;
            _orbs[i].transform.position = (Vector2)transform.position + offset;
        }
    }
    public void Upgrade()
    {
        orbCount++;
        orbitRadius += 0.3f;
        SpawnOrbs();
    }

    void OnDestroy()
    {
        if (_orbs != null)
        {
            foreach (var o in _orbs)
            {
                if (o != null) Destroy(o);
            }
        }
    }
}
