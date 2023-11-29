using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject sol; // Referencia al objeto sol
    private Rigidbody rb; // Rigidbody del planeta

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 direction = sol.transform.position - transform.position; // Dirección hacia el sol
        float distance = direction.magnitude; // Distancia al sol
        float forceMagnitude = (rb.mass * sol.GetComponent<Rigidbody>().mass) / (distance * distance); // Fórmula de gravitación
        Vector3 force = direction.normalized * forceMagnitude; // Fuerza gravitacional

        rb.AddForce(force); // Aplicar la fuerza al planeta
    }
}