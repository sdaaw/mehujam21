
using UnityEngine;

public class Egg : Entity
{
    public float HatchProgress;
    public float HatchTime;
    public float HatchSpeed;

    private float _timer;

    public float xswaySpeed, xswayAmount, yswaySpeed, yswayAmount;
    public float randomVariation;
    private EasingUtil _easingDown = new();
    private EasingUtil _easingUp = new();
    private Vector3 _startScale;



    [SerializeField]
    private EasingUtil.EasingType _sizeEasingType;
    [SerializeField]
    private float _sizeEasingSpeed;

    private SpriteRenderer _sr;
    // Eggs don't move/animate/attack but still use the Entity HP system
    protected override void MoveTowardsTarget() { }
    protected override void HandleAnimation() { }
    protected override void Attack() { }

    protected override void Start()
    {
        _startScale = transform.localScale;
        _sr = GetComponent<SpriteRenderer>();
        base.Start();
    }

    protected override void Update()
    {
        _timer += Time.deltaTime;
        if(_timer > HatchTime)
        {
            GameManager.Instance.UpdateEggHatchText("Egg Hatching \n" + HatchProgress.ToString("F0") + "%");
            HatchProgress += HatchSpeed;
            _timer = 0;
        }
        if(HatchProgress >= 100)
        {
            Hatch();
            HatchProgress = 0;
        }

        if(_easingDown.IsFinished && _easingUp.IsFinished)
        {
            Vector3 sway = new Vector3(
                transform.localScale.x + Mathf.Sin(Time.time * xswaySpeed) * xswayAmount * Time.deltaTime,
                transform.localScale.y + Mathf.Cos(Time.time * yswaySpeed) * yswayAmount * Time.deltaTime,
                transform.localScale.z);
            transform.localScale = sway;
        }

        if(!_easingDown.IsFinished)
        {
            transform.localScale = _easingDown.EaseVector3(_startScale, Vector3.zero, _sizeEasingSpeed, _sizeEasingType);
            if(_easingDown.IsFinished)
            {
                _sr.color = new Color(Random.Range(0.4f, 1f), Random.Range(0.4f, 1f), Random.Range(0.4f, 1f));
                _easingUp.IsFinished = false;
            }
        }
        if(!_easingUp.IsFinished)
        {
            transform.localScale = _easingUp.EaseVector3(Vector3.zero, _startScale, _sizeEasingSpeed, _sizeEasingType);
        }
        
    }

    private void Hatch()
    {
        SpinningAttack.Instance.Upgrade();
        _easingDown.IsFinished = false;
    }

    protected override void OnDeath()
    {
        GameManager.Instance.IsGameOver = true;
        GameManager.Instance.GameOver();
    }
    protected override void OnDamaged(float amount)
    {
        GameManager.Instance.UpdateEggHealthText("Egg Health \n" + currentHealth.ToString("F0"));
        base.OnDamaged(amount);
    }
}
