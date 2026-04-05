using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infection : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float dotDamage;

    public int spreadAmount;

    public static Infection Instance;

    public float InfectionCooldown;
    private float _timer;

    private bool _canApply;

    void Start()
    {
        Instance = this;
    }

    private void Update()
    {
        if (_canApply) return;
        _timer += Time.deltaTime;
        if (_timer > InfectionCooldown) 
        {
            _canApply = true;
        }
    }

    public Entity GetClosestEnemy(Vector3 from, GameObject source)
    {
        float closestDist = 9999f;
        GameObject closestEnemy = null;
        foreach(GameObject enemy in GameManager.Instance.EnemiesAlive)
        {
            if (enemy == source) continue;
            if (enemy.GetComponent<Entity>().dotDamagePool > 0) continue;
            if (!enemy.GetComponent<Entity>()) continue;
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < closestDist)
            {
                closestDist = Vector3.Distance(transform.position, enemy.transform.position);
                closestEnemy = enemy;
            }
        }
        //idk it was giving me a lot of errors but it doesnt anymore so thats nice hehe
        if (!closestEnemy?.GetComponent<Entity>()) return null;
        return closestEnemy?.GetComponent<Entity>();
    }

    public void ApplyInfection(Entity hitEntity)
    {
        if (!_canApply) return;

        _canApply = false;
        if (GameManager.Instance.EnemiesAlive.Count <= 0) return;


        Entity closestEnemy = GetClosestEnemy(transform.position, gameObject);
        if (closestEnemy == null) return;

        closestEnemy.ApplyDot(dotDamage);
        closestEnemy.GetComponent<SpriteRenderer>().color = Color.darkOliveGreen;
        List<Entity> infected = new();
        for (int i = 0; i < spreadAmount; i++)
        {
            closestEnemy = GetClosestEnemy(closestEnemy.transform.position, closestEnemy.gameObject);
            if (closestEnemy == null) return;
            closestEnemy.GetComponent<SpriteRenderer>().color = Color.darkOliveGreen;
            closestEnemy.ApplyDot(dotDamage);
        }
    }
}
