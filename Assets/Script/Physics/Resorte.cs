using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resorte : MonoBehaviour
{
    public float constanteElastica = 100f;
    public float longitudNatural = 0.5f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ObjectoCae"))
        {
            AplicarFuerzaRestauradora(collision.gameObject);
        }
    }
    private void AplicarFuerzaRestauradora(GameObject obj)
    {
        Rigidbody rigidbody = obj.GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            // Calcular la posicion relativa entre la superficie y el objecto que cayo
            float posicionRelativa = obj.transform.position.y - transform.position.y;
            // Calcular la fuerza elastica segun la ley de hooke
            float fuerzaElastica = constanteElastica * (posicionRelativa - longitudNatural);
            // Aplicar la fuerza hacia arriba
            rigidbody.AddForce(Vector3.up * fuerzaElastica, ForceMode.Impulse);
        }
    }
}
