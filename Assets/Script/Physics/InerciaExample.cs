using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InerciaExample : MonoBehaviour
{
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //fuerza constante
            rb.AddForce(Vector3.right * 5f, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //fuerza constante
            rb.AddForce(Vector3.left * 5f, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //fuerza constante
            rb.AddForce(Vector3.forward * 5f, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            //fuerza constante
            rb.AddForce(Vector3.back * 5f, ForceMode.Force);
        }
    }
}
