using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.Animations.Rigging;
using UnityEngine.Pool;
using UnityEditor;
using UnityEngine.UIElements;

// Controlador de personaje en tercera persona para un juego de disparos.
public class ThirdPersonShooterController : MonoBehaviour
{
    // Variables serializadas para configuraci�n en el inspector de Unity.
    [SerializeField] private Rig _aimRig; // Sistema de animaci�n para apuntar.
    [SerializeField] private CinemachineVirtualCamera _aimVirtualCamera; // C�mara virtual para la vista de apuntar.
    [SerializeField] private float _normalSensitivity; // Sensibilidad del control en estado normal.
    [SerializeField] private float _aimSensitivity; // Sensibilidad del control mientras se apunta.
    [SerializeField] private LayerMask aimColliderMask = new LayerMask(); // M�scara de colisi�n para raycasting al apuntar.
    [SerializeField] private Transform debugTransform; // Transform para depuraci�n.

    [Tooltip("Prefab to shoot")]
    [SerializeField] private BulletProjectible pfBulletProjectile; // Prefab del proyectil.
    [Tooltip("End point of gun where shots appear")]
    [SerializeField] private Transform spawnBulletPosition; // Posici�n de generaci�n del proyectil.
    [Tooltip("Time between shots")]
    [SerializeField] private float cooldown = 0.1f;

    private IObjectPool<BulletProjectible> _objectPool;

    // throw an exception if we try to return an existing item, already in the pool
    [SerializeField] private bool _collectionCheck = true;
    [SerializeField] private int _defaultCapacity = 50;
    [SerializeField] private int _maxSize = 100;

    private float _nextTimeToShoot;

    // Variables privadas para almacenar referencias a otros componentes.
    private ThirdPersonController _thirdPersonController; // Controlador de personaje en tercera persona.
    private StarterAssetsInputs _starterAssetsInputs; // Entradas del usuario.
    private Animator _animator; // Componente de animaci�n.
    private float _aimRigWeight; // Peso de la animaci�n de apuntar.
    // Awake se llama cuando se instancia el script.
    private void Awake()
    {
        // Obtener referencias a otros componentes en el mismo GameObject.
        _thirdPersonController = GetComponent<ThirdPersonController>();
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        _animator = GetComponent<Animator>();
        _objectPool = new ObjectPool<BulletProjectible>(CreateProjectile, OnGetFromPool, OnReleaseToPool,
            OnDestroyPooledObject, _collectionCheck, _defaultCapacity, _maxSize);
    }

    private BulletProjectible CreateProjectile()
    {
        BulletProjectible projectileInstance = Instantiate(pfBulletProjectile);
        projectileInstance.ObjectPool = _objectPool;
        return projectileInstance;
    }

    private void OnGetFromPool(BulletProjectible projectileInstance)
    {
        projectileInstance.gameObject.SetActive(true);
    }

    private void OnReleaseToPool(BulletProjectible projectileInstance)
    {
        projectileInstance.gameObject.SetActive(false);
        projectileInstance.transform.position = spawnBulletPosition.position;
    }

    private void OnDestroyPooledObject(BulletProjectible projectileInstance)
    {
        Destroy(projectileInstance.gameObject);
    }

    // Update se llama una vez por Frame.
    private void Update()
    {
        // Variables para manejar la posici�n del rat�n en el mundo y el objeto golpeado por el raycast.
        Vector3 mouseWorldPosition = Vector3.zero;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height /2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        Transform hitTransform = null;
        // Raycast para detectar colisiones con la capa especificada.
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderMask))
        {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
            hitTransform = raycastHit.transform;
        }
        // L�gica para cuando el jugador est� en modo de apuntar.
        if (_starterAssetsInputs.aim)
        {
            // Activar c�mara de apuntar y ajustar sensibilidad y animaci�n.
            _aimVirtualCamera.gameObject.SetActive(true);
            _thirdPersonController.SetSensitivity(_aimSensitivity);
            _animator.SetLayerWeight(1, Mathf.Lerp(_animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

            // Ajustar la direcci�n de apuntado del personaje.
            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
            _aimRigWeight = 1f;
        }
        else
        {
            // Desactivar c�mara de apuntar y volver a la sensibilidad y animaci�n normal.
            _aimVirtualCamera.gameObject.SetActive(false);
            _thirdPersonController.SetSensitivity(_normalSensitivity);
            _animator.SetLayerWeight(1, Mathf.Lerp(_animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
            _aimRigWeight = 0f;
        }
        
        // Ajustar el peso de la animaci�n de apuntar gradualmente.
        _aimRig.weight = Mathf.Lerp(_aimRig.weight, _aimRigWeight, Time.deltaTime * 20f);
    }
    private void FixedUpdate()
    {
        // Variables para manejar la posici�n del rat�n en el mundo y el objeto golpeado por el raycast.
        Vector3 mouseWorldPosition = Vector3.zero;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        Transform hitTransform = null;
        // Raycast para detectar colisiones con la capa especificada.
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderMask))
        {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
            hitTransform = raycastHit.transform;
        }
        // L�gica para disparar.
        if (_starterAssetsInputs.shoot)
        {
            if (hitTransform != null && _objectPool != null && Time.time > _nextTimeToShoot)
            {
                //Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));

                BulletProjectible bulletProjectible = _objectPool.Get();

                if (bulletProjectible != null)
                {
                    // Crear y lanzar un proyectil en la direcci�n del apuntado.
                    Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
                    Vector3 force = aimDir * 800f;
                    bulletProjectible.Activate(spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up), force);
                    bulletProjectible.Deactivate();

                    _nextTimeToShoot = Time.time + cooldown;
                }

                _starterAssetsInputs.shoot = false;
            }
        }
    }
}
