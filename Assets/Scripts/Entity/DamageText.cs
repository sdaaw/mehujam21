using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{

    public float floatSpeed = 1.5f;
    public Vector2 randomSpread = new Vector2(0.5f, 0f);

    public float startScale = 1f;
    public float endScale = 1.4f;
    private TMP_Text _text;
    private Vector3 _moveDir;
    private Camera _camera;
    private RectTransform _rect;
    private Canvas _canvas;

    private Vector3 _worldPosition;

    private EasingUtil _anim = new();
    [SerializeField]
    private EasingUtil.EasingType _animType;

    [SerializeField]
    private float _animSpeed = 0.6f;

    public void Init(float damage, Vector3 worldPosition, Color color)
    {
        _text = GetComponent<TextMeshProUGUI>();
        _rect = GetComponent<RectTransform>();
        _camera = Camera.main;
        _canvas = GetComponentInParent<Canvas>();
        _text.text = "-" + Mathf.RoundToInt(damage).ToString();
        _text.color = color;

        SetCanvasPosition(worldPosition);

        //some random horizontal offset
        _moveDir = new Vector3(
            Random.Range(-randomSpread.x, randomSpread.x),
            floatSpeed,
            0f
        );
        _anim.IsFinished = false;

        _worldPosition = worldPosition + _moveDir;
    }

    void SetCanvasPosition(Vector3 worldPosition)
    {
        Vector2 screenPoint = _camera.WorldToScreenPoint(worldPosition);

        if (_canvas.renderMode == RenderMode.ScreenSpaceOverlay)
        {
            _rect.position = screenPoint;
        }
        else
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.transform as RectTransform,
                screenPoint,
                _canvas.renderMode == RenderMode.ScreenSpaceCamera ? _canvas.worldCamera : null,
                out Vector2 localPoint
            );
            _rect.localPosition = localPoint;
        }
    }

    void Update()
    {

        if(!_anim.IsFinished)
        {
            _rect.localScale = _anim.EaseVector3(Vector3.zero, Vector3.one * endScale, _animSpeed, _animType);
        }
        else
        {
            Destroy(gameObject);
        }

        _rect.position += _moveDir * Time.deltaTime;
        SetCanvasPosition(_worldPosition);
    }
}
