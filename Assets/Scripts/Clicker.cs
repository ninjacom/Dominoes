using System.Collections;
using UnityEngine;

public class Clicker
{
    public bool clicked()
    {
    #if (UNITY_ANDROID || UNITY_IPHONE) //TODO: FIX THIS
        return Cardboard.SDK.CardboardTriggered;
    #else
        return Input.GetButtonDown("Fire1");
    #endif
    }
}