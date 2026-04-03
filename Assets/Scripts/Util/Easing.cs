using UnityEngine;
public static class Easing 
{

    //
    // from easings.net
    //

    public static float easeInOutBack(float t)
    {
        float c1 = 1.70158f;
        float c2 = c1 * 1.525f;
        return t < 0.5f ? (Mathf.Pow(2 * t, 2) * ((c2 + 1) * 2 * t - c2)) / 2 
                        : (Mathf.Pow(2 * t - 2, 2) * ((c2 + 1) * (t * 2 - 2) + c2) + 2) / 2;
    }
    public static float easeOutBack(float t)
    {
        float c1 = 1.70158f;
        float c3 = c1 + 1;
        return 1 + c3 * Mathf.Pow(t - 1, 3) + c1 * Mathf.Pow(t - 1, 2);
    }

    public static float easeOutBounce(float t)
    {
        float n1 = 7.5625f;
        float d1 = 2.75f;
        if (t < 1 / d1)
        {
            return n1 * t * t;
        }
        else if (t < 2 / d1)
        {
            return n1 * (t -= 1.5f / d1) * t + 0.75f;
        }
        else if (t < 2.5 / d1)
        {
            return n1 * (t -= 2.25f / d1) * t + 0.9375f;
        }
        else
        {
            return n1 * (t -= 2.625f / d1) * t + 0.984375f;
        }
    }

    public static float easeInOutElastic(float t)
    {
        float c = (2 * Mathf.PI) / 4.5f;
        return t == 0 
                ? 0 
                : t == 1 
                ? 1 
                : t < 0.5f 
                ? -(Mathf.Pow(2, 20 * t - 10) * Mathf.Sin((20 * t - 11.125f) * c)) / 2 
                : Mathf.Pow(2, -20 * t + 10) * Mathf.Sin((20 * t - 11.125f) * c) / 2 + 1;
    }

    public static float easeInCubic(float t)
    {
        return t * t * t;
    }

    public static float easeOutCirc(float t)
    {
        return Mathf.Sqrt(1 - Mathf.Pow(t - 1, 2));
    }

    public static float easeInBack(float t)
    {
        float c1 = 1.70158f;
        float c2 = c1 + 1;
        return c2 * t * t * t - c1 * t * t;
    }
}
