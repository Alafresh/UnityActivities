using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // La nave espacial a seguir
    public Vector3 offset; // Desplazamiento de la cámara respecto al objetivo
    public float smoothSpeed = 0.125f; // Velocidad con la que la cámara sigue al objetivo

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }
}
