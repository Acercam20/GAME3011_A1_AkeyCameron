using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopBarBehaviour : MonoBehaviour
{
    public bool isRotating;
    public GameObject target;
    public float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isRotating)
        {
            transform.RotateAround(target.transform.position, Vector3.forward, speed * Time.deltaTime);
        }
    }
}
