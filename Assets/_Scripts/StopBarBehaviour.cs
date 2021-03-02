using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopBarBehaviour : MonoBehaviour
{
    public bool isRotating;
    public GameObject target;
    public float speed;
    public bool inZone;
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TargetZone")
        {
            inZone = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TargetZone")
        {
            inZone = false;
        }
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
