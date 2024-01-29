using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

// Clase para controlar el comportamiento de los proyectiles de bala en el juego.
public class BulletProjectible : MonoBehaviour
{
    // Variables para efectos visuales de impacto y sangre, asignables desde el inspector de Unity.
    [SerializeField] private GameObject _vfxBulletImpact; // Efecto de impacto de bala.
    [SerializeField] private GameObject _vfxBlood; // Efecto de sangre.

    [SerializeField] private float _timeOutDelay = 3f;
    private IObjectPool<BulletProjectible> objectPool;
    public IObjectPool<BulletProjectible> ObjectPool { set => objectPool = value; }
  
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

    // OnTriggerEnter se llama cuando el proyectil entra en un trigger collider.
    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el proyectil golpea a un enemigo tipo Zombie.
        if (other.GetComponent<ZombieTarget>() != null)
        {
            // Crea el efecto de sangre y destruye el proyectil.
            GameObject vfx = Instantiate(_vfxBlood, transform.position, Quaternion.identity);
            Destroy(vfx, 2f);
        }
        // Si el proyectil no está dentro del volumen, crea el efecto de impacto de bala.
        else if (!IsWithinVolume())
        {
            GameObject vfx = Instantiate(_vfxBulletImpact, transform.position, Quaternion.identity);
            Destroy(vfx, 2f);
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

    public void Activate(Vector3 position, Quaternion rotation, Vector3 force)
    {
        transform.SetPositionAndRotation(position, rotation);
        _bulletRigidbody.velocity = Vector3.zero; // Resetear la velocidad por si acaso
        _bulletRigidbody.AddForce(force, ForceMode.Acceleration);
        // Resto de la inicialización necesaria
    }


    public void Deactivate()
    {      
        StartCoroutine(DeactivateRoutine(_timeOutDelay));
    }

    IEnumerator DeactivateRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Reset rigidbody
        _bulletRigidbody.velocity = Vector3.zero;
        _bulletRigidbody.angularVelocity = Vector3.zero;

        objectPool.Release(this);


    }
}
