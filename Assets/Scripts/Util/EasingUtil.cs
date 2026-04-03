
using System;
using UnityEngine;

public class EasingUtil
{

    public enum EasingType
    {
        Bounce,
        Circle,
        InOutBack,
        InBack,
        InOutElastic,
        InCubic
    }

    private float _elapsedTime;

    public bool IsFinished = true;

    public bool IsFirstCycleFinished = false;


    public Vector3 EaseVector3(Vector3 start, Vector3 end, float duration, EasingType type, bool pingpong = false)
    {
        if (pingpong) 
        {
            if(!IsFirstCycleFinished)
            {
                if (_elapsedTime < duration && !IsFinished)
                {
                    _elapsedTime += Time.deltaTime;
                    float t = _elapsedTime / duration;
                    t = EasingMode(t, type);
                    return Ease(start, end, t, Vector3.LerpUnclamped);
                }
                else
                {
                    IsFirstCycleFinished = true;
                    _elapsedTime = 0;
                    return end;
                }
            } else
            {
                if (_elapsedTime < duration && !IsFinished)
                {
                    _elapsedTime += Time.deltaTime;
                    float t = _elapsedTime / duration;
                    t = EasingMode(t, type);
                    return Ease(end, start, t, Vector3.LerpUnclamped);
                }
                else
                {
                    IsFinished = true;
                    _elapsedTime = 0;
                    return start;
                }
            }
        }


        if (_elapsedTime < duration && !IsFinished && !pingpong)
        {
            _elapsedTime += Time.deltaTime;
            float t = _elapsedTime / duration;
            t = EasingMode(t, type);
            return Ease(start, end, t, Vector3.LerpUnclamped);
        }
        else
        {
            IsFinished = true;
            _elapsedTime = 0;
            return end;
        }
    }

    public Quaternion EaseQuaternion(Quaternion start, Quaternion end, float duration, EasingType type)
    {
        if (_elapsedTime < duration && !IsFinished)
        {
            _elapsedTime += Time.deltaTime;
            float t = _elapsedTime / duration;
            t = EasingMode(t, type);
            return Ease(start, end, t, Quaternion.LerpUnclamped);
        }
        else
        {
            IsFinished = true;
            _elapsedTime = 0;
            return end;
        }
    }

    public Color EaseColor(Color start, Color end, float duration, EasingType type)
    {
        if (_elapsedTime < duration && !IsFinished)
        {
            _elapsedTime += Time.deltaTime;
            float t = _elapsedTime / duration;
            t = EasingMode(t, type);
            return Ease(start, end, t, Color.LerpUnclamped);
        }
        else
        {
            IsFinished = true;
            _elapsedTime = 0;
            return end;
        }
    }

    public float EaseFloat(float start, float end, float duration, EasingType type)
    {
        if (_elapsedTime < duration && !IsFinished)
        {
            _elapsedTime += Time.deltaTime;
            float t = _elapsedTime / duration;
            t = EasingMode(t, type);
            return Ease(start, end, t, Mathf.LerpUnclamped);
        }
        else
        {
            IsFinished = true;
            _elapsedTime = 0;
            return end;
        }
    }

    private T Ease<T>(T start, T end, float t, Func<T, T, float, T> lerp)
    {
        return lerp(start, end, t);
    }


    private float EasingMode(float t, EasingType type)
    {
        switch (type)
        {
            case EasingType.Bounce:
            {
                return Easing.easeOutBounce(t);
            }
            case EasingType.Circle:
            {
                return Easing.easeOutCirc(t);
            }
            case EasingType.InOutBack:
            {
                return Easing.easeInOutBack(t);
            }
            case EasingType.InBack:
            {
                return Easing.easeInBack(t);
            }
            case EasingType.InOutElastic:
            {
                return Easing.easeInOutElastic(t);
            }
            case EasingType.InCubic:
            {
                return Easing.easeInCubic(t);
            }
        }
        return 0f;
    }
}
