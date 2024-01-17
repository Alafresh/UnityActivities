using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectible : MonoBehaviour
{
    [SerializeField] private Transform _vfxBulletImpact;
    [SerializeField] private Transform _vfxBlood;
    private Rigidbody _bulletRigidbody;

    private void Awake()
    {
        _bulletRigidbody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        float speed = 40f;
        _bulletRigidbody.velocity = transform.forward * speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ZombieTarget>() != null)
        {
            Instantiate(_vfxBlood, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_vfxBulletImpact, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
