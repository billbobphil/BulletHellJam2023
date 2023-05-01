using UnityEngine;

namespace Utilities
{
    public class CameraPositionResetter : MonoBehaviour
    {
        public Vector3 originalCameraPosition;
        
        public void ResetCameraPosition()
        {
            transform.position = originalCameraPosition;
        }
    }
}
