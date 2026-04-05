using TMPro;
using UnityEngine;

public class UpgradeAnnouncer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    private TMP_Text _upgradeLogText;
    [SerializeField]
    private TMP_Text _announceText;

    public static UpgradeAnnouncer Instance;
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddLog(string message)
    {
        _upgradeLogText.text = message;
    }

    public void UpgradeText(string text)
    {
        _announceText.GetComponent<UpgradeText>().ShowText(text);
    }
}
