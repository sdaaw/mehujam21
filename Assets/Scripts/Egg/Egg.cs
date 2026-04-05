using UnityEngine;

public class Egg : Entity
{
    public float HatchProgress;
    public float HatchTime;
    public float HatchSpeed;
    public float passiveHealthDecay = 100f;

    // Eggs don't move/animate/attack but still use the Entity HP system
    protected override void MoveTowardsTarget() { }
    protected override void HandleAnimation() { }
    protected override void Attack() { }

    // Expose the base health percent for UI
    public float GetHealthPercentEgg()
    {
        passiveHealthDecay-= Time.deltaTime * 0.5f;
        //Debug.Log(passiveHealthDecay);
        return passiveHealthDecay / 100;
    }

    protected override void OnDeath()
    {
        GameManager.Instance.IsGameOver = true;
        GameManager.Instance.GameOver();
    }
    protected override void OnDamaged(float amount)
    {
        base.OnDamaged(amount);
    }
}
