using UnityEngine;

public class EnemySmall : Entity
{

    protected override void Attack()
    {
        target.GetComponent<Entity>()?.TakeDamage(damage);
    }
}
