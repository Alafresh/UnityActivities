using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imprimir : MonoBehaviour
{
    int framesOperacion;

    // Update is called once per frame
    void Update()
    {
        framesOperacion += 1;
        if (framesOperacion % 30 == 0)
        {
            print("Han Pasado 30 frames "+ framesOperacion);
        }
    }
}
