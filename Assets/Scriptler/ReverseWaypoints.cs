using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseWaypoints : MonoBehaviour
{
    public static Transform[] reversePoints;

    void Awake()
    {
        reversePoints = new Transform[transform.childCount];
        for (int i = 0; i < reversePoints.Length; i++)
        {
            reversePoints[i] = transform.GetChild(i);
        }
    }
}
