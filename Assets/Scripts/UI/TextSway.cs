using UnityEngine;

public class TextSway : MonoBehaviour
{

    public float xswaySpeed, xswayAmount, yswaySpeed, yswayAmount;
    public float randomVariation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        xswaySpeed += Random.Range(-randomVariation, randomVariation);
        yswayAmount += Random.Range(-randomVariation, randomVariation);
        yswaySpeed += Random.Range(-randomVariation, randomVariation);
        yswayAmount += Random.Range(-randomVariation, randomVariation);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 sway = new Vector3(
            transform.position.x + Mathf.Sin(Time.time * xswaySpeed) * xswayAmount * Time.deltaTime,
            transform.position.y + Mathf.Cos(Time.time * yswaySpeed) * yswayAmount * Time.deltaTime,
            transform.position.z);
        transform.position = sway;
    }
}
