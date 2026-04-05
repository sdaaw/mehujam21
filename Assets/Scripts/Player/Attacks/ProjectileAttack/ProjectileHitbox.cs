using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class ProjectileHitbox : MonoBehaviour
{

    private float _damage;
    private float _speed;
    private GameObject _owner;
    private float _lifeTime = 2f;

    private Vector2 _dir;

    private float _angle;

    private void Start()
    {
        _dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }
    public void Init(float speed, float damage, GameObject owner)
    {
        _damage = damage;
        _owner = owner;
        _speed = speed;
        Destroy(gameObject, _lifeTime);
    }

    private void Update()
    {
        _angle += 0.2f * Time.deltaTime;
        transform.Translate(_dir * _speed * Time.deltaTime);
        transform.Rotate(Vector3.forward, _angle);
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == _owner) return;
        if (!col.CompareTag("Enemy")) return;

        Vector2 knockback = (col.transform.position - _owner.transform.position).normalized * 2f;
        col.GetComponent<Entity>()?.TakeDamage(_damage, knockback);
        Destroy(gameObject);
    }
}
