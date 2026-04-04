using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infection : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float dotDamage;

    public float castCooldown = 2f;
    public float castRange = 5f;

    public int spreadAmount;
    public bool _isEnabled;


    void Start()
    {
        StartCoroutine(InfectionCooldown());
        EnableSkill();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Upgrade()
    {
        castCooldown -= castCooldown * 0.8f;
        dotDamage += 2f;
        spreadAmount += 1;
    }

    public void EnableSkill()
    {
        _isEnabled = true;
    }

    public Entity GetClosestEnemy(Vector3 from, GameObject source)
    {
        float closestDist = 9999f;
        GameObject closestEnemy = null;
        foreach(GameObject enemy in GameManager.Instance.EnemiesAlive)
        {
            if (enemy == source) continue;
            if (enemy.GetComponent<Entity>().dotDamagePool > 0) continue;
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < closestDist)
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
            if (GameManager.Instance.EnemiesAlive.Count <= 0) continue;
            if (!_isEnabled) continue;


            Entity closestEnemy = GetClosestEnemy(transform.position, gameObject);
            if(closestEnemy == null) continue;

            closestEnemy.ApplyDot(dotDamage);
            closestEnemy.GetComponent<SpriteRenderer>().color = Color.blue;
            List<Entity> infected = new();
            for (int i = 0; i < spreadAmount; i++)
            {
                closestEnemy = GetClosestEnemy(closestEnemy.transform.position, closestEnemy.gameObject);
                closestEnemy.GetComponent<SpriteRenderer>().color = Color.blue;
                closestEnemy.ApplyDot(dotDamage);
            }
        }
        
    }

}
