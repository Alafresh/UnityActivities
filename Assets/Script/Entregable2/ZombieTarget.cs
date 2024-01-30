using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieTarget : MonoBehaviour
{
    [SerializeField] private GameObject _targetLook;

    private GameObject _target;
    private NavMeshAgent _agent;
    private Animator _anim;


    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
        _target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        _agent.SetDestination(_target.transform.position);

        if (_agent.velocity.x == 0 && _agent.velocity.y == 0 && _agent.velocity.z == 0)
        {
            _targetLook.transform.LookAt(_target.transform);
            transform.rotation = Quaternion.Euler(0f, _targetLook.transform.rotation.eulerAngles.y, 0f);
            _anim.SetBool("walk", false);
        }
        else
        {
            _anim.SetBool("walk", true);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _anim.SetBool("walk", false);
            _target.GetComponent<Player>().GetDamage();
            Debug.Log("Muere");
        }
    }
}
