using UnityEngine;
using UnityEngine.UI;

public class UI_HPPartLogic : MonoBehaviour
{

    public Egg egg;

    private float hpPos;

    public static UI_HPPartLogic Instance;
    public void InitChange(float _hpPos, Egg _egg)
    {
        hpPos = _hpPos;
        egg = _egg;
    }

    private void Start()
    {
        Instance = this;
    }

    private void Update()
    {
        egg.GetComponent<SpriteRenderer>().material.SetFloat("_CrackStrength", Mathf.Min(egg.GetHealthPercentEgg(), 1f));
    }
}