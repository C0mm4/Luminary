using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Func
{
    public static PointPosition GetPointPosition(Vector2 baseP, Vector2 targetP)
    {
        float horizonDist = targetP.x - baseP.x;
        float verticalDist = targetP.y - baseP.y;

        if (Mathf.Abs(horizonDist) >= Mathf.Abs(verticalDist))
        {
            if (horizonDist > 0)
            {
                return PointPosition.Right;
            }
            else
            {
                return PointPosition.Left;
            }

        }
        else
        {
            if (verticalDist > 0)
            {
                return PointPosition.Up;
            }
            else
            {
                return PointPosition.Down;
            }
        }
    }
    public static void SetRectTransform(GameObject go, Vector3 pos = default(Vector3))
    {
        RectTransform rt = go.GetComponent<RectTransform>();
        rt.transform.SetParent(GameManager.Instance.canvas.transform, false);
        rt.transform.localScale = Vector3.one;
        rt.transform.localPosition = pos;
    }
}