using UnityEngine;

public class UI_HPLogic : MonoBehaviour
{
    [SerializeField] private GameObject egg;
    [SerializeField] private GameObject hpBarPartPrefab; // This is an egg, which represents 20% of hp

    private GameObject[] hpBarParts;

    private void Start()
    {
        for(int i = 0; i <= 2; i++)
        {
            GameObject hpBarPart = Instantiate(hpBarPartPrefab, transform);
            hpBarPart.transform.localPosition = new Vector3(i * 1f, 0, 0); // Adjust spacing as needed
            hpBarParts[i] = hpBarPart;
        }
    }
}
