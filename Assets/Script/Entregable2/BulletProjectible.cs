using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Clase para controlar el comportamiento de los proyectiles de bala en el juego.
public class BulletProjectible : MonoBehaviour
{
    // Variables para efectos visuales de impacto y sangre, asignables desde el inspector de Unity.
    [SerializeField] private Transform _vfxBulletImpact; // Efecto de impacto de bala.
    [SerializeField] private Transform _vfxBlood; // Efecto de sangre.

    // Variables internas para el cuerpo rígido del proyectil y referencia al volumen local.
    private Rigidbody _bulletRigidbody;
    private GameObject _localVolume;

    // Awake se llama cuando se instancia el script.
    private void Awake()
    {
        // Inicializa el cuerpo rígido del proyectil y busca el objeto "LocalVolume" en la escena.
        _bulletRigidbody = GetComponent<Rigidbody>();
        _localVolume = GameObject.Find("LocalVolume");
    }

    // Start se llama antes del primer frame de actualización.
    private void Start()
    {
        // Define la velocidad del proyectil y aplica esa velocidad al cuerpo rígido.
        float speed = 40f; // Velocidad del proyectil.
        _bulletRigidbody.velocity = transform.forward * speed; // Asigna velocidad hacia adelante.
    }

    // OnTriggerEnter se llama cuando el proyectil entra en un trigger collider.
    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el proyectil golpea a un enemigo tipo Zombie.
        if (other.GetComponent<ZombieTarget>() != null)
        {
            // Crea el efecto de sangre y destruye el proyectil.
            Instantiate(_vfxBlood, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        // Si el proyectil no está dentro del volumen, crea el efecto de impacto de bala.
        else if (!IsWithinVolume())
        {
            Instantiate(_vfxBulletImpact, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        // Nota: Si el proyectil está dentro del volumen, no se hace nada aquí.
    }

    // OnTriggerExit se llama cuando el proyectil sale de un trigger collider.
    private void OnTriggerExit(Collider other)
    {
        // Comprueba si el proyectil sale del volumen local.
        if (other.gameObject == _localVolume)
        {
            // Destruye el proyectil al salir del volumen.
            Destroy(gameObject);
        }
    }

    // Función auxiliar para comprobar si el proyectil está dentro del volumen local.
    bool IsWithinVolume()
    {
        // Retorna true si el proyectil está dentro de los límites del collider del volumen.
        return _localVolume.GetComponent<Collider>().bounds.Contains(transform.position);
    }
}
