using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacionAsteroide : MonoBehaviour
{
    Vector3 rotationSpeed;
    private void Start()
    {
        rotationSpeed = new Vector3(Random.Range(0, 200), Random.Range(0, 200), Random.Range(0, 200));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
