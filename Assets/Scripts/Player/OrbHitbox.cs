using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class OrbHitbox : MonoBehaviour
{
    private float damage;
    private GameObject owner;
    private float hitCooldown = 0.2f;
    private float hitTimer;

    private Transform _player; //for knockback vector

    public void Setup(float damage, GameObject owner)
    {
        _player = GameManager.Instance.Player.transform;
        this.damage = damage;
        this.owner = owner;
    }

    void Update()
    {
        if (hitTimer > 0) hitTimer -= Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == owner) return;
        if (!col.CompareTag("Enemy")) return;
        if (hitTimer > 0) return;

        Vector2 knockback = (col.transform.position - _player.transform.position).normalized * 2f;
        col.GetComponent<Entity>()?.TakeDamage(damage, knockback);
        hitTimer = hitCooldown;
    }
}
