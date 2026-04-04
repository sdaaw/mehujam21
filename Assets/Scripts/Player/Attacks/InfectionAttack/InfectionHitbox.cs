using UnityEngine;

public class InfectionHitbox : MonoBehaviour
{
    private float _damage;
    private GameObject _owner;
    private float _hitCooldown = 0.3f;
    private float _hitTimer;
    public void Init(float damage, GameObject owner)
    {
        _damage = damage;
        _owner = owner;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == _owner) return;
        if (!col.CompareTag("Enemy")) return;
        if (_hitTimer > 0) return;

        //Vector2 knockback = (col.transform.position - _owner.transform.position).normalized * 2f;
        col.GetComponent<Entity>()?.TakeDamage(_damage, Vector2.zero);
        _hitTimer = _hitCooldown;
    }
}
