using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dot : MonoBehaviour
{
    public enum DotType
    {
        BlueDot,
        RedDot,
        GreenDot,
        YellowDot
    }

    public DotType dotType;

    private bool isConnected;

    public bool IsConnected
    {
        get { return isConnected; }
        set { isConnected = value; }
    }
}
