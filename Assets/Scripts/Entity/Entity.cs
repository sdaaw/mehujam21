using System;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float maxHealth = 100f;
    public float moveSpeed = 3f;
    public float damage = 10f;
    public float attackCooldown = 1f;

    public float knockbackResistance = 0f; // 0 = full knockback | 1 = immune

    protected float currentHealth;
    protected float attackTimer;
    protected Rigidbody2D rb;
    protected Transform target;

    private EasingUtil _anim = new();
    private Quaternion _oldRotation, _newRotation;
    [SerializeField]
    private EasingUtil.EasingType _animType;
    [SerializeField]
    private float _animSpeed;

    

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        target = GameManager.Instance.BigEgg.transform;
    }

    protected virtual void Update()
    {
        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;

    }

    protected virtual void FixedUpdate()
    {
        HandleAnimation();
        MoveTowardsTarget();
    }

    protected virtual void HandleAnimation()
    {
        if (!_anim.IsFinished)
        {
            transform.rotation = _anim.EaseQuaternion(_oldRotation, _newRotation, _animSpeed, _animType);
        }
        else
        {
            _oldRotation = transform.rotation;
            _newRotation.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + 90);
            _anim.IsFinished = false;
        }
    }

    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }

    protected virtual void MoveTowardsTarget()
    {
        if (target == null) return;

        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;

        FlipSprite(direction.x);
    }

    protected void FlipSprite(float directionX)
    {
        if (directionX != 0)
            transform.localScale = new Vector3(directionX > 0 ? 1 : -1, 1, 1);
    }

    public virtual void TakeDamage(float amount, Vector2 knockbackForce = default)
    {
        currentHealth -= amount;

        if (knockbackForce != default)
            ApplyKnockback(knockbackForce);

        OnDamaged(amount);

        if (currentHealth <= 0)
            Die();
    }

    public float GetHealthPercent() => currentHealth / maxHealth;

    protected virtual void ApplyKnockback(Vector2 force)
    {
        transform.position += (Vector3)(force * (1f - knockbackResistance));
    }

    protected virtual void TryAttack()
    {
        if (attackTimer > 0) return;
        attackTimer = attackCooldown;
        Attack();
    }

    protected virtual void Attack()
    {
        // override thissss
    }

    protected virtual void OnDamaged(float amount)
    {
        DamageNumberSpawner.Instance?.Spawn(amount, transform.position);
    }

    protected virtual void OnDeath()
    {
        GameManager.Instance.EnemiesAlive.Remove(gameObject);
    }

    protected virtual void Die()
    {
        OnDeath();
        Destroy(gameObject);
    }

    protected virtual void OnCollisionStay2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Egg")) return;
        TryAttack();
    }
}
