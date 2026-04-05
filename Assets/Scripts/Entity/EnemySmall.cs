using UnityEngine;

public class EnemySmall : Entity
{

    protected override void Attack()
    {
        if (target == null) return;
        target?.GetComponent<Entity>()?.TakeDamage(damage);
    }
    protected override void OnDeath()
    {
        GameManager.Instance.EnemiesAlive.Remove(gameObject);
        base.OnDeath();
    }
}
