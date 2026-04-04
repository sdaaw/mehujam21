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

    [SerializeField]
    private GameObject _projectilePrefab;

    public bool IsInfectionUpgrade;
    public bool IsProjectileUpgrade;

    public static SpinningAttack Instance;

    public float projectileShootCooldown;
    public float projectileDamage;
    public float projectileSpeed;

    private float _projectileTimer;

    public int UpgradeLevel = 0;

    void Start()
    {
        Instance = this;
        SpawnOrbs();
    }

    void Update()
    {

        _currAngle += rotationSpeed * Time.deltaTime;
        PositionOrbs();
        if (!IsProjectileUpgrade) return;
        _projectileTimer += Time.deltaTime;
        if(_projectileTimer > projectileShootCooldown)
        {
            _projectileTimer = 0;
            SpawnProjectile();
        }
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
            if (hitbox != null) hitbox.Init(damage, gameObject, IsInfectionUpgrade);
        }
    }

    private void SpawnProjectile()
    {
        GameObject projectile = Instantiate(_projectilePrefab, _orbs[Random.Range(0, _orbs.Length)].transform.position, Quaternion.identity);
        projectile.GetComponent<ProjectileHitbox>().Init(Random.Range(projectileSpeed, projectileSpeed + 4), projectileDamage, gameObject);
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
        UpgradeLevel++;
        if(UpgradeLevel == 3)
        {
            IsInfectionUpgrade = true;
        }
        if (UpgradeLevel == 5)
        {
            IsProjectileUpgrade = true;
        }
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
