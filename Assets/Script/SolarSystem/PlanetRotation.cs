using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    [SerializeField] Transform sol;
    [SerializeField] float speed;
    [SerializeField] Vector3 axis;
    // Start is called before the first frame update
    void Start()
    {
        //axis = new Vector3 (0, Random.Range(0f, 1f), Random.Range(0f, 1f));
        //speed = Random.Range(5, 100);
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(sol.position, axis, speed * Time.deltaTime);
    }
}
