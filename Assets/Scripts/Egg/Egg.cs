using UnityEngine;
public class Egg : Entity
{
    public float HatchProgress;
    public float HatchTime;
    public float HatchSpeed;


    // CHECKPOINT: Here I just removed the ability to move, attack, and rotate for the egg
    protected override void MoveTowardsTarget()
    {
        // Eggs shouldnt move
    }

    protected override void HandleAnimation()
    {
        // Eggs shouldnt rotate?
    }

    protected override void Attack()
    {
        // Eggs shouldnt attack
    }
}
