using UnityEngine;
using System.Collections;

public class DriveForward : MonoBehaviour
{
    void Update()
    {
        transform.position += Vector3.forward *20* Time.deltaTime;
    }
}