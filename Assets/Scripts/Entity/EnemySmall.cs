using UnityEngine;

public class EnemySmall : Entity
{
    [SerializeField]
    private Sprite[] _runAnim;

    [SerializeField]
    private float _spriteAnimSpeed;

    private float _animTimer;

    private int _animIndex;

    private SpriteRenderer _sr;

    protected override void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        base.Start();
    }
    protected override void Attack()
    {
        
        if (target == null) return;
        target?.GetComponent<Entity>()?.TakeDamage(damage);
    }

    protected override void OnDamaged(float amount)
    {
        AudioManager.Instance.PlayHitSfx();
        base.OnDamaged(amount);
    }
    protected override void OnDeath()
    {
        GameManager.Instance.EnemiesAlive.Remove(gameObject);
        GameManager.Instance.TotalKills++;
        base.OnDeath();
    }
    protected override void MoveTowardsTarget()
    {
        _animTimer += Time.deltaTime;
        if(_animTimer > _spriteAnimSpeed)
        {
            _animIndex++;
            if (_animIndex >= _runAnim.Length) _animIndex = 0;
            _sr.sprite = _runAnim[_animIndex];
            _animTimer = 0;
        }
        base.MoveTowardsTarget();
    }
}
