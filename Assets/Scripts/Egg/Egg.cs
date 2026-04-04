using UnityEngine;

public class Egg : Entity
{
    public float HatchProgress;
    public float HatchTime;
    public float HatchSpeed;
    [Tooltip("Passive HP lost per second (set in inspector)")]
    public float passiveHealthDecay = 100f;

    // Eggs don't move/animate/attack but still use the Entity HP system
    protected override void MoveTowardsTarget() { }
    protected override void HandleAnimation() { }
    protected override void Attack() { }
    protected override void OnCollisionStay2D(Collision2D col) { }

    // Expose the base health percent for UI
    public float GetHealthPercentEgg()
    {
        passiveHealthDecay-= Time.deltaTime * 0.5f;
        Debug.Log(passiveHealthDecay);
        return passiveHealthDecay / 100;
    }

}
