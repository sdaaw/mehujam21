using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class OrbHitbox : MonoBehaviour
{
    private float _damage;
    private GameObject _owner;
    private float _hitCooldown = 0.2f;
    private float _hitTimer;


    public void Init(float damage, GameObject owner)
    {
        _damage = damage;
        _owner = owner;
    }

    void Update()
    {
        if (_hitTimer > 0) _hitTimer -= Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == _owner) return;
        if (!col.CompareTag("Enemy")) return;
        if (_hitTimer > 0) return;

        Vector2 knockback = (col.transform.position - _owner.transform.position).normalized * 2f;
        col.GetComponent<Entity>()?.TakeDamage(_damage, knockback);
        _hitTimer = _hitCooldown;
    }
}
