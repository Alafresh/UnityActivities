using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleFormation : MonoBehaviour
{
    public GameObject prefab; // El prefab que deseas instanciar.
    public int numberOfObjects = 10; // El número de objetos en el círculo.
    public float radius = 5f; // El radio del círculo.

    private void Start()
    {
        CreateCircleFormation();
    }

    void CreateCircleFormation()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfObjects;
            Vector3 pos = new Vector3(Mathf.Cos(angle) * radius, 0f, Mathf.Sin(angle) * radius);
            Quaternion rot = Quaternion.Euler(0f, -angle * Mathf.Rad2Deg, 0f);
            Instantiate(prefab, transform.position + pos, rot);
        }
    }
}
