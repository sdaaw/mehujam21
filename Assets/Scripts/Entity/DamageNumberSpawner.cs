using UnityEngine;

public class DamageNumberSpawner : MonoBehaviour
{
    public static DamageNumberSpawner Instance { get; private set; }

    public GameObject damageNumberPrefab;
    public Canvas worldCanvas;

    public Color normalColor = Color.white;
    public Color critColor = Color.yellow;
    public Color healColor = Color.green;
    public Color dotColor = Color.plum;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void Spawn(float damage, Vector3 worldPosition, bool isCrit = false)
    {
        SpawnWithColor(damage, worldPosition, isCrit ? critColor : normalColor);
    }

    public void SpawnHeal(float amount, Vector3 worldPosition)
    {
        SpawnWithColor(amount, worldPosition, healColor);
    }

    public void SpawnDot(float amount, Vector3 worldPosition)
    {
        SpawnWithColor(amount, worldPosition, dotColor);
    }

    void SpawnWithColor(float value, Vector3 worldPosition, Color color)
    {
        if (damageNumberPrefab == null || worldCanvas == null) return;

        GameObject go = Instantiate(damageNumberPrefab, worldCanvas.transform);
        var dn = go.GetComponent<DamageText>();
        dn.Init(value, worldPosition, color);

        if (color == critColor) dn.startScale = 1.4f;
        if (color == dotColor) dn.startScale = 0.8f;
    }
}