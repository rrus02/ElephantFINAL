using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{

    public float rotatespeed;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(0, 0, rotatespeed);

    }
}
