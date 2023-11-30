using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotacion : MonoBehaviour
{
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotacion = Quaternion.Euler(new Vector3(0, 20, 0) * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * rotacion);
    }
}
