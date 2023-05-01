using Cinemachine;
using UnityEngine;
using Utilities;

namespace Player
{
    public class PlayerScreenShakeEmitter : MonoBehaviour
    {
        private CinemachineImpulseSource _impulseSource;
        private GameObject _mainCamera;
        
        private void Awake()
        {
            _impulseSource = GetComponent<CinemachineImpulseSource>();
            _mainCamera = GameObject.FindWithTag("MainCamera");
        }
        
        private void OnEnable()
        {
            PlayerCollisionController.OnPlayerHit += TriggerShake;
        }

        private void OnDisable()
        {
            PlayerCollisionController.OnPlayerHit -= TriggerShake;
        }

        private void TriggerShake(float _)
        {
            _impulseSource.GenerateImpulse(.5f);
            Invoke(nameof(ResetCamera), .1f);
        }

        private void ResetCamera()
        {
            _mainCamera.GetComponent<CameraPositionResetter>().ResetCameraPosition();
        }
    }
}