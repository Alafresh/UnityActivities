using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipController : MonoBehaviour
{
    public float speed = 10f; // Velocidad de movimiento
    public float rotationSpeed = 100f; // Velocidad de rotaci�n

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Movimiento hacia adelante y atr�s
        float moveVertical = Input.GetAxis("Vertical");
        rb.AddForce(transform.forward * moveVertical * speed);

        // Rotaci�n
        float rotateHorizontal = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * rotateHorizontal * rotationSpeed * Time.deltaTime);
    }
}
