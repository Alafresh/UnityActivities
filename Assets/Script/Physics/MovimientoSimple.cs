using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoSimple : MonoBehaviour
{
    public float velocidad = 5f;
    // Update is called once per frame
    void Update()
    {
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

    }
}
