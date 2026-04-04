using UnityEngine;

public class UI_HPSpawner : MonoBehaviour
{
    [SerializeField] private GameObject egg;
    [SerializeField] private GameObject hpBarPartPrefab; // This is an egg, which represents 20% of hp

    private GameObject[] hpBarParts;

    // Random variables xD
    private float UHPD = 165f; // UI HP Part Distance, the distance between each hp bar part

    private void Start()
    {
        hpBarParts = new GameObject[5];
        for(int i = -2; i < 3; i++)
        {
            GameObject hpBarPart = Instantiate(hpBarPartPrefab, transform);
            hpBarPart.transform.localPosition = new Vector3(i * UHPD, 0, 0); // Adjusted spacing as needed
            hpBarPart.GetComponent<UI_HPPartLogic>().InitChange(i + 2, egg.GetComponent<Egg>());
            hpBarParts[i + 2] = hpBarPart;
        }
    }
}
