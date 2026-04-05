using System.Collections;
using TMPro;
using UnityEngine;

public class UpgradeText : MonoBehaviour
{

    private EasingUtil _startEasingScale = new();
    private EasingUtil _startEasingRotation = new();
    private EasingUtil _startEasingPosition = new();
    private TMP_Text _text;
    private Vector2 _textEndPosition;

    public EasingUtil.EasingType TextEasingType;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        _text = GetComponent<TMP_Text>();
        _textEndPosition = _text.rectTransform.anchoredPosition;
    }

    public void ShowText(string text)
    {
        _text.text = text;
        StartCoroutine(TextAnimationDeath());
    }

    IEnumerator TextAnimationDeath()
    {
        Vector3 startSize = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
        Vector3 startPos = new Vector3(Random.Range(-20, 20), Random.Range(-20, 20), 0);
        Quaternion startRot = Random.rotation;
        _startEasingScale.IsFinished = false;
        _startEasingRotation.IsFinished = false;
        _startEasingPosition.IsFinished = false;
        while (!_startEasingScale.IsFinished)
        {
            _text.rectTransform.anchoredPosition = _startEasingPosition.EaseVector3(startPos, _textEndPosition, 0.2f, TextEasingType);
            _text.transform.localScale = _startEasingScale.EaseVector3(startSize, Vector3.one, 0.2f, TextEasingType);
            _text.transform.rotation = _startEasingRotation.EaseQuaternion(startRot, Quaternion.identity, 0.2f, TextEasingType);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(3f);
        _text.text = "";

    }
}
