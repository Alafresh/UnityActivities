using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectible : MonoBehaviour
{
    private Rigidbody _bulletRigidbody;

    private void Awake()
    {
        _bulletRigidbody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        float speed = 10f;
        _bulletRigidbody.velocity = transform.forward * speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
