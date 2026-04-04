using System.Collections;
using UnityEngine;

public class Infection : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float dotDamage;

    public float castCooldown = 2f;

    public int spreadAmount;
    public bool _isEnabled;

    void Start()
    {
        StartCoroutine(InfectionCooldown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableSkill()
    {
        _isEnabled = true;
    }

    public Entity GetClosestEnemy(Vector3 from)
    {
        float closestDist = 9999f;
        GameObject closestEnemy = null;
        foreach(GameObject enemy in GameManager.Instance.EnemiesAlive)
        {
            if(Vector3.Distance(transform.position, enemy.transform.position) < closestDist)
            {
                closestDist = Vector3.Distance(transform.position, enemy.transform.position);
                closestEnemy = enemy;
            }
        }

        return closestEnemy.GetComponent<Entity>();
    }

    IEnumerator InfectionCooldown()
    {
        while (true) 
        {
            yield return new WaitForSeconds(castCooldown);
            if (!_isEnabled) continue;

            Entity closestEnemy = GetClosestEnemy(transform.position);
            closestEnemy.ApplyDot(dotDamage);
            closestEnemy.GetComponent<SpriteRenderer>().color = Color.blue;
            for (int i = 0; i < spreadAmount; i++)
            {
                closestEnemy = GetClosestEnemy(closestEnemy.transform.position);
                closestEnemy.GetComponent<SpriteRenderer>().color = Color.blue;
                closestEnemy.ApplyDot(dotDamage);
            }
        }
        
    }

}
