using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform follow;
    
    private void Update()
    {
        transform.position = follow.position + Vector3.forward * -10;
    }
}
