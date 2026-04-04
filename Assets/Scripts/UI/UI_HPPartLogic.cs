using UnityEngine;
using UnityEngine.UI;

public class UI_HPPartLogic : MonoBehaviour
{

    [SerializeField] private Egg egg;

    private float hpPos; 
    public void InitChange(float _hpPos, Egg _egg)
    {
        hpPos = _hpPos;
        egg = _egg;
    }

    private void Start()
    {
        if (egg == null)
        {
            GameObject eggObj = GameObject.FindWithTag("Egg");
        }
    }

    private void Update()
    {
        if(egg.GetHealthPercentEgg() < hpPos/5)
        {
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f); // Set to semi-transparent
        }
        else
        {
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1f); // Set to fully opaque
        }
    }
}
