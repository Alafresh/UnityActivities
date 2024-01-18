using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumController : MonoBehaviour
{
    [SerializeField] float speed = 1.5f;
    [SerializeField] float limit = 75f;
    [SerializeField] bool randomStart = false;
    [SerializeField] float random = 0;
    private void Awake()
    {
        if(randomStart)
        {
            random = Random.Range(0, 1);
        }
    }
    private void Update()
    {
        float angle = limit * Mathf.Sin(Time.deltaTime + random * speed);
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
