using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _aimVirtualCamera;
    [SerializeField] private float _normalSensitivity;
    [SerializeField] private float _aimSensitivity;
    [SerializeField] private LayerMask aimColliderMask = new LayerMask();
    [SerializeField] private Transform debugTransform;


    private ThirdPersonController _thirdPersonController;
    private StarterAssetsInputs _starterAssetsInputs;
    private void Awake()
    {
       _thirdPersonController = GetComponent<ThirdPersonController>();
       _starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }
    private void Update()
    {
        if(_starterAssetsInputs.aim)
        {
            _aimVirtualCamera.gameObject.SetActive(true);
            _thirdPersonController.SetSensitivity(_aimSensitivity);
        }
        else
        {
            _aimVirtualCamera.gameObject.SetActive(false);
            _thirdPersonController.SetSensitivity(_normalSensitivity);
        }
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderMask))
        {
            debugTransform.position = raycastHit.point;
        }
    }
}
